﻿//******************************************************************************************************
//  ChannelSetting.cs - Gbtc
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
//  09/11/2015 - Stephen C. Wills
//       Generated original version of source code.
//
//******************************************************************************************************

using System;
using System.IO;
using System.Linq;
using Gemstone.PQDIF.Physical;

namespace Gemstone.PQDIF.Logical
{
    /// <summary>
    /// Represents a channel setting in a PQDIF file. A channel setting
    /// resides in an <see cref="MonitorSettingsRecord"/>, and is defined by
    /// a <see cref="ChannelDefinition"/> inside the observation record's
    /// <see cref="DataSourceRecord"/>.
    /// </summary>
    public class ChannelSetting
    {
        #region [ Constructors ]

        /// <summary>
        /// Creates a new instance of the <see cref="ChannelSetting"/> class.
        /// </summary>
        /// <param name="physicalStructure">The collection element which is the physical structure of the channel setting.</param>
        /// <param name="monitorSettingsRecord">The monitor settings record in which the channel setting resides.</param>
        public ChannelSetting(CollectionElement physicalStructure, MonitorSettingsRecord monitorSettingsRecord)
        {
            PhysicalStructure = physicalStructure;
            MonitorSettingsRecord = monitorSettingsRecord;
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets the physical structure of the channel setting.
        /// </summary>
        public CollectionElement PhysicalStructure { get; }

        /// <summary>
        /// Gets the monitor settings record in which the channel setting resides.
        /// </summary>
        public MonitorSettingsRecord MonitorSettingsRecord { get; }

        /// <summary>
        /// Gets the index of the <see cref="ChannelDefinition"/>
        /// which defines the channel instance.
        /// </summary>
        /// <exception cref="InvalidDataException">ChannelDefinitionIndex element not found in channel setting.</exception>
        public uint ChannelDefinitionIndex
        {
            get
            {
                ScalarElement channelDefinitionIndexElement = PhysicalStructure.GetScalarByTag(ChannelDefinition.ChannelDefinitionIndexTag)
                    ?? throw new InvalidDataException("ChannelDefinitionIndex element not found in channel setting.");

                return channelDefinitionIndexElement.GetUInt4();
            }
            set
            {
                ScalarElement channelDefinitionIndexElement = PhysicalStructure.GetOrAddScalar(ChannelDefinition.ChannelDefinitionIndexTag);
                channelDefinitionIndexElement.TypeOfValue = PhysicalType.UnsignedInteger4;
                channelDefinitionIndexElement.SetUInt4(value);
            }
        }

        /// <summary>
        /// Gets the system-side part of the transducer ratio.
        /// </summary>
        public double XDSystemSideRatio
        {
            get
            {
                ScalarElement? xdSystemSideRatioElement = PhysicalStructure.GetScalarByTag(XDSystemSideRatioTag);

                if (xdSystemSideRatioElement is null)
                    return 1.0D;

                return xdSystemSideRatioElement.GetReal8();
            }
            set
            {
                ScalarElement xdSystemSideRatioElement = PhysicalStructure.GetOrAddScalar(XDSystemSideRatioTag);
                xdSystemSideRatioElement.TypeOfValue = PhysicalType.Real8;
                xdSystemSideRatioElement.SetReal8(value);
            }
        }

        /// <summary>
        /// Gets the monitor-side part of the transducer ratio.
        /// </summary>
        public double XDMonitorSideRatio
        {
            get
            {
                ScalarElement? xdMonitorSideRatioElement = PhysicalStructure.GetScalarByTag(XDMonitorSideRatioTag);

                if (xdMonitorSideRatioElement is null)
                    return 1.0D;

                return xdMonitorSideRatioElement.GetReal8();
            }
            set
            {
                ScalarElement xdMonitorSideRatioElement = PhysicalStructure.GetOrAddScalar(XDMonitorSideRatioTag);
                xdMonitorSideRatioElement.TypeOfValue = PhysicalType.Real8;
                xdMonitorSideRatioElement.SetReal8(value);
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

        #endregion

        #region [ Static ]

        // Static Fields

        /// <summary>
        /// Tag that identifies the system side ratio.
        /// </summary>
        public static Guid XDSystemSideRatioTag { get; } = new("62f2818a-f9c4-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the monitor side ratio.
        /// </summary>
        public static Guid XDMonitorSideRatioTag { get; } = new("62f2818b-f9c4-11cf-9d89-0080c72e70a3");

        #endregion
    }
}
