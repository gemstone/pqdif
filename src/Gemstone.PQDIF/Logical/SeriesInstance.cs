//******************************************************************************************************
//  SeriesInstance.cs - Gbtc
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
using System.Collections.Generic;
using System.IO;
using Gemstone.PQDIF.Physical;

namespace Gemstone.PQDIF.Logical
{
    /// <summary>
    /// Represents an instance of a series in a PQDIF file. A series
    /// instance resides in a <see cref="ChannelInstance"/> and is
    /// defined by a <see cref="SeriesDefinition"/>.
    /// </summary>
    public class SeriesInstance : IEquatable<SeriesInstance>
    {
        #region [ Constructors ]

        /// <summary>
        /// Creates a new instance of the <see cref="SeriesInstance"/> class.
        /// </summary>
        /// <param name="physicalStructure">The physical structure of the series instance.</param>
        /// <param name="channel">The channel instance that this series instance resides in.</param>
        /// <param name="definition">The series definition that defines this series instance.</param>
        public SeriesInstance(CollectionElement physicalStructure, ChannelInstance channel, SeriesDefinition definition)
        {
            PhysicalStructure = physicalStructure;
            Channel = channel;
            Definition = definition;
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets the physical structure of the series instance.
        /// </summary>
        public CollectionElement PhysicalStructure { get; }

        /// <summary>
        /// Gets the channel instance in which the series instance resides.
        /// </summary>
        public ChannelInstance Channel { get; }

        /// <summary>
        /// Gets the series definition that defines the series.
        /// </summary>
        public SeriesDefinition Definition { get; }

        /// <summary>
        /// Gets the value by which to scale the values in
        /// order to restore the original data values.
        /// </summary>
        public ScalarElement? SeriesScale
        {
            get
            {
                return PhysicalStructure.GetScalarByTag(SeriesScaleTag)
                    ?? SeriesShareSeries?.SeriesScale;
            }
            set
            {
                PhysicalStructure.RemoveElementsByTag(SeriesScaleTag);

                if (value is not null)
                {
                    value.TagOfElement = SeriesScaleTag;
                    PhysicalStructure.AddElement(value);
                }
            }
        }

        /// <summary>
        /// Gets the value added to the values in order
        /// to restore the original data values.
        /// </summary>
        public ScalarElement? SeriesOffset
        {
            get
            {
                return PhysicalStructure.GetScalarByTag(SeriesOffsetTag)
                    ?? SeriesShareSeries?.SeriesOffset;
            }
            set
            {
                PhysicalStructure.RemoveElementsByTag(SeriesOffsetTag);

                if (value is not null)
                {
                    value.TagOfElement = SeriesOffsetTag;
                    PhysicalStructure.AddElement(value);
                }
            }
        }

        /// <summary>
        /// Gets the values contained in this series instance.
        /// </summary>
        /// <exception cref="InvalidDataException">SeriesValues element not found in series instance.</exception>
        public VectorElement SeriesValues
        {
            get
            {
                return SeriesShareSeries?.SeriesValues
                    ?? PhysicalStructure.GetVectorByTag(SeriesValuesTag)
                    ?? throw new InvalidDataException("SeriesValues element not found in series instance.");
            }
            set
            {
                value.TagOfElement = SeriesValuesTag;
                PhysicalStructure.RemoveElementsByTag(SeriesValuesTag);
                PhysicalStructure.AddElement(value);
            }
        }

        /// <summary>
        /// Gets the original data values, after expanding
        /// sequences and scale and offset modifications.
        /// </summary>
        public IList<object> OriginalValues => GetOriginalValues();

        /// <summary>
        /// Gets the index of the channel that owns the series to be shared.
        /// </summary>
        public uint? SeriesShareChannelIndex
        {
            get
            {
                ScalarElement? seriesShareChannelIndexScalar = PhysicalStructure
                    .GetScalarByTag(SeriesShareChannelIndexTag);

                return seriesShareChannelIndexScalar is not null
                    ? seriesShareChannelIndexScalar.GetUInt4()
                    : null;
            }
            set
            {
                if (!value.HasValue)
                {
                    PhysicalStructure.RemoveElementsByTag(SeriesShareChannelIndexTag);
                }
                else
                {
                    ScalarElement seriesShareChannelIndexScalar = PhysicalStructure
                        .GetOrAddScalar(SeriesShareChannelIndexTag);

                    seriesShareChannelIndexScalar.TypeOfValue = PhysicalType.UnsignedInteger4;
                    seriesShareChannelIndexScalar.SetUInt4(value.GetValueOrDefault());
                }
            }
        }

        /// <summary>
        /// Gets the index of the series to be shared.
        /// </summary>
        public uint? SeriesShareSeriesIndex
        {
            get
            {
                ScalarElement? seriesShareSeriesIndexScalar = PhysicalStructure
                    .GetScalarByTag(SeriesShareSeriesIndexTag);

                return seriesShareSeriesIndexScalar is not null
                    ? seriesShareSeriesIndexScalar.GetUInt4()
                    : null;
            }
            set
            {
                if (!value.HasValue)
                {
                    PhysicalStructure.RemoveElementsByTag(SeriesShareSeriesIndexTag);
                }
                else
                {
                    ScalarElement seriesShareSeriesIndexScalar = PhysicalStructure
                        .GetOrAddScalar(SeriesShareSeriesIndexTag);

                    seriesShareSeriesIndexScalar.TypeOfValue = PhysicalType.UnsignedInteger4;
                    seriesShareSeriesIndexScalar.SetUInt4(value.GetValueOrDefault());
                }
            }
        }

        /// <summary>
        /// Gets the channel that owns the series to be shared.
        /// </summary>
        public ChannelInstance? SeriesShareChannel
        {
            get
            {
                uint? seriesShareChannelIndex = SeriesShareChannelIndex;

                return seriesShareChannelIndex is not null
                    ? Channel.ObservationRecord.ChannelInstances[(int)seriesShareChannelIndex]
                    : null;
            }
        }

        /// <summary>
        /// Gets the series to be shared.
        /// </summary>
        public SeriesInstance? SeriesShareSeries
        {
            get
            {
                uint? seriesShareSeriesIndex = SeriesShareSeriesIndex;
                ChannelInstance? seriesShareChannel = SeriesShareChannel;
                SeriesInstance? seriesShareSeries = null;

                if (seriesShareSeriesIndex is not null && seriesShareChannel is not null)
                    seriesShareSeries = seriesShareChannel.SeriesInstances[(int)seriesShareSeriesIndex];

                return seriesShareSeries;
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Sets the raw values to be written to the PQDIF file as the <see cref="SeriesValues"/>.
        /// </summary>
        /// <param name="values">The values to be written to the PQDIF file.</param>
        public void SetValues(IList<object> values)
        {
            VectorElement seriesValuesElement = new()
            {
                Size = values.Count,
                TagOfElement = SeriesValuesTag,
                TypeOfValue = PhysicalTypeExtensions.GetPhysicalType(values[0].GetType())
            };

            for (int i = 0; i < values.Count; i++)
                seriesValuesElement.Set(i, values[i]);

            SeriesValues = seriesValuesElement;
        }

        /// <summary>
        /// Sets the values to be written to the PQDIF
        /// file for the increment storage method.
        /// </summary>
        /// <param name="start">The start of the increment.</param>
        /// <param name="count">The number of values in the series.</param>
        /// <param name="increment">The amount by which to increment each value in the series.</param>
        public void SetValues(object start, object count, object increment)
        {
            VectorElement seriesValuesElement = new()
            {
                Size = 3,
                TagOfElement = SeriesValuesTag,
                TypeOfValue = PhysicalTypeExtensions.GetPhysicalType(start.GetType())
            };

            seriesValuesElement.Set(0, start);
            seriesValuesElement.Set(1, count);
            seriesValuesElement.Set(2, increment);

            SeriesValues = seriesValuesElement;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
        public bool Equals(SeriesInstance? other) =>
            ReferenceEquals(PhysicalStructure, other?.PhysicalStructure);

        /// <summary>
        /// Determines whether the specified <see cref="SeriesInstance"/> is equal to the current <see cref="SeriesInstance"/>.
        /// </summary>
        /// <param name="obj">The object to compare with the current object. </param>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object? obj) =>
            obj is SeriesInstance other && Equals(other);

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>A hash code for the current <see cref="SeriesInstance"/>.</returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode() =>
            PhysicalStructure.GetHashCode();

        /// <summary>
        /// Gets the original data values by expanding
        /// sequences and applying scale and offset.
        /// </summary>
        /// <returns>A list of the original data values.</returns>
        private IList<object> GetOriginalValues()
        {
            int size = Math.Min(SeriesValues.Size, 200);
            IList<object> values = new List<object>(size);
            VectorElement valuesVector = SeriesValues;
            StorageMethods storageMethods = Definition.StorageMethodID;

            bool incremented = (storageMethods & StorageMethods.Increment) != 0;

            bool scaled = (storageMethods & StorageMethods.Scaled) != 0;
            dynamic offset = SeriesOffset is not null ? SeriesOffset.Get() : 0;
            dynamic scale = SeriesScale is not null ? SeriesScale.Get() : 1;
            dynamic value;

            if (!scaled)
            {
                offset = 0;
                scale = 1;
            }

            if (incremented)
            {
                dynamic rateCount = valuesVector.Get(0);

                if (rateCount > 0)
                {
                    dynamic zero = rateCount - rateCount;
                    dynamic one = rateCount / rateCount;
                    dynamic start = zero;

                    for (int i = 0; i < rateCount; i++)
                    {
                        int countIndex = i * 2 + 1;
                        int incrementIndex = i * 2 + 2;
                        dynamic count = valuesVector.Get(countIndex);
                        dynamic increment = valuesVector.Get(incrementIndex);

                        for (dynamic j = zero; j < count; j += one)
                            values.Add((object)(start + j * increment));

                        start = count * increment;
                    }
                }
            }
            else
            {
                for (int i = 0; i < valuesVector.Size; i++)
                    values.Add(valuesVector.Get(i));
            }

            if (valuesVector.TypeOfValue != PhysicalType.Timestamp)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    value = values[i];
                    values[i] = offset + value * scale;
                }

                ApplyTransducerRatio(values);
            }

            return values;
        }

        private void ApplyTransducerRatio(IList<object> values)
        {
            if (Channel.ObservationRecord.Settings is null)
                return;

            if (!Channel.ObservationRecord.Settings.UseTransducer)
                return;

            ChannelSetting? channelSetting = Channel.Setting;

            if (channelSetting is null)
                return;

            if (!channelSetting.HasElement(ChannelSetting.XDSystemSideRatioTag))
                return;

            if (!channelSetting.HasElement(ChannelSetting.XDMonitorSideRatioTag))
                return;

            double ratio = channelSetting.XDSystemSideRatio / channelSetting.XDMonitorSideRatio;

            for (int i = 0; i < values.Count; i++)
            {
                dynamic value = values[i];
                value *= ratio;
                values[i] = value;
            }
        }

        #endregion

        #region [ Static ]

        // Static Fields

        /// <summary>
        /// Tag that identifies the scale value to apply to the series.
        /// </summary>
        public static Guid SeriesScaleTag { get; } = new("3d786f96-f76e-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the offset value to apply to the series.
        /// </summary>
        public static Guid SeriesOffsetTag { get; } = new("3d786f97-f76e-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the values contained in the series.
        /// </summary>
        public static Guid SeriesValuesTag { get; } = new("3d786f99-f76e-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the index of the channel that owns the series to be shared.
        /// </summary>
        public static Guid SeriesShareChannelIndexTag { get; } = new("8973861f-f1c3-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the index of the series to be shared.
        /// </summary>
        public static Guid SeriesShareSeriesIndexTag { get; } = new("89738620-f1c3-11cf-9d89-0080c72e70a3");

        #endregion
    }
}
