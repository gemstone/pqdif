﻿//******************************************************************************************************
//  SeriesDefinition.cs - Gbtc
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
//  05/04/2012 - Stephen C. Wills, Grid Protection Alliance
//       Generated original version of source code.
//  12/17/2012 - Starlynn Danyelle Gilliam
//       Modified Header.
//
//******************************************************************************************************

using System;
using System.IO;
using System.Linq;
using System.Text;
using Gemstone.PQDIF.Physical;

namespace Gemstone.PQDIF.Logical
{
    #region [ Enumerations ]

    /// <summary>
    /// Defines flags that determine the how the
    /// data is stored in a series instance.
    /// </summary>
    [Flags]
    public enum StorageMethods : uint
    {
        /// <summary>
        /// Straight array of data points.
        /// </summary>
        Values = 0x1u,
        
        /// <summary>
        /// Data values are scaled.
        /// </summary>
        Scaled = 0x2u,

        /// <summary>
        /// Start, count, and increment are stored and
        /// the series is recreated from those values.
        /// </summary>
        Increment = 0x4u
    }

    /// <summary>
    /// Units of data defined in a PQDIF file.
    /// </summary>
    public enum QuantityUnits : uint
    {
        /// <summary>
        /// Unitless.
        /// </summary>
        None = 0u,

        /// <summary>
        /// Absolute time. Each timestamp in the series must be in absolute
        /// time using the <see cref="PhysicalType.Timestamp"/> type.
        /// </summary>
        Timestamp = 1u,

        /// <summary>
        /// Seconds relative to the start time of an observation.
        /// </summary>
        /// <seealso cref="ObservationRecord.StartTime"/>
        Seconds = 2u,

        /// <summary>
        /// Cycles relative to the start time of an observation.
        /// </summary>
        /// <seealso cref="ObservationRecord.StartTime"/>
        Cycles = 3u,

        /// <summary>
        /// Volts.
        /// </summary>
        Volts = 6u,

        /// <summary>
        /// Amperes.
        /// </summary>
        Amps = 7u,

        /// <summary>
        /// Volt-amperes.
        /// </summary>
        VoltAmps = 8u,

        /// <summary>
        /// Watts.
        /// </summary>
        Watts = 9u,

        /// <summary>
        /// Volt-amperes reactive.
        /// </summary>
        Vars = 10u,

        /// <summary>
        /// Ohms.
        /// </summary>
        Ohms = 11u,

        /// <summary>
        /// Siemens.
        /// </summary>
        Siemens = 12u,

        /// <summary>
        /// Volts per ampere.
        /// </summary>
        VoltsPerAmp = 13u,

        /// <summary>
        /// Joules.
        /// </summary>
        Joules = 14u,

        /// <summary>
        /// Hertz.
        /// </summary>
        Hertz = 15u,

        /// <summary>
        /// Celcius.
        /// </summary>
        Celcius = 16u,

        /// <summary>
        /// Degrees of arc.
        /// </summary>
        Degrees = 17u,

        /// <summary>
        /// Decibels.
        /// </summary>
        Decibels = 18u,

        /// <summary>
        /// Percent.
        /// </summary>
        Percent = 19u,

        /// <summary>
        /// Per-unit.
        /// </summary>
        PerUnit = 20u,

        /// <summary>
        /// Number of counts or samples.
        /// </summary>
        Samples = 21u,

        /// <summary>
        /// Energy in var-hours.
        /// </summary>
        VarHours = 22u,

        /// <summary>
        /// Energy in watt-hours.
        /// </summary>
        WattHours = 23u,

        /// <summary>
        /// Energy in VA-hours.
        /// </summary>
        VoltAmpHours = 24u,

        /// <summary>
        /// Meters/second.
        /// </summary>
        MetersPerSecond = 25u,

        /// <summary>
        /// Miles/hour.
        /// </summary>
        MilesPerHour = 26u,

        /// <summary>
        /// Pressure in bars.
        /// </summary>
        Bars = 27u,

        /// <summary>
        /// Pressure in pascals.
        /// </summary>
        Pascals = 28u,

        /// <summary>
        /// Force in newtons.
        /// </summary>
        Newtons = 29u,

        /// <summary>
        /// Torque in newton-meters.
        /// </summary>
        NewtonMeters = 30u,

        /// <summary>
        /// Revolutions/minute.
        /// </summary>
        RevolutionsPerMinute = 31u,

        /// <summary>
        /// Radians/second.
        /// </summary>
        RadiansPerSecond = 32u,

        /// <summary>
        /// Meters.
        /// </summary>
        Meters = 33u,

        /// <summary>
        /// Flux linkage in Weber Turns.
        /// </summary>
        WeberTurns = 34u,

        /// <summary>
        /// Flux density in teslas.
        /// </summary>
        Teslas = 35u,

        /// <summary>
        /// Magnetic field in webers.
        /// </summary>
        Webers = 36u,

        /// <summary>
        /// Volts/volt transfer function.
        /// </summary>
        VoltsPerVolt = 37u,

        /// <summary>
        /// Amps/amp transfer function.
        /// </summary>
        AmpsPerAmp = 38u,
        
        /// <summary>
        /// Impedance transfer function.
        /// </summary>
        AmpsPerVolt = 39u
    }

    #endregion

    /// <summary>
    /// Definition of a <see cref="SeriesInstance"/>.
    /// </summary>
    public class SeriesDefinition : IEquatable<SeriesDefinition>
    {
        #region [ Constructors ]

        /// <summary>
        /// Creates a new instance of the <see cref="SeriesDefinition"/> class.
        /// </summary>
        /// <param name="physicalStructure">The collection that is the physical structure of the series definition.</param>
        /// <param name="channelDefinition">The channel definition in which the series definition resides.</param>
        public SeriesDefinition(CollectionElement physicalStructure, ChannelDefinition channelDefinition)
        {
            PhysicalStructure = physicalStructure;
            ChannelDefinition = channelDefinition;
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets the physical structure of the series definition.
        /// </summary>
        public CollectionElement PhysicalStructure { get; }

        /// <summary>
        /// Gets the channel definition in which the series definition resides.
        /// </summary>
        public ChannelDefinition ChannelDefinition { get; }

        /// <summary>
        /// Gets the value type ID of the series.
        /// </summary>
        /// <exception cref="InvalidDataException">ValueTypeID element not found in series definition.</exception>
        /// <seealso cref="SeriesValueType"/>
        public Guid ValueTypeID
        {
            get
            {
                ScalarElement valueTypeIDElement = PhysicalStructure.GetScalarByTag(ValueTypeIDTag)
                    ?? throw new InvalidDataException("ValueTypeID element not found in series definition.");

                return valueTypeIDElement.GetGuid();
            }
            set
            {
                ScalarElement valueTypeIDElement = PhysicalStructure.GetOrAddScalar(ValueTypeIDTag);
                valueTypeIDElement.TypeOfValue = PhysicalType.Guid;
                valueTypeIDElement.SetGuid(value);
            }
        }

        /// <summary>
        /// Gets the units of the data in the series.
        /// </summary>
        /// <exception cref="InvalidDataException">QuantityUnits element not found in series definition.</exception>
        public QuantityUnits QuantityUnits
        {
            get
            {
                ScalarElement quantityUnitsElement = PhysicalStructure.GetScalarByTag(QuantityUnitsIDTag)
                    ?? throw new InvalidDataException("QuantityUnits element not found in series definition.");

                return (QuantityUnits)quantityUnitsElement.GetUInt4();
            }
            set
            {
                ScalarElement quantityUnitsIDElement = PhysicalStructure.GetOrAddScalar(QuantityUnitsIDTag);
                quantityUnitsIDElement.TypeOfValue = PhysicalType.UnsignedInteger4;
                quantityUnitsIDElement.SetUInt4((uint)value);
            }
        }

        /// <summary>
        /// Gets additional detail about the meaning of the series data.
        /// </summary>
        /// <exception cref="InvalidDataException">QuantityCharacteristicID element not found in series definition.</exception>
        public Guid QuantityCharacteristicID
        {
            get
            {
                ScalarElement quantityCharacteristicIDElement = PhysicalStructure.GetScalarByTag(QuantityCharacteristicIDTag)
                    ?? throw new InvalidDataException("QuantityCharacteristicID element not found in series definition.");

                return quantityCharacteristicIDElement.GetGuid();
            }
            set
            {
                ScalarElement quantityCharacteristicIDElement = PhysicalStructure.GetOrAddScalar(QuantityCharacteristicIDTag);
                quantityCharacteristicIDElement.TypeOfValue = PhysicalType.Guid;
                quantityCharacteristicIDElement.SetGuid(value);
            }
        }

        /// <summary>
        /// Gets the storage method ID, which can be used with
        /// <see cref="StorageMethods"/> to determine how the data is stored.
        /// </summary>
        /// <exception cref="InvalidDataException">StorageMethodID element not found in series definition.</exception>
        public StorageMethods StorageMethodID
        {
            get
            {
                ScalarElement storageMethodIDElement = PhysicalStructure.GetScalarByTag(StorageMethodIDTag)
                    ?? throw new InvalidDataException("StorageMethodID element not found in series definition.");

                return (StorageMethods)storageMethodIDElement.GetUInt4();
            }
            set
            {
                ScalarElement storageMethodIDElement = PhysicalStructure.GetOrAddScalar(StorageMethodIDTag);
                storageMethodIDElement.TypeOfValue = PhysicalType.UnsignedInteger4;
                storageMethodIDElement.SetUInt4((uint)value);
            }
        }

        /// <summary>
        /// Gets the value type name of the series.
        /// </summary>
        public string? ValueTypeName
        {
            get
            {
                VectorElement? valueTypeNameElement = PhysicalStructure.GetVectorByTag(ValueTypeNameTag);

                if (valueTypeNameElement is null)
                    return null;

                return Encoding.ASCII.GetString(valueTypeNameElement.GetValues()).Trim((char)0);
            }
            set
            {
                byte[] bytes = Encoding.ASCII.GetBytes(value + (char)0);
                PhysicalStructure.AddOrUpdateVector(ValueTypeNameTag, PhysicalType.Char1, bytes);
            }
        }

        /// <summary>
        /// Gets the nominal quantity of the series.
        /// </summary>
        public double SeriesNominalQuantity
        {
            get
            {
                ScalarElement? seriesNominalQuantityElement = PhysicalStructure.GetScalarByTag(SeriesNominalQuantityTag);

                if (seriesNominalQuantityElement is null)
                    return 0.0D;

                return seriesNominalQuantityElement.GetReal8();
            }
            set
            {
                ScalarElement seriesNominalQuantityElement = PhysicalStructure.GetOrAddScalar(SeriesNominalQuantityTag);
                seriesNominalQuantityElement.TypeOfValue = PhysicalType.Real8;
                seriesNominalQuantityElement.SetReal8(value);
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Determines whether an element identified by the
        /// given tag exists in this object's physical structure.
        /// </summary>
        /// <param name="tag">The tag of the element to search for.</param>
        /// <returns>True if the element exists; false otherwise.</returns>
        public bool HasElement(Guid tag) =>
            PhysicalStructure.GetElementsByTag(tag).Any();

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
        public bool Equals(SeriesDefinition? other) =>
            ReferenceEquals(PhysicalStructure, other?.PhysicalStructure);

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The object to compare with the current object. </param>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object? obj) => 
            obj is SeriesDefinition other && Equals(other);

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>A hash code for the current <see cref="T:System.Object"/>.</returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode() =>
            PhysicalStructure.GetHashCode();

        #endregion

        #region [ Static ]

        // Static Fields

        /// <summary>
        /// Tag that identifies the value type ID of the series.
        /// </summary>
        public static Guid ValueTypeIDTag { get; } = new("b48d859c-f5f5-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the quantity units ID of the series.
        /// </summary>
        public static Guid QuantityUnitsIDTag { get; } = new("b48d859b-f5f5-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the characteristic ID of the series.
        /// </summary>
        public static Guid QuantityCharacteristicIDTag { get; } = new("3d786f9e-f76e-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the storage method ID of the series.
        /// </summary>
        public static Guid StorageMethodIDTag { get; } = new("b48d85a1-f5f5-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the value type name of the series.
        /// </summary>
        public static Guid ValueTypeNameTag { get; } = new("b48d859d-f5f5-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the nominal quantity of the series.
        /// </summary>
        public static Guid SeriesNominalQuantityTag { get; } = new("0fa118c8-cb4a-11d2-b30b-fe25cb9a1760");

        #endregion
    }
}
