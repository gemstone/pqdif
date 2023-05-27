//******************************************************************************************************
//  DisturbanceCategory.cs - Gbtc
//
//  Copyright © 2014, Grid Protection Alliance.  All Rights Reserved.
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
//  09/04/2019 - Christoph Lackner
//       Generated original version of source code.
//
//******************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace Gemstone.PQDIF.Logical
{
    /// <summary>
    /// Disturbance Categories (as defined in IEEE 1159).
    /// </summary>
    public static class DisturbanceCategory
    {
        /// <summary>
        /// The ID for no distrubance or undefined.
        /// </summary>
        public static Guid None { get; } = new("67f6af8f-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID for a IEEE 1159 Transient.
        /// </summary>
        public static Guid Transient { get; } = new("67f6af90-f753-0x11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID for a IEEE 1159 Impulsive Transient.
        /// </summary>
        public static Guid ImpulsiveTransient { get; } = new("dd56ef60-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159 Impulsive Transient with nanosecond duration.
        /// </summary>
        public static Guid ImpulsiveTransient_nano { get; } = new("dd56ef61-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159 Impulsive Transient with microsecond duration.
        /// </summary>
        public static Guid ImpulsiveTransient_micro { get; } = new("dd56ef63-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159 Impulsive Transient with millisecond duration.
        /// </summary>
        public static Guid ImpulsiveTransient_milli { get; } = new("dd56ef64-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159 Oscillatory Transient.
        /// </summary>
        public static Guid OscillatoryTransient { get; } = new("dd56ef65-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159 Low Frequency Oscillatory Transient.
        /// </summary>
        public static Guid OscillatoryTransient_low { get; } = new("dd56ef66-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159 Medium Frequency Oscillatory Transient.
        /// </summary>
        public static Guid OscillatoryTransient_medium { get; } = new("dd56ef67-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159 High Frequency Oscillatory Transient.
        /// </summary>
        public static Guid OscillatoryTransient_high { get; } = new("dd56ef68-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159 Short Duration RMS Variation
        /// </summary>n
        public static Guid RMSVariationShortDuration { get; } = new("67f6af91-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID for a IEEE 1159 Short Duration RMS Variation - Instantaneous duration.
        /// </summary>
        public static Guid RMSVariationShortDuration_Instantaneous { get; } = new("dd56ef69-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159 Short Duration RMS Variation - Instantaneous Sag.
        /// </summary>
        public static Guid RMSVariationShortDuration_InstantaneousSag { get; } = new("dd56ef6a-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159 Short Duration RMS Variation - Instantaneous Swell.
        /// </summary>
        public static Guid RMSVariationShortDuration_InstantaneousSwell { get; } = new("dd56ef6b-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159 Short Duration RMS Variation - Momentary Duration.
        /// </summary>
        public static Guid RMSVariationShortDuration_Momentary { get; } = new("dd56ef6c-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159 Short Duration RMS Variation - Momentary Interruption.
        /// </summary>
        public static Guid RMSVariationShortDuration_MomentaryInterruption { get; } = new("dd56ef6d-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159 Short Duration RMS Variation - Momentary Sag.
        /// </summary>
        public static Guid RMSVariationShortDuration_MomentarySag { get; } = new("dd56ef6e-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159 Short Duration RMS Variation - Momentary Swell.
        /// </summary>
        public static Guid RMSVariationShortDuration_MomentarySwell { get; } = new("dd56ef6f-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159Short Duration RMS Variation - Temporary Duration.
        /// </summary>
        public static Guid RMSVariationShortDuration_Temporary { get; } = new("dd56ef70-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159 Short Duration RMS Variation - Temporary Interruption.
        /// </summary>
        public static Guid RMSVariationShortDuration_TemporaryInterruption { get; } = new("dd56ef71-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159 Short Duration RMS Variation - Temporary Sag.
        /// </summary>
        public static Guid RMSVariationShortDuration_TemporarySag { get; } = new("dd56ef72-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159 Short Duration RMS Variation - Temporary Swell.
        /// </summary>
        public static Guid RMSVariationShortDuration_TemporarySwell { get; } = new("dd56ef73-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159  Long Duration RMS Variation.
        /// </summary>
        public static Guid RMSVariationLongDuration { get; } = new("67f6af92-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID for a IEEE 1159 Long Duration RMS Variation - Interruption.
        /// </summary>
        public static Guid RMSVariationLongDuration_Interrruption { get; } = new("dd56ef74-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159 Long Duration RMS Variation - Undervoltage.
        /// </summary>
        public static Guid RMSVariationLongDuration_UnderVoltage { get; } = new("dd56ef75-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159 Long Duration RMS Variation - Overvoltage.
        /// </summary>
        public static Guid RMSVariationLongDuration_OverVoltage { get; } = new("dd56ef76-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159 Imbalance.
        /// </summary>
        public static Guid Imbalance { get; } = new("dd56ef77-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159 Power Frequency Variation.
        /// </summary>
        public static Guid PowerFrequencyVariation { get; } = new("dd56ef7e-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159 Voltage Fluctuation.
        /// </summary>
        public static Guid VoltageFluctuation { get; } = new("67f6af93-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID for a IEEE 1159 Waveform Distortion.
        /// </summary>
        public static Guid WaveformDistortion { get; } = new("67f6af94-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID for a IEEE 1159 DC offset of voltage or current waveform.
        /// </summary>
        public static Guid DCoffset { get; } = new("dd56ef78-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159 Waveform Harmonics Present.
        /// </summary>
        public static Guid WaveformHarmonics { get; } = new("dd56ef79-7edd-11d2-b30a-00609789d193");

        /// <summary>
        /// The ID for a IEEE 1159 Waveform Interharmonics Present.
        /// </summary>

        public static Guid WaveformInterHarmonics { get; } = new("dd56ef7a-7edd-11d2-b30a-00609789d193");
        /// <summary>
        /// The ID for a IEEE 1159 Waveform Notching Present.
        /// </summary>
        public static Guid WaveformNotching { get; } = new("67f6af95-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID for a IEEE 1159 Waveform Noise Present.
        /// </summary>
        public static Guid WaveformNoise { get; } = new("67f6af96-f753-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Gets information about the Disturbance identified by the given ID.
        /// </summary>
        /// <param name="disturbanceCategoryID">Globally unique identifier for the Disturbance Category.</param>
        /// <returns>The information about the vendor.</returns>
        public static Identifier? GetInfo(Guid disturbanceCategoryID) => 
            DisturbanceLookup.TryGetValue(disturbanceCategoryID, out Identifier? identifier) ? identifier : null;

        /// <summary>
        /// Converts the given Disturbance ID to a string containing the name of the Disturbance.
        /// </summary>
        /// <param name="disturbanceCategoryID">The ID of the Disturbance to be converted to a string.</param>
        /// <returns>A string containing the name of the Disturbance Category with the given ID.</returns>
        public static string ToString(Guid disturbanceCategoryID) =>
            GetInfo(disturbanceCategoryID)?.Name ?? disturbanceCategoryID.ToString();

        private static Dictionary<Guid, Identifier> DisturbanceLookup
        {
            get
            {
                Tag? disturbanceTag = Tag.GetTag(ObservationRecord.DisturbanceCategoryTag);

                if (s_disturbanceTag != disturbanceTag)
                {
                    s_disturbanceTag = disturbanceTag;
                    s_disturbanceLookup = disturbanceTag?.ValidIdentifiers.ToDictionary(id => Guid.Parse(id.Value));
                }

                return s_disturbanceLookup ?? new Dictionary<Guid, Identifier>();
            }
        }

        private static Tag? s_disturbanceTag;
        private static Dictionary<Guid, Identifier>? s_disturbanceLookup;
    }
}
