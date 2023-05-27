//******************************************************************************************************
//  QuantityType.cs - Gbtc
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
//  05/16/2012 - Stephen C. Wills, Grid Protection Alliance
//       Generated original version of source code.
//  12/17/2012 - Starlynn Danyelle Gilliam
//       Modified Header.
//
//******************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace Gemstone.PQDIF.Logical
{
    /// <summary>
    /// The high-level description of the type of
    /// quantity which is being captured by a channel.
    /// </summary>
    public static class QuantityType
    {
        /// <summary>
        /// Point-on-wave measurements.
        /// </summary>
        public static Guid WaveForm { get; } = new("67f6af80-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Time-based logged entries.
        /// </summary>
        public static Guid ValueLog { get; } = new("67f6af82-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Time-domain measurements including
        /// magnitudes and (optionally) phase angle.
        /// </summary>
        public static Guid Phasor { get; } = new("67f6af81-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Frequency-domain measurements including
        /// magnitude and (optionally) phase angle.
        /// </summary>
        public static Guid Response { get; } = new("67f6af85-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Time, latitude, longitude, value, polarity, ellipse.
        /// </summary>
        public static Guid Flash { get; } = new("67f6af83-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// BinLow, BinHigh, BinID, count.
        /// </summary>
        public static Guid Histogram { get; } = new("67f6af87-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// XBinLow, XBinHigh, YBinLow, YBinHigh, BinID, count.
        /// </summary>
        public static Guid Histogram3D { get; } = new("67f6af88-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Probability, value.
        /// </summary>
        public static Guid CPF { get; } = new("67f6af89-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// X-values and y-values.
        /// </summary>
        public static Guid XY { get; } = new("67f6af8a-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Magnitude and duration.
        /// </summary>
        public static Guid MagDur { get; } = new("67f6af8b-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// X-values, y-values, and z-values.
        /// </summary>
        public static Guid XYZ { get; } = new("67f6af8c-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Time, magnitude, and duration.
        /// </summary>
        public static Guid MagDurTime { get; } = new("67f6af8d-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Time, magnitude, duration, and count.
        /// </summary>
        public static Guid MagDurCount { get; } = new("67f6af8e-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Gets the information about the quantity
        /// type identified by the given ID.
        /// </summary>
        /// <param name="quantityTypeID">The quantity type ID.</param>
        /// <returns>Information about the quantity type.</returns>
        public static Identifier? GetInfo(Guid quantityTypeID) => 
            QuantityTypeLookup.TryGetValue(quantityTypeID, out Identifier? identifier) ? identifier : null;

        /// <summary>
        /// Gets the name of the quantity type with the given ID.
        /// </summary>
        /// <param name="quantityTypeID">The ID of the quantity type.</param>
        /// <returns>The name of the quantity type with the given ID.</returns>
        public static string? ToString(Guid quantityTypeID) =>
            GetInfo(quantityTypeID)?.Name;

        /// <summary>
        /// Determines whether the given ID is a quantity type ID.
        /// </summary>
        /// <param name="id">The ID to be tested.</param>
        /// <returns>True if the given ID is a quantity type ID; false otherwise.</returns>
        public static bool IsQuantityTypeID(Guid id) =>
            GetInfo(id) is not null;

        private static Dictionary<Guid, Identifier> QuantityTypeLookup
        {
            get
            {
                Tag? quantityTypeTag = Tag.GetTag(ChannelDefinition.QuantityTypeIDTag);

                if (s_quantityTypeTag != quantityTypeTag)
                {
                    s_quantityTypeTag = quantityTypeTag;
                    s_quantityTypeLookup = quantityTypeTag?.ValidIdentifiers.ToDictionary(id => Guid.Parse(id.Value));
                }

                return s_quantityTypeLookup ?? new Dictionary<Guid, Identifier>();
            }
        }

        private static Tag? s_quantityTypeTag;
        private static Dictionary<Guid, Identifier>? s_quantityTypeLookup;
    }
}
