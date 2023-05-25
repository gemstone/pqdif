//******************************************************************************************************
//  PhysicalWriter.cs - Gbtc
//
//  Copyright © 2015, Grid Protection Alliance.  All Rights Reserved.
//
//  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
//  the NOTICE file distributed with this work for additional information regarding copyright ownership.
//  The GPA licenses this file to you under the MIT License (MIT), the "License"; you may
//  not use this file except in compliance with the License. You may obtain a copy of the License at:
//
//      http://opensource.org/licenses/MIT
//
//  Unless agreed to in writing, the subject software distributed under the License is distributed on an
//  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
//  License for the specific language governing permissions and limitations.
//
//  Code Modification History:
//  ----------------------------------------------------------------------------------------------------
//  09/15/2015 - Stephen C. Wills
//       Generated original version of source code.
//
//******************************************************************************************************

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ionic.Zlib;

namespace Gemstone.PQDIF.Physical
{
    /// <summary>
    /// Represents a writer used to write the physical
    /// structure of a PQDIF file to a byte stream.
    /// </summary>
    public class PhysicalWriter : IAsyncDisposable, IDisposable
    {
        #region [ Members ]

        // Fields
        private readonly Stream m_stream;
        private CompressionStyle m_compressionStyle;
        private CompressionAlgorithm m_compressionAlgorithm;
        private readonly bool m_leaveOpen;

        private bool m_disposed;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Creates a new instance of the <see cref="PhysicalWriter"/> class.
        /// </summary>
        /// <param name="filePath">The path to the file where the PQDIF data is to be written.</param>
        public PhysicalWriter(string filePath)
            : this(new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="PhysicalWriter"/> class.
        /// </summary>
        /// <param name="stream">The stream to write the PQDIF data to.</param>
        /// <param name="leaveOpen">Indicates whether to leave the stream open when disposing of the writer.</param>
        /// <exception cref="InvalidOperationException"><paramref name="stream"/> is not writable.</exception>
        public PhysicalWriter(Stream stream, bool leaveOpen = false)
        {
            if (!stream.CanWrite)
                throw new InvalidOperationException("Cannot write to the given stream.");

            m_stream = stream;
            m_leaveOpen = leaveOpen;
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets the compression style used by the PQDIF file.
        /// </summary>
        /// <exception cref="NotSupportedException">Attempt is made to set <see cref="Physical.CompressionStyle.TotalFile"/>.</exception>
        public CompressionStyle CompressionStyle
        {
            get
            {
                return m_compressionStyle;
            }
            set
            {
                if (value == CompressionStyle.TotalFile)
                    throw new NotSupportedException("Total file compression has been deprecated and is not supported");

                m_compressionStyle = value;
            }
        }

        /// <summary>
        /// Gets or sets the compression algorithm used by the PQDIF file.
        /// </summary>
        /// <exception cref="NotSupportedException">Attempt is made to set <see cref="Physical.CompressionAlgorithm.PKZIP"/>.</exception>
        public CompressionAlgorithm CompressionAlgorithm
        {
            get
            {
                return m_compressionAlgorithm;
            }
            set
            {
                if (value == CompressionAlgorithm.PKZIP)
                    throw new NotSupportedException("PKZIP compression has been deprecated and is not supported");

                m_compressionAlgorithm = value;
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Writes the given record to the PQDIF file.
        /// </summary>
        /// <param name="record">The record to be written to the file.</param>
        /// <param name="lastRecord">Indicates whether this record is the last record in the file.</param>
        /// <exception cref="InvalidDataException">The PQDIF data is invalid.</exception>
        /// <exception cref="ObjectDisposedException">The writer was disposed.</exception>
        public async Task WriteRecordAsync(Record record, bool lastRecord = false)
        {
            if (m_disposed)
                throw new ObjectDisposedException(GetType().Name);

            byte[] bodyImage;
            uint checksum;

            using (MemoryStream bodyStream = new())
            await using (BinaryWriter bodyWriter = new(bodyStream))
            {
                // Write the record body to the memory stream
                if (record.Body is not null)
                    WriteCollection(bodyWriter, record.Body.Collection);

                // Read and compress the body to a byte array
                bodyImage = bodyStream.ToArray();

                if (m_compressionAlgorithm == CompressionAlgorithm.Zlib && m_compressionStyle == CompressionStyle.RecordLevel)
                    bodyImage = ZlibStream.CompressBuffer(bodyImage);

                // Create the checksum after compression
                uint adler = Adler.Adler32(0u, null, 0, 0);
                checksum = Adler.Adler32(adler, bodyImage, 0, bodyImage.Length);

                // Save the checksum in the record body
                if (record.Body is not null)
                    record.Body.Checksum = checksum;
            }

            // Make sure the header points to the correct location based on the size of the body
            record.Header.HeaderSize = 64;
            record.Header.BodySize = bodyImage.Length;
            record.Header.NextRecordPosition = (int)m_stream.Length + record.Header.HeaderSize + record.Header.BodySize;
            record.Header.Checksum = checksum;

            using (MemoryStream headerStream = new())
            await using (BinaryWriter headerWriter = new(headerStream))
            {
                // Write up to the next record position
                headerWriter.Write(record.Header.RecordSignature.ToByteArray());
                headerWriter.Write(record.Header.RecordTypeTag.ToByteArray());
                headerWriter.Write(record.Header.HeaderSize);
                headerWriter.Write(record.Header.BodySize);

                // The PQDIF standard defines the NextRecordPosition to be 0 for the last record in the file
                if (!lastRecord)
                    headerWriter.Write(record.Header.NextRecordPosition);
                else
                    headerWriter.Write(0);

                // Write the rest of the header as well as the body
                headerWriter.Write(record.Header.Checksum);
                headerWriter.Write(record.Header.Reserved);

                byte[] headerImage = headerStream.ToArray();
                await m_stream.WriteAsync(headerImage, 0, headerImage.Length);
            }

            await m_stream.WriteAsync(bodyImage, 0, bodyImage.Length);

            // Dispose of the writer if this is the last record
            if (!m_stream.CanSeek && lastRecord)
                await DisposeAsync();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public async ValueTask DisposeAsync()
        {
            if (m_disposed)
                return;

            try
            {
#if NETSTANDARD2_0
                if (!m_leaveOpen)
                {
                    await m_stream.FlushAsync();
                    m_stream.Dispose();
                }
#else
                if (!m_leaveOpen)
                    await m_stream.DisposeAsync();
#endif
            }
            finally
            {
                m_disposed = true;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            if (m_disposed)
                return;

            try
            {
                if (!m_leaveOpen)
                    m_stream.Dispose();
            }
            finally
            {
                m_disposed = true;
            }
        }

        private void WriteCollection(BinaryWriter writer, CollectionElement collection)
        {
            int linkPosition = (int)writer.BaseStream.Position + 4 + 28 * collection.Size;

            writer.Write(collection.Size);

            foreach (Element element in collection.Elements)
            {
                bool isEmbedded = IsEmbedded(element);

                writer.Write(element.TagOfElement.ToByteArray());
                writer.Write((byte)element.TypeOfElement);
                writer.Write((byte)element.TypeOfValue);
                writer.Write(isEmbedded ? (byte)1 : (byte)0);
                writer.Write((byte)0);

                if (!isEmbedded)
                {
                    int padSize = GetPaddedByteSize(element);
                    writer.Write(linkPosition);
                    writer.Write(padSize);
                    linkPosition += padSize;
                }
                else
                {
                    ScalarElement scalar = element as ScalarElement
                        ?? throw new InvalidDataException("Embedded element is not a scalar element.");

                    WriteScalar(writer, scalar);

                    for (int i = element.TypeOfValue.GetByteSize(); i < 8; i++)
                        writer.Write((byte)0);
                }
            }

            foreach (Element element in collection.Elements)
            {
                if (IsEmbedded(element))
                    continue;

                switch (element.TypeOfElement)
                {
                    case ElementType.Collection:
                        CollectionElement nestedCollection = element as CollectionElement
                            ?? throw new InvalidDataException("Element with type Collection is not a CollectionElement.");

                        WriteCollection(writer, nestedCollection);
                        break;

                    case ElementType.Vector:
                        VectorElement vector = element as VectorElement
                            ?? throw new InvalidDataException("Element with type Vector is not a VectorElement.");

                        WriteVector(writer, vector);
                        break;

                    case ElementType.Scalar:
                        ScalarElement scalar = element as ScalarElement
                            ?? throw new InvalidDataException("Element with type Scalar is not a ScalarElement.");

                        WriteScalar(writer, scalar);
                        break;
                }

                int byteSize = GetByteSize(element);
                int padSize = GetPaddedByteSize(element);

                for (int i = 0; i < padSize - byteSize; i++)
                    writer.Write((byte)0);
            }
        }

        private void WriteVector(BinaryWriter writer, VectorElement vector)
        {
            writer.Write(vector.Size);
            writer.Write(vector.GetValues());
        }

        private void WriteScalar(BinaryWriter writer, ScalarElement scalar)
        {
            writer.Write(scalar.GetValue());
        }

        private int GetPaddedByteSize(Element element)
        {
            int byteSize = GetByteSize(element);
            int padSize = byteSize + 3;
            return padSize / 4 * 4;
        }

        private int GetByteSize(Element element)
        {
            switch (element.TypeOfElement)
            {
                case ElementType.Collection:
                    CollectionElement collection = element as CollectionElement
                        ?? throw new InvalidDataException("Element with type Collection is not a CollectionElement.");

                    return GetByteSize(collection);

                case ElementType.Vector:
                    VectorElement vector = element as VectorElement
                        ?? throw new InvalidDataException("Element with type Vector is not a VectorElement.");

                    return GetByteSize(vector);

                case ElementType.Scalar:
                    ScalarElement scalar = element as ScalarElement
                        ?? throw new InvalidDataException("Element with type Scalar is not a ScalarElement.");

                    return GetByteSize(scalar);

                default:
                    return 0;
            }
        }

        private int GetByteSize(CollectionElement collection)
        {
            int sum = collection.Elements
                .Where(element => !IsEmbedded(element))
                .Sum(GetPaddedByteSize);

            return 4 + 28 * collection.Size + sum;
        }

        private int GetByteSize(VectorElement vector) =>
            4 + vector.Size * vector.TypeOfValue.GetByteSize();

        private int GetByteSize(ScalarElement scalar) =>
            scalar.TypeOfValue.GetByteSize();

        private bool IsEmbedded(Element element) =>
            element.TypeOfElement == ElementType.Scalar && element.TypeOfValue.GetByteSize() < 8;

#endregion
    }
}
