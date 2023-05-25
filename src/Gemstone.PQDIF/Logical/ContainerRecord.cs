//******************************************************************************************************
//  ContainerRecord.cs - Gbtc
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
using System.IO;
using System.Text;
using Gemstone.PQDIF.Physical;

namespace Gemstone.PQDIF.Logical
{
    /// <summary>
    /// Represents the container record in a PQDIF file. There can be only
    /// one container record in a PQDIF file, and it is the first physical
    /// <see cref="Record"/>.
    /// </summary>
    public class ContainerRecord
    {
        #region [ Constructors ]

        /// <summary>
        /// Creates a new instance of the <see cref="ContainerRecord"/> class.
        /// </summary>
        /// <param name="physicalRecord">The physical structure of the container record.</param>
        private ContainerRecord(Record physicalRecord)
        {
            PhysicalRecord = physicalRecord;
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets the physical structure of the container record.
        /// </summary>
        public Record PhysicalRecord { get; }

        /// <summary>
        /// Gets the major version number of the PQDIF file writer.
        /// </summary>
        /// <exception cref="InvalidDataException">WriterMajorVersion element not found in container record.</exception>
        public uint WriterMajorVersion
        {
            get
            {
                VectorElement writerMajorVersionElement = PhysicalRecord.Body.Collection.GetVectorByTag(VersionInfoTag)
                    ?? throw new InvalidDataException("WriterMajorVersion element not found in container record.");

                return writerMajorVersionElement.GetUInt4(0);
            }
            set
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                VectorElement versionInfoElement = collectionElement.GetOrAddVector(VersionInfoTag);
                versionInfoElement.TypeOfValue = PhysicalType.UnsignedInteger4;
                versionInfoElement.Size = 4;
                versionInfoElement.SetUInt4(0, value);
            }
        }

        /// <summary>
        /// Gets the minor version number of the PQDIF file writer.
        /// </summary>
        /// <exception cref="InvalidDataException">WriterMinorVersion element not found in container record.</exception>
        public uint WriterMinorVersion
        {
            get
            {
                VectorElement writerMinorVersionElement = PhysicalRecord.Body.Collection.GetVectorByTag(VersionInfoTag)
                    ?? throw new InvalidDataException("WriterMinorVersion element not found in container record.");

                return writerMinorVersionElement.GetUInt4(1);
            }
            set
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                VectorElement versionInfoElement = collectionElement.GetOrAddVector(VersionInfoTag);
                versionInfoElement.TypeOfValue = PhysicalType.UnsignedInteger4;
                versionInfoElement.Size = 4;
                versionInfoElement.SetUInt4(1, value);
            }
        }

        /// <summary>
        /// Gets the compatible major version that the file can be read as.
        /// </summary>
        /// <exception cref="InvalidDataException">CompatibleMajorVersion element not found in container record.</exception>
        public uint CompatibleMajorVersion
        {
            get
            {
                VectorElement compatibleMajorVersionElement = PhysicalRecord.Body.Collection.GetVectorByTag(VersionInfoTag)
                    ?? throw new InvalidDataException("CompatibleMajorVersion element not found in container record.");

                return compatibleMajorVersionElement.GetUInt4(2);
            }
            set
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                VectorElement versionInfoElement = collectionElement.GetOrAddVector(VersionInfoTag);
                versionInfoElement.TypeOfValue = PhysicalType.UnsignedInteger4;
                versionInfoElement.Size = 4;
                versionInfoElement.SetUInt4(2, value);
            }
        }

        /// <summary>
        /// Gets the compatible minor version that the file can be read as.
        /// </summary>
        /// <exception cref="InvalidDataException">CompatibleMinorVersion element not found in container record.</exception>
        public uint CompatibleMinorVersion
        {
            get
            {
                VectorElement compatibleMinorVersionElement = PhysicalRecord.Body.Collection.GetVectorByTag(VersionInfoTag)
                    ?? throw new InvalidDataException("CompatibleMinorVersion element not found in container record.");

                return compatibleMinorVersionElement.GetUInt4(3);
            }
            set
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                VectorElement versionInfoElement = collectionElement.GetOrAddVector(VersionInfoTag);
                versionInfoElement.TypeOfValue = PhysicalType.UnsignedInteger4;
                versionInfoElement.Size = 4;
                versionInfoElement.SetUInt4(3, value);
            }
        }

        /// <summary>
        /// Gets the name of the file at the time the file was written.
        /// </summary>
        /// <exception cref="InvalidDataException">FileName element not found in container record.</exception>
        public string FileName
        {
            get
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;

                VectorElement fileNameElement = collectionElement.GetVectorByTag(FileNameTag)
                    ?? throw new InvalidDataException("FileName element not found in container record.");

                return Encoding.ASCII.GetString(fileNameElement.GetValues()).Trim((char)0);
            }
            set
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                byte[] bytes = Encoding.ASCII.GetBytes(value + (char)0);
                collectionElement.AddOrUpdateVector(FileNameTag, PhysicalType.Char1, bytes);
            }
        }

        /// <summary>
        /// Gets the date and time of file creation.
        /// </summary>
        /// <exception cref="InvalidDataException">Creation element not found in container record.</exception>
        public DateTime Creation
        {
            get
            {
                ScalarElement creationElement = PhysicalRecord.Body.Collection.GetScalarByTag(CreationTag)
                    ?? throw new InvalidDataException("Creation element not found in container record.");

                return creationElement.GetTimestamp();
            }
            set
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                ScalarElement creationElement = collectionElement.GetOrAddScalar(CreationTag);
                creationElement.TypeOfValue = PhysicalType.Timestamp;
                creationElement.SetTimestamp(value);
            }
        }

        /// <summary>
        /// Gets the title applied to the file.
        /// </summary>
        public string? Title
        {
            get
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                VectorElement? titleElement = collectionElement.GetVectorByTag(TitleTag);

                if (titleElement is null)
                    return null;

                return Encoding.ASCII.GetString(titleElement.GetValues()).Trim((char)0);
            }
            set
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                byte[] bytes = Encoding.ASCII.GetBytes(value + (char)0);
                collectionElement.AddOrUpdateVector(TitleTag, PhysicalType.Char1, bytes);
            }
        }

        /// <summary>
        /// Gets the subject applied to the file.
        /// </summary>
        public string? Subject
        {
            get
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                VectorElement? subjectElement = collectionElement.GetVectorByTag(SubjectTag);

                if (subjectElement is null)
                    return null;

                return Encoding.ASCII.GetString(subjectElement.GetValues()).Trim((char)0);
            }
            set
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                byte[] bytes = Encoding.ASCII.GetBytes(value + (char)0);
                collectionElement.AddOrUpdateVector(SubjectTag, PhysicalType.Char1, bytes);
            }
        }

        /// <summary>
        /// Gets the notes stored in the file.
        /// </summary>
        public string? Notes
        {
            get
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                VectorElement? notesElement = collectionElement.GetVectorByTag(NotesTag);

                if (notesElement is null)
                    return null;

                return Encoding.ASCII.GetString(notesElement.GetValues()).Trim((char)0);
            }
            set
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                byte[] bytes = Encoding.ASCII.GetBytes(value + (char)0);
                collectionElement.AddOrUpdateVector(NotesTag, PhysicalType.Char1, bytes);
            }
        }

        /// <summary>
        /// Gets the style of compression used to compress the PQDIF file.
        /// </summary>
        public CompressionStyle CompressionStyle
        {
            get
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                ScalarElement? compressionStyleElement = collectionElement.GetScalarByTag(CompressionStyleTag);
                uint compressionStyleID = (uint)CompressionStyle.None;

                if (compressionStyleElement is not null)
                    compressionStyleID = compressionStyleElement.GetUInt4();

                return (CompressionStyle)compressionStyleID;
            }
            set
            {
                CollectionElement collection = PhysicalRecord.Body.Collection;
                ScalarElement compressionStyleElement = collection.GetOrAddScalar(CompressionStyleTag);
                compressionStyleElement.TypeOfValue = PhysicalType.UnsignedInteger4;
                compressionStyleElement.SetUInt4((uint)value);
            }
        }

        /// <summary>
        /// Gets the compression algorithm used to compress the PQDIF file.
        /// </summary>
        public CompressionAlgorithm CompressionAlgorithm
        {
            get
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                ScalarElement? compressionAlgorithmElement = collectionElement.GetScalarByTag(CompressionAlgorithmTag);
                uint compressionAlgorithmID = (uint)CompressionAlgorithm.None;

                if (compressionAlgorithmElement is not null)
                    compressionAlgorithmID = compressionAlgorithmElement.GetUInt4();

                return (CompressionAlgorithm)compressionAlgorithmID;
            }
            set
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                ScalarElement compressionAlgorithmElement = collectionElement.GetOrAddScalar(CompressionAlgorithmTag);
                compressionAlgorithmElement.TypeOfValue = PhysicalType.UnsignedInteger4;
                compressionAlgorithmElement.SetUInt4((uint)value);
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Removes the element identified by the given tag from the record.
        /// </summary>
        /// <param name="tag">The tag of the element to be removed.</param>
        public void RemoveElement(Guid tag) => 
            PhysicalRecord.Body.Collection.RemoveElementsByTag(tag);

        #endregion

        #region [ Static ]

        // Static Fields

        /// <summary>
        /// Tag that identifies the version info.
        /// </summary>
        public static Guid VersionInfoTag { get; } = new("89738607-f1c3-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the file name.
        /// </summary>
        public static Guid FileNameTag { get; } = new("89738608-f1c3-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the date and time of creation.
        /// </summary>
        public static Guid CreationTag { get; } = new("89738609-f1c3-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the title applied to the PQDIF file.
        /// </summary>
        public static readonly Guid TitleTag = new("8973860d-f1c3-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the subject applied to the PQDIF file.
        /// </summary>
        public static readonly Guid SubjectTag = new("8973860e-f1c3-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the notes stored in the PQDIF file.
        /// </summary>
        public static Guid NotesTag { get; } = new("89738617-f1c3-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the compression style of the PQDIF file.
        /// </summary>
        public static Guid CompressionStyleTag { get; } = new("8973861b-f1c3-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the compression algorithm used when writing the PQDIF file.
        /// </summary>
        public static Guid CompressionAlgorithmTag { get; } = new("8973861c-f1c3-11cf-9d89-0080c72e70a3");

        // Static Methods

        /// <summary>
        /// Creates a new container record from scratch.
        /// </summary>
        /// <returns>The new container record.</returns>
        public static ContainerRecord CreateContainerRecord()
        {
            Guid recordTypeTag = Record.GetTypeAsTag(RecordType.Container);
            Record physicalRecord = new(recordTypeTag);
            ContainerRecord containerRecord = new(physicalRecord);

            DateTime now = DateTime.UtcNow;
            containerRecord.WriterMajorVersion = 1;
            containerRecord.WriterMinorVersion = 5;
            containerRecord.CompatibleMajorVersion = 1;
            containerRecord.CompatibleMinorVersion = 0;
            containerRecord.FileName = $"{now:yyyy-MM-dd_HH.mm.ss}.pqd";
            containerRecord.Creation = now;

            return containerRecord;
        }

        /// <summary>
        /// Creates a new container record from the given physical record
        /// if the physical record is of type container. Returns null if
        /// it is not.
        /// </summary>
        /// <param name="physicalRecord">The physical record used to create the container record.</param>
        /// <returns>The new container record, or null if the physical record does not define a container record.</returns>
        public static ContainerRecord? CreateContainerRecord(Record physicalRecord)
        {
            bool isValidPhysicalRecord = physicalRecord.Header.TypeOfRecord == RecordType.Container;
            return isValidPhysicalRecord ? new ContainerRecord(physicalRecord) : null;
        }

        #endregion
    }
}
