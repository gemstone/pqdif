//******************************************************************************************************
//  RecordHeader.cs - Gbtc
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
//  04/05/2012 - Mehulbhai Thakkar, Grid Protection Alliance
//       Generated original version of source code.
//  12/17/2012 - Starlynn Danyelle Gilliam
//       Modified Header.
//
//******************************************************************************************************

using System;
using System.Text;

namespace Gemstone.PQDIF.Physical
{
    /// <summary>
    /// The header of a PQDIF <see cref="Record"/>. The header is part of
    /// the physical structure of a PQDIF file, and contains information
    /// on how to parse the <see cref="RecordBody"/> as well as how to find
    /// the next record.
    /// </summary>
    public class RecordHeader
    {
        #region [ Constructors ]

        /// <summary>
        /// Creates a new instance of the <see cref="RecordHeader"/> class.
        /// </summary>
        public RecordHeader()
        {
            RecordSignature = Record.Signature;
            Reserved = new byte[16];
        }

        /// <summary>
        /// Creates a new instance of the <see cref="RecordHeader"/> class.
        /// </summary>
        /// <param name="recordTypeTag">The tag which identifies the type of the record.</param>
        public RecordHeader(Guid recordTypeTag) : this() =>
            RecordTypeTag = recordTypeTag;

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets the position of this record.
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Gets or sets the record's globally unique identifier.
        /// </summary>
        public Guid RecordSignature { get; set; }

        /// <summary>
        /// Gets or sets the type of the record which determines
        /// the logical structure of the record.
        /// </summary>
        public RecordType TypeOfRecord => Record.GetRecordType(RecordTypeTag);

        /// <summary>
        /// Gets or sets the tag which identifies the type of the record.
        /// </summary>
        public Guid RecordTypeTag { get; set; }

        /// <summary>
        /// Gets or sets the size of the header, in bytes.
        /// </summary>
        public int HeaderSize { get; set; }

        /// <summary>
        /// Gets or sets the size of the body, in bytes.
        /// </summary>
        public int BodySize { get; set; }

        /// <summary>
        /// Gets or sets the position of the next record in the PQDIF file.
        /// This value is a byte offset relative to the beginning of the file.
        /// </summary>
        public int NextRecordPosition { get; set; }

        /// <summary>
        /// Optional checksum (such as a 32-bit CRC)
        /// of the record body to verify decompression.
        /// </summary>
        public uint Checksum { get; set; }

        /// <summary>
        /// Reserved to fill structure to 64 bytes. Should be filled with 0.
        /// </summary>
        public byte[] Reserved { get; set; }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Returns a string representation of the record header.
        /// </summary>
        /// <returns>A string representation of the record header.</returns>
        public override string ToString()
        {
            StringBuilder builder = new();

            builder.AppendFormat("Record type: {0}", TypeOfRecord);
            builder.AppendLine();
            builder.AppendFormat("Header size: {0}", HeaderSize);
            builder.AppendLine();
            builder.AppendFormat("Body size: {0}", BodySize);

            return builder.ToString();
        }

        #endregion
    }
}
