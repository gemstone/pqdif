//******************************************************************************************************
//  SeriesValueType.cs - Gbtc
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
//  05/08/2012 - Stephen C. Wills, Grid Protection Alliance
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
    /// Defines tags used to identify different series value types.
    /// </summary>
    public static class SeriesValueType
    {
        /// <summary>
        /// Value type for a measurement.
        /// </summary>
        public static Guid Val { get; } = new("67f6af97-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Time.
        /// </summary>
        public static Guid Time { get; } = new("c690e862-f755-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Minimum.
        /// </summary>
        public static Guid Min { get; } = new("67f6af98-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Maximum.
        /// </summary>
        public static Guid Max { get; } = new("67f6af99-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Average.
        /// </summary>
        public static Guid Avg { get; } = new("67f6af9a-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Instantaneous.
        /// </summary>
        public static Guid Inst { get; } = new("67f6af9b-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Phase angle.
        /// </summary>
        public static Guid PhaseAngle { get; } = new("3d786f9d-f76e-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Phase angle which corresponds to a <see cref="Min"/> series.
        /// </summary>
        public static Guid PhaseAngleMin { get; } = new("dc762340-3c56-11d2-ae44-0060083a2628");

        /// <summary>
        /// Phase angle which corresponds to a <see cref="Max"/> series.
        /// </summary>
        public static Guid PhaseAngleMax { get; } = new("dc762341-3c56-11d2-ae44-0060083a2628");

        /// <summary>
        /// Phase angle which corresponds to an <see cref="Avg"/> series.
        /// </summary>
        public static Guid PhaseAngleAvg { get; } = new("dc762342-3c56-11d2-ae44-0060083a2628");

        /// <summary>
        /// Area under the signal, usually an rms voltage, current, or other quantity.
        /// </summary>
        public static Guid Area { get; } = new("c7825ce0-8ace-11d3-b92f-0050da2b1f4d");

        /// <summary>
        /// Latitude.
        /// </summary>
        public static Guid Latitude { get; } = new("c690e864-f755-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Duration.
        /// </summary>
        public static Guid Duration { get; } = new("c690e863-f755-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Longitude.
        /// </summary>
        public static Guid Longitude { get; } = new("c690e865-f755-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Polarity.
        /// </summary>
        public static Guid Polarity { get; } = new("c690e866-f755-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Ellipse (for lightning flash density).
        /// </summary>
        public static Guid Ellipse { get; } = new("c690e867-f755-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// BinID.
        /// </summary>
        public static Guid BinID { get; } = new("c690e869-f755-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// BinHigh.
        /// </summary>
        public static Guid BinHigh { get; } = new("c690e86a-f755-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// BinLow.
        /// </summary>
        public static Guid BinLow { get; } = new("c690e86b-f755-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// XBinHigh.
        /// </summary>
        public static Guid XBinHigh { get; } = new("c690e86c-f755-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// XBinLow.
        /// </summary>
        public static Guid XBinLow { get; } = new("c690e86d-f755-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// YBinHigh.
        /// </summary>
        public static Guid YBinHigh { get; } = new("c690e86e-f755-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// YBinLow.
        /// </summary>
        public static Guid YBinLow { get; } = new("c690e86f-f755-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Count.
        /// </summary>
        public static Guid Count { get; } = new("c690e870-f755-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Transition event code series.
        /// </summary>
        /// <remarks>
        /// This series contains codes corresponding to values in a value
        /// series that indicates what kind of transition caused the event
        /// to be recorded. Used only with VALUELOG data.
        /// </remarks>
        public static Guid Transition { get; } = new("5369c260-c347-11d2-923f-00104b2b84b1");

        /// <summary>
        /// Cumulative probability in percent.
        /// </summary>
        public static Guid Prob { get; } = new("6763cc71-17d6-11d4-9f1c-002078e0b723");

        /// <summary>
        /// Interval data.
        /// </summary>
        public static Guid Interval { get; } = new("72e82a40-336c-11d5-a4b3-444553540000");

        /// <summary>
        /// Status data.
        /// </summary>
        public static Guid Status { get; } = new("b82b5c82-55c7-11d5-a4b3-444553540000");

        /// <summary>
        /// Probability: 1%.
        /// </summary>
        public static Guid P1 { get; } = new("67f6af9c-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Probability: 5%.
        /// </summary>
        public static Guid P5 { get; } = new("67f6af9d-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Probability: 10%.
        /// </summary>
        public static Guid P10 { get; } = new("67f6af9e-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Probability: 90%.
        /// </summary>
        public static Guid P90 { get; } = new("67f6af9f-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Probability: 95%.
        /// </summary>
        public static Guid P95 { get; } = new("c690e860-f755-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Probability: 99%.
        /// </summary>
        public static Guid P99 { get; } = new("c690e861-f755-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Frequency.
        /// </summary>
        public static Guid Frequency { get; } = new("c690e868-f755-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Gets information about the series value type identified by the given ID.
        /// </summary>
        /// <param name="seriesValueTypeID">The identifier for the series value type.</param>
        /// <returns>Information about the series value type.</returns>
        public static Identifier? GetInfo(Guid seriesValueTypeID) => 
            SeriesValueTypeLookup.TryGetValue(seriesValueTypeID, out Identifier? identifier) ? identifier : null;

        /// <summary>
        /// Returns the name of the given series value type.
        /// </summary>
        /// <param name="seriesValueTypeID">The GUID tag which identifies the series value type.</param>
        /// <returns>The name of the given series value type.</returns>
        public static string? ToString(Guid seriesValueTypeID) =>
            GetInfo(seriesValueTypeID)?.Name;

        private static Dictionary<Guid, Identifier> SeriesValueTypeLookup
        {
            get
            {
                Tag? seriesValueTypeTag = Tag.GetTag(SeriesDefinition.ValueTypeIDTag);

                if (s_seriesValueTypeTag != seriesValueTypeTag)
                {
                    s_seriesValueTypeTag = seriesValueTypeTag;
                    s_seriesValueTypeLookup = seriesValueTypeTag?.ValidIdentifiers.ToDictionary(id => Guid.Parse(id.Value));
                }

                return s_seriesValueTypeLookup ?? new Dictionary<Guid, Identifier>();
            }
        }

        private static Tag? s_seriesValueTypeTag;
        private static Dictionary<Guid, Identifier>? s_seriesValueTypeLookup;
    }
}
