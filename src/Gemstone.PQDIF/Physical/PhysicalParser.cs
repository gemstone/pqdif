//******************************************************************************************************
//  PhysicalParser.cs - Gbtc
//
//  Copyright © 2012, Grid Protection Alliance.  All Rights Reserved.
//
//  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
//  the NOTICE file distributed with this work for additional information regarding copyright ownership.
//  The GPA licenses this file to you under the MIT License (MIT), the "License"; you may
//  not use this file except in compliance with the License. You may obtain a copy of the License at:
//
//      http://www.opensource.org/licenses/MIT
//
//  Unless agreed to in writing, the subject software distributed under the License is distributed on an
//  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
//  License for the specific language governing permissions and limitations.
//
//  Code Modification History:
//  ----------------------------------------------------------------------------------------------------
//  05/03/2012 - Stephen C. Wills, Grid Protection Alliance
//       Generated original version of source code.
//  12/17/2012 - Starlynn Danyelle Gilliam
//       Modified Header.
//
//******************************************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Ionic.Zlib;

namespace Gemstone.PQDIF.Physical
{
    #region [ Enumerations ]

    /// <summary>
    /// Enumeration which defines the types of compression used in PQDIF files.
    /// </summary>
    public enum CompressionStyle : uint
    {
        /// <summary>
        /// No compression.
        /// </summary>
        None = 0,

        /// <summary>
        /// Compress the entire file after the container record.
        /// This compression style is deprecated and is currently
        /// not supported by this PQDIF library.
        /// </summary>
        TotalFile = 1,

        /// <summary>
        /// Compress the body of each record.
        /// </summary>
        RecordLevel = 2
    }

    /// <summary>
    /// Enumeration which defines the algorithms used to compress PQDIF files.
    /// </summary>
    public enum CompressionAlgorithm : uint
    {
        /// <summary>
        /// No compression.
        /// </summary>
        None = 0,

        /// <summary>
        /// Zlib compression.
        /// http://www.zlib.net/
        /// </summary>
        Zlib = 1,

        /// <summary>
        /// PKZIP compression.
        /// This compression algorithm is deprecated and
        /// is currently not supported by this PQDIF library.
        /// </summary>
        PKZIP = 64
    }

    #endregion

    /// <summary>
    /// Represents a parser which parses the physical structure of a PQDIF file.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class is used internally by the <see cref="Logical.LogicalParser"/> to read the
    /// physical structure of the PQDIF file. If your goal is to read data from a PQD file into
    /// an application, you probably do not want to instantiate the physical parser directly.
    /// Instead, the logical parser will apply the rules governing the logical structure of the PQDIF
    /// file to make the data more readily usable by an application that will be processing that data.
    /// </para>
    ///
    /// <para>
    /// The following example of usage was adapted from the PQDIFDump utility, which represents a
    /// bare minimum level of effort to read an uncompressed PQDIF file and display its contents
    /// in a console application.
    /// </para>
    ///
    /// <code>
    /// string filePath = args[0];
    /// await using PhysicalParser parser = new PhysicalParser(filePath);
    /// await parser.OpenAsync();
    ///
    /// while (parser.HasNextRecord())
    /// {
    ///     Record record = await parser.GetNextRecordAsync();
    ///     Console.WriteLine(record);
    ///     Console.WriteLine();
    /// }
    /// </code>
    /// </remarks>
    public class PhysicalParser : IAsyncDisposable, IDisposable
    {
        #region [ Members ]

        // Nested Types
        private class UnknownElement : Element
        {
            public UnknownElement(ElementType typeOfElement) =>
                TypeOfElement = typeOfElement;

            public override ElementType TypeOfElement { get; }
        }

        private Stream? m_stream;
        private CompressionStyle m_compressionStyle;
        private CompressionAlgorithm m_compressionAlgorithm;
        private bool m_leaveStreamOpen;

        private bool m_hasNextRecord;
        private readonly HashSet<long> m_headerAddresses;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Creates a new instance of the <see cref="PhysicalParser"/> class.
        /// </summary>
        public PhysicalParser()
        {
            m_headerAddresses = new HashSet<long>();
            ExceptionList = new List<Exception>();
            MaximumExceptionsAllowed = 100;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="PhysicalParser"/> class.
        /// </summary>
        /// <param name="filePath">Path to the PQDIF file to be parsed.</param>
        public PhysicalParser(string filePath)
            : this()
        {
            FilePath = filePath;
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets the file path (not just the name) of the PQDIF file to be parsed.
        /// Obsolete in favor of <see cref="FilePath"/>.
        /// </summary>
        [Obsolete("Property is deprecated. Please use FilePath instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string? FileName
        {
            get => FilePath;
            set => FilePath = value;
        }

        /// <summary>
        /// Gets or sets the file path of the PQDIF file to be parsed.
        /// </summary>
        public string? FilePath { get; set; }

        /// <summary>
        /// Gets all the exceptions encountered while parsing.
        /// </summary>
        public List<Exception> ExceptionList { get; }

        /// <summary>
        /// Gets or sets the compression style used by the PQDIF file.
        /// </summary>
        /// <exception cref="NotSupportedException"><see cref="Physical.CompressionStyle.TotalFile"/> compression has been deprecated by the standard and is not supported by this parser.</exception>
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
        /// <exception cref="NotSupportedException"><see cref="Physical.CompressionAlgorithm.PKZIP"/> compression has been deprecated by the standard and is not supported by this parser.</exception>
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

        /// <summary>
        /// Gets or sets the maximum number of exceptions
        /// in the exception list before parser will quit.
        /// </summary>
        /// <remarks>Enter a negative value to disable this safeguard.</remarks>
        public int MaximumExceptionsAllowed { get; set; }

        /// <summary>
        /// Gets a value that indicates whether the maximum number of exceptions has been reached.
        /// </summary>
        public bool MaximumExceptionsReached
        {
            get
            {
                return MaximumExceptionsAllowed >= 0 && ExceptionList.Count > MaximumExceptionsAllowed;
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Opens the PQDIF file.
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="FilePath"/> has not been defined.</exception>
        public Task OpenAsync()
        {
            if (FilePath is null)
                throw new InvalidOperationException("Unable to open PQDIF file when no file name has been defined.");

            using (m_stream)
                m_stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);

            m_hasNextRecord = true;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Opens a PQDIF file from a stream of data.
        /// </summary>
        /// <param name="stream">The stream from which to read the PQDIF file.</param>
        /// <param name="leaveOpen">True to leave the stream open when closing the parser; false otherwise.</param>
        /// <exception cref="InvalidOperationException"><paramref name="stream"/> is not both readable and seekable.</exception>
        public Task OpenAsync(Stream stream, bool leaveOpen = false)
        {
            if (!stream.CanRead)
                throw new InvalidOperationException("Stream must be readable in order to parse PQDIF file data.");

            if (!stream.CanSeek)
                throw new InvalidOperationException("Stream must be seekable in order to parse PQDIF file data.");

            using (m_stream)
                m_stream = stream;

            m_leaveStreamOpen = leaveOpen;
            m_hasNextRecord = true;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Returns true if this parser has not reached the end of the PQDIF file.
        /// </summary>
        /// <returns><c>false</c> if the end of the file has been reached; <c>true</c> otherwise</returns>
        public bool HasNextRecord() => m_hasNextRecord;

        /// <summary>
        /// Reads the next record from the PQDIF file.
        /// </summary>
        /// <returns>The next record to be parsed from the PQDIF file.</returns>
        /// <exception cref="InvalidOperationException">The PQDIF file is not open.</exception>
        /// <exception cref="EndOfStreamException">End of stream encountered while reading the next record.</exception>
        public async Task<Record> GetNextRecordAsync()
        {
            if (m_stream is null)
                throw new InvalidOperationException("PQDIF file is not open.");

            if (!m_hasNextRecord)
                Reset();

            RecordHeader header = await ReadRecordHeaderAsync();
            RecordBody body = await ReadRecordBodyAsync(header.BodySize);

            if (body.Collection.TagOfElement == Guid.Empty)
                body.Collection.TagOfElement = header.RecordTypeTag;

            m_hasNextRecord =
                header.NextRecordPosition > 0 &&
                header.NextRecordPosition < m_stream.Length &&
                m_headerAddresses.Add(header.NextRecordPosition) &&
                !MaximumExceptionsReached;

            m_stream.Seek(header.NextRecordPosition, SeekOrigin.Begin);

            return new Record(header, body);
        }

        /// <summary>
        /// Jumps in the file to the location of the record with the given header.
        /// </summary>
        /// <param name="header">The header to seek to.</param>
        /// <exception cref="InvalidOperationException">The PQDIF file is not open.</exception>
        public void Seek(RecordHeader header)
        {
            if (m_stream is null)
                throw new InvalidOperationException("PQDIF file is not open.");

            m_stream.Seek(header.Position, SeekOrigin.Begin);
        }

        /// <summary>
        /// Sets the parser back to the beginning of the file.
        /// </summary>
        /// <exception cref="InvalidOperationException">The PQDIF file is not open.</exception>
        public void Reset()
        {
            if (m_stream is null)
                throw new InvalidOperationException("PQDIF file is not open.");

            m_compressionAlgorithm = CompressionAlgorithm.None;
            m_compressionStyle = CompressionStyle.None;
            m_stream.Seek(0, SeekOrigin.Begin);
            m_hasNextRecord = true;
            m_headerAddresses.Clear();
            ExceptionList.Clear();
        }

        /// <summary>
        /// Closes the PQDIF file.
        /// </summary>
        /// <exception cref="InvalidOperationException">The PQDIF file is not open.</exception>
        public async Task CloseAsync()
        {
            if (m_stream is null)
                throw new InvalidOperationException("PQDIF file is not open.");

            await m_stream.FlushAsync();
            m_stream.Close();
            m_hasNextRecord = false;
        }

        /// <summary>
        /// Releases all resources held by this parser.
        /// </summary>
        public async ValueTask DisposeAsync()
        {
#if NETSTANDARD2_0
            if (!m_leaveStreamOpen && m_stream is not null)
            {
                await m_stream.FlushAsync();
                m_stream.Dispose();
            }
#else
            if (!m_leaveStreamOpen && m_stream is not null)
                await m_stream.DisposeAsync();
#endif

            m_stream = null;
            m_hasNextRecord = false;
        }

        /// <summary>
        /// Releases all resources held by this parser.
        /// </summary>
        public void Dispose()
        {
            if (!m_leaveStreamOpen && m_stream is not null)
                m_stream.Dispose();

            m_stream = null;
            m_hasNextRecord = false;
        }

        // Reads bytes from the stream into a byte array of the given size.
        private async Task<byte[]> ReadBytesAsync(int size)
        {
            if (m_stream is null)
                throw new InvalidOperationException("PQDIF file is not open.");

            byte[] data = new byte[size];
            int offset = 0;

            while (offset < size)
            {
                int count = size - offset;
                int bytesRead = await m_stream.ReadAsync(data, offset, count);

                if (bytesRead == 0)
                    throw new EndOfStreamException("Unexpected end of stream encountered");

                offset += bytesRead;
            }

            return data;
        }

        // Reads the header of a record from the PQDIF file.
        private async Task<RecordHeader> ReadRecordHeaderAsync()
        {
            if (m_stream is null)
                throw new InvalidOperationException("PQDIF file is not open.");

            const int HeaderSize = 64;
            int position = (int)m_stream.Position;
            byte[] headerData = await ReadBytesAsync(HeaderSize);

            using MemoryStream memoryStream = new(headerData);
            using BinaryReader reader = new(memoryStream);

            return new RecordHeader
            {
                Position = position,
                RecordSignature = new Guid(reader.ReadBytes(16)),
                RecordTypeTag = new Guid(reader.ReadBytes(16)),
                HeaderSize = reader.ReadInt32(),
                BodySize = reader.ReadInt32(),
                NextRecordPosition = reader.ReadInt32(),
                Checksum = reader.ReadUInt32(),
                Reserved = reader.ReadBytes(16)
            };
        }

        // Reads the body of a record from the PQDIF file.
        private async Task<RecordBody> ReadRecordBodyAsync(int byteSize)
        {
            if (m_stream is null)
                throw new InvalidOperationException("PQDIF file is not open.");

            if (byteSize == 0)
                return new RecordBody(Guid.Empty);

            byte[] bytes = await ReadBytesAsync(byteSize);
            uint adler = Adler.Adler32(0u, null, 0, 0);
            uint checksum = Adler.Adler32(adler, bytes, 0, bytes.Length);

            if (m_compressionAlgorithm == CompressionAlgorithm.Zlib && m_compressionStyle != CompressionStyle.None)
                bytes = ZlibStream.UncompressBuffer(bytes);

            using MemoryStream stream = new(bytes);
            using BinaryReader reader = new(stream);
            CollectionElement root = ReadCollection(reader);
            return new RecordBody(root) { Checksum = checksum };
        }

        // Reads an element from the PQDIF file.
        private Element ReadElement(BinaryReader recordBodyReader)
        {
            Guid tagOfElement = Guid.Empty;
            ElementType typeOfElement = 0;
            PhysicalType typeOfValue = 0;

            // Calculate the location of the next element
            // after this one in case we encounter any errors
            long nextLink = recordBodyReader.BaseStream.Position + 28L;

            try
            {
                tagOfElement = new Guid(recordBodyReader.ReadBytes(16));
                typeOfElement = (ElementType)recordBodyReader.ReadByte();
                typeOfValue = (PhysicalType)recordBodyReader.ReadByte();
                bool isEmbedded = recordBodyReader.ReadByte() != 0;

                // Read reserved byte
                recordBodyReader.ReadByte();

                long returnLink = recordBodyReader.BaseStream.Position + 8L;

                if (!isEmbedded || typeOfElement != ElementType.Scalar)
                {
                    long link = recordBodyReader.ReadInt32();

                    if (link < 0 || link >= recordBodyReader.BaseStream.Length)
                        throw new EndOfStreamException("Element link is outside the bounds of the file");

                    recordBodyReader.BaseStream.Seek(link, SeekOrigin.Begin);
                }

                Element element;
                switch (typeOfElement)
                {
                    case ElementType.Collection:
                        element = ReadCollection(recordBodyReader);
                        break;

                    case ElementType.Scalar:
                        element = ReadScalar(recordBodyReader, typeOfValue);
                        break;

                    case ElementType.Vector:
                        element = ReadVector(recordBodyReader, typeOfValue);
                        break;

                    default:
                        element = new UnknownElement(typeOfElement);
                        element.TypeOfValue = typeOfValue;
                        break;
                }

                element.TagOfElement = tagOfElement;
                recordBodyReader.BaseStream.Seek(returnLink, SeekOrigin.Begin);

                return element;
            }
            catch (Exception ex)
            {
                ExceptionList.Add(ex);

                // Jump to the location of the next element after this one
                if (nextLink < recordBodyReader.BaseStream.Length)
                    recordBodyReader.BaseStream.Seek(nextLink, SeekOrigin.Begin);
                else
                    recordBodyReader.BaseStream.Seek(0L, SeekOrigin.End);

                return new ErrorElement(typeOfElement, ex)
                {
                    TagOfElement = tagOfElement,
                    TypeOfValue = typeOfValue
                };
            }
        }

        // Reads a collection element from the PQDIF file.
        private CollectionElement ReadCollection(BinaryReader recordBodyReader)
        {
            int size = recordBodyReader.ReadInt32();
            CollectionElement collection = new(size);
            
            for (int i = 0; i < size; i++)
            {
                collection.AddElement(ReadElement(recordBodyReader));

                if (recordBodyReader.BaseStream.Position >= recordBodyReader.BaseStream.Length || MaximumExceptionsReached)
                    break;
            }

            return collection;
        }

        // Reads a vector element from the PQDIF file.
        private VectorElement ReadVector(BinaryReader recordBodyReader, PhysicalType typeOfValue)
        {
            VectorElement element = new()
            {
                Size = recordBodyReader.ReadInt32(),
                TypeOfValue = typeOfValue
            };

            byte[] values = recordBodyReader.ReadBytes(element.Size * typeOfValue.GetByteSize());

            element.SetValues(values, 0);

            return element;
        }

        // Reads a scalar element from the PQDIF file.
        private ScalarElement ReadScalar(BinaryReader recordBodyReader, PhysicalType typeOfValue)
        {
            ScalarElement element = new()
            {
                TypeOfValue = typeOfValue
            };

            byte[] value = recordBodyReader.ReadBytes(typeOfValue.GetByteSize());

            element.SetValue(value, 0);

            return element;
        }

#endregion
    }
}
