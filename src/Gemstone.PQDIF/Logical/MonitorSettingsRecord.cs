//******************************************************************************************************
//  MonitorSettingsRecord.cs - Gbtc
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
using System.IO;
using System.Linq;
using Gemstone.PQDIF.Physical;

namespace Gemstone.PQDIF.Logical
{
    /// <summary>
    /// Represents a monitor settings record in a PQDIF file.
    /// </summary>
    public class MonitorSettingsRecord
    {
        #region [ Constructors ]

        /// <summary>
        /// Creates a new instance of the <see cref="MonitorSettingsRecord"/> class.
        /// </summary>
        /// <param name="physicalRecord">The physical structure of the monitor settings record.</param>
        private MonitorSettingsRecord(Record physicalRecord)
        {
            PhysicalRecord = physicalRecord;
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets the physical record of the monitor settings record.
        /// </summary>
        public Record PhysicalRecord { get; }

        /// <summary>
        /// Gets or sets the date time at which these settings become effective.
        /// </summary>
        /// <exception cref="InvalidDataException">Effective element not found in monitor settings record.</exception>
        public DateTime Effective
        {
            get
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;

                ScalarElement effectiveElement = collectionElement.GetScalarByTag(EffectiveTag)
                    ?? throw new InvalidDataException("Effective element not found in monitor settings record.");

                return effectiveElement.GetTimestamp();
            }
            set
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                ScalarElement effectiveElement = collectionElement.GetOrAddScalar(EffectiveTag);
                effectiveElement.TypeOfValue = PhysicalType.Timestamp;
                effectiveElement.SetTimestamp(value);
            }
        }

        /// <summary>
        /// Gets or sets the time at which the settings were installed.
        /// </summary>
        /// <exception cref="InvalidDataException">TimeInstalled element not found in monitor settings record.</exception>
        public DateTime TimeInstalled
        {
            get
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;

                ScalarElement timeInstalledElement = collectionElement.GetScalarByTag(TimeInstalledTag)
                    ?? throw new InvalidDataException("TimeInstalled element not found in monitor settings record.");

                return timeInstalledElement.GetTimestamp();
            }
            set
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                ScalarElement timeInstalledElement = collectionElement.GetOrAddScalar(TimeInstalledTag);
                timeInstalledElement.TypeOfValue = PhysicalType.Timestamp;
                timeInstalledElement.SetTimestamp(value);
            }
        }

        /// <summary>
        /// Gets or sets the flag that determines whether the
        /// calibration settings need to be applied before using
        /// the values recorded by this monitor.
        /// </summary>
        /// <exception cref="InvalidDataException">UseCalibration element not found in monitor settings record.</exception>
        public bool UseCalibration
        {
            get
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;

                ScalarElement useCalibrationElement = collectionElement.GetScalarByTag(UseCalibrationTag)
                    ?? throw new InvalidDataException("UseCalibration element not found in monitor settings record.");

                return useCalibrationElement.GetBool4();
            }
            set
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                ScalarElement useCalibrationElement = collectionElement.GetOrAddScalar(UseCalibrationTag);
                useCalibrationElement.TypeOfValue = PhysicalType.Boolean4;
                useCalibrationElement.SetBool4(value);
            }
        }

        /// <summary>
        /// Gets or sets the flag that determines whether the
        /// transducer ratio needs to be applied before using
        /// the values recorded by this monitor.
        /// </summary>
        /// <exception cref="InvalidDataException">UseTransducer element not found in monitor settings record.</exception>
        public bool UseTransducer
        {
            get
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;

                ScalarElement useTransducerElement = collectionElement.GetScalarByTag(UseTransducerTag)
                    ?? throw new InvalidDataException("UseTransducer element not found in monitor settings record.");

                return useTransducerElement.GetBool4();
            }
            set
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                ScalarElement useTransducerElement = collectionElement.GetOrAddScalar(UseTransducerTag);
                useTransducerElement.TypeOfValue = PhysicalType.Boolean4;
                useTransducerElement.SetBool4(value);
            }
        }

        /// <summary>
        /// Gets or sets the settings for the channels defined in the data source.
        /// </summary>
        public IList<ChannelSetting>? ChannelSettings
        {
            get
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                CollectionElement? channelSettingsArray = collectionElement.GetCollectionByTag(ChannelSettingsArrayTag);

                if (channelSettingsArray is null)
                    return null;

                return channelSettingsArray
                    .GetElementsByTag(OneChannelSettingTag)
                    .Cast<CollectionElement>()
                    .Select(collection => new ChannelSetting(collection, this))
                    .ToList();
            }
        }

        /// <summary>
        /// Gets or sets nominal frequency.
        /// </summary>
        public double NominalFrequency
        {
            get
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                ScalarElement? nominalFrequencyElement = collectionElement.GetScalarByTag(NominalFrequencyTag);

                if (nominalFrequencyElement is null)
                    return DefaultNominalFrequency;

                return nominalFrequencyElement.GetReal8();
            }
            set
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                ScalarElement nominalFrequencyElement = collectionElement.GetOrAddScalar(NominalFrequencyTag);
                nominalFrequencyElement.TypeOfValue = PhysicalType.Real8;
                nominalFrequencyElement.SetReal8(value);
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Adds a new channel setting to the collection
        /// of channel settings in this monitor settings record.
        /// </summary>
        /// <param name="channelDefinition">Channel definition to use for looking up channel definition index of new channel setting.</param>
        /// <returns>New channel setting.</returns>
        public ChannelSetting AddNewChannelSetting(ChannelDefinition channelDefinition)
        {
            CollectionElement channelSettingElement = new() { TagOfElement = OneChannelSettingTag };
            ChannelSetting channelSetting = new(channelSettingElement, this);
            channelSetting.ChannelDefinitionIndex = (uint)channelDefinition.DataSource.ChannelDefinitions.IndexOf(channelDefinition);

            CollectionElement? channelSettingsElement = PhysicalRecord.Body.Collection.GetCollectionByTag(ChannelSettingsArrayTag);

            if (channelSettingsElement is null)
            {
                channelSettingsElement = new CollectionElement { TagOfElement = OneChannelSettingTag };
                PhysicalRecord.Body.Collection.AddElement(channelSettingsElement);
            }

            channelSettingsElement.AddElement(channelSettingElement);

            return channelSetting;
        }

        /// <summary>
        /// Removes the given channel setting from the collection of channel settings.
        /// </summary>
        /// <param name="channelSetting">The channel setting to be removed.</param>
        public void Remove(ChannelSetting channelSetting)
        {
            CollectionElement? channelSettingsElement = PhysicalRecord.Body.Collection.GetCollectionByTag(ChannelSettingsArrayTag);

            if (channelSettingsElement is null)
                return;

            List<CollectionElement> channelSettingElements = channelSettingsElement.GetElementsByTag(OneChannelSettingTag).Cast<CollectionElement>().ToList();

            foreach (CollectionElement channelSettingElement in channelSettingElements)
            {
                ChannelSetting setting = new(channelSettingElement, this);

                if (Equals(channelSetting, setting))
                    channelSettingsElement.RemoveElement(channelSettingElement);
            }
        }

        #endregion

        #region [ Static ]

        // Static Fields

        /// <summary>
        /// Tag that identifies the time that these settings become effective.
        /// </summary>
        public static Guid EffectiveTag { get; } = new("62f28183-f9c4-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the install time.
        /// </summary>
        public static Guid TimeInstalledTag { get; } = new("3d786f85-f76e-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the flag which determines whether to apply calibration to the series.
        /// </summary>
        public static Guid UseCalibrationTag { get; } = new("62f28180-f9c4-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the flag which determines whether to apply transducer adjustments to the series.
        /// </summary>
        public static Guid UseTransducerTag { get; } = new("62f28181-f9c4-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the collection of channel settings.
        /// </summary>
        public static Guid ChannelSettingsArrayTag { get; } = new("62f28182-f9c4-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies one channel setting in the collection.
        /// </summary>
        public static Guid OneChannelSettingTag { get; } = new("3d786f9a-f76e-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the nominal frequency.
        /// </summary>
        public static Guid NominalFrequencyTag { get; } = new("0fa118c3-cb4a-11d2-b30b-fe25cb9a1760");

        // Static Properties

        /// <summary>
        /// Gets or sets the default value for the <see cref="NominalFrequency"/>
        /// property when the value is not defined in the PQDIF file.
        /// </summary>
        public static double DefaultNominalFrequency { get; set; } = 60.0D;

        // Static Methods

        /// <summary>
        /// Creates a new monitor settings record from scratch.
        /// </summary>
        /// <returns>The new monitor settings record.</returns>
        public static MonitorSettingsRecord CreateMonitorSettingsRecord()
        {
            Guid recordTypeTag = Record.GetTypeAsTag(RecordType.MonitorSettings);
            Record physicalRecord = new(recordTypeTag);
            MonitorSettingsRecord monitorSettingsRecord = new(physicalRecord);

            DateTime now = DateTime.UtcNow;
            monitorSettingsRecord.Effective = now;
            monitorSettingsRecord.TimeInstalled = now;
            monitorSettingsRecord.UseCalibration = false;
            monitorSettingsRecord.UseTransducer = false;

            CollectionElement bodyElement = physicalRecord.Body.Collection;
            bodyElement.AddElement(new CollectionElement { TagOfElement = ChannelSettingsArrayTag });

            return monitorSettingsRecord;
        }

        /// <summary>
        /// Creates a new monitor settings record from the given physical record
        /// if the physical record is of type monitor settings. Returns null if
        /// it is not.
        /// </summary>
        /// <param name="physicalRecord">The physical record used to create the monitor settings record.</param>
        /// <returns>The new monitor settings record, or null if the physical record does not define a monitor settings record.</returns>
        public static MonitorSettingsRecord? CreateMonitorSettingsRecord(Record physicalRecord)
        {
            bool isValidMonitorSettingsRecord = physicalRecord.Header.TypeOfRecord == RecordType.MonitorSettings;
            return isValidMonitorSettingsRecord ?  new MonitorSettingsRecord(physicalRecord) : null;
        }

        #endregion
    }
}
