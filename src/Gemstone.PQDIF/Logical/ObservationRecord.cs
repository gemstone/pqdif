﻿//******************************************************************************************************
//  ObservationRecord.cs - Gbtc
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
using System.Text;
using Gemstone.PQDIF.Physical;

namespace Gemstone.PQDIF.Logical
{
    #region [ Enumerations ]

    /// <summary>
    /// Type of trigger which caused the observation.
    /// </summary>
    public enum TriggerMethod : uint
    {
        /// <summary>
        /// No trigger.
        /// </summary>
        None = 0u,

        /// <summary>
        /// A specific channel (or channels) caused the trigger; should be
        /// used with tagChannelTriggerIdx to specify which channels.
        /// </summary>
        Channel = 1u,

        /// <summary>
        /// Periodic data trigger.
        /// </summary>
        Periodic = 2u,

        /// <summary>
        /// External system trigger.
        /// </summary>
        External = 3u,

        /// <summary>
        /// Periodic statistical data.
        /// </summary>
        PeriodicStats = 4u
    }

    #endregion

    /// <summary>
    /// Represents an observation record in a PQDIF file.
    /// </summary>
    public class ObservationRecord
    {
        #region [ Constructors ]

        /// <summary>
        /// Creates a new instance of the <see cref="ObservationRecord"/> class.
        /// </summary>
        /// <param name="physicalRecord">The physical structure of the observation record.</param>
        /// <param name="dataSource">The data source record that defines the channels in this observation record.</param>
        /// <param name="settings">The monitor settings to be applied to this observation record.</param>
        private ObservationRecord(Record physicalRecord, DataSourceRecord dataSource, MonitorSettingsRecord? settings)
        {
            PhysicalRecord = physicalRecord;
            DataSource = dataSource;
            Settings = settings;
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets the physical structure of the observation record.
        /// </summary>
        public Record PhysicalRecord { get; }

        /// <summary>
        /// Gets the data source record that defines
        /// the channels in this observation record.
        /// </summary>
        public DataSourceRecord DataSource { get; }

        /// <summary>
        /// Gets the monitor settings record that defines the
        /// settings to be applied to this observation record.
        /// </summary>
        public MonitorSettingsRecord? Settings { get; }

        /// <summary>
        /// Gets the name of the observation record.
        /// </summary>
        /// <exception cref="InvalidDataException">Name element not found in observation record.</exception>
        public string Name
        {
            get
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;

                VectorElement nameElement = collectionElement.GetVectorByTag(ObservationNameTag)
                    ?? throw new InvalidDataException("Name element not found in observation record.");

                return Encoding.ASCII.GetString(nameElement.GetValues()).Trim((char)0);
            }
            set
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                byte[] bytes = Encoding.ASCII.GetBytes(value + (char)0);
                collectionElement.AddOrUpdateVector(ObservationNameTag, PhysicalType.Char1, bytes);
            }
        }

        /// <summary>
        /// Gets the creation time of the observation record.
        /// </summary>
        /// <exception cref="InvalidDataException">CreateTime element not found in observation record.</exception>
        public DateTime CreateTime
        {
            get
            {
                ScalarElement createTimeElement = PhysicalRecord.Body.Collection.GetScalarByTag(TimeCreateTag)
                    ?? throw new InvalidDataException("CreateTime element not found in observation record.");

                return createTimeElement.GetTimestamp();
            }
            set
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                ScalarElement timeCreateElement = collectionElement.GetOrAddScalar(TimeCreateTag);
                timeCreateElement.TypeOfValue = PhysicalType.Timestamp;
                timeCreateElement.SetTimestamp(value);
            }
        }

        /// <summary>
        /// Gets the starting time of the data in the observation record.
        /// </summary>
        /// <exception cref="InvalidDataException">StartTime element not found in observation record.</exception>
        public DateTime StartTime
        {
            get
            {
                ScalarElement startTimeElement = PhysicalRecord.Body.Collection.GetScalarByTag(TimeStartTag)
                    ?? throw new InvalidDataException("StartTime element not found in observation record.");

                return startTimeElement.GetTimestamp();
            }
            set
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                ScalarElement timeStartElement = collectionElement.GetOrAddScalar(TimeStartTag);
                timeStartElement.TypeOfValue = PhysicalType.Timestamp;
                timeStartElement.SetTimestamp(value);
            }
        }

        /// <summary>
        /// Gets or sets the type of trigger which caused the observation.
        /// </summary>
        /// <exception cref="InvalidDataException">TriggerMethod element not found in observation record.</exception>
        public TriggerMethod TriggerMethod
        {
            get
            {
                ScalarElement triggerMethodElement = PhysicalRecord.Body.Collection.GetScalarByTag(TriggerMethodTag)
                    ?? throw new InvalidDataException("TriggerMethod element not found in observation record.");

                return (TriggerMethod)triggerMethodElement.GetUInt4();
            }
            set
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                ScalarElement triggerMethodElement = collectionElement.GetOrAddScalar(TriggerMethodTag);
                triggerMethodElement.TypeOfValue = PhysicalType.UnsignedInteger4;
                triggerMethodElement.SetUInt4((uint)value);
            }
        }


        /// <summary>
        /// Gets the time the observation was triggered.
        /// </summary>
        public DateTime TimeTriggered
        {
            get
            {
                ScalarElement? timeTriggeredElement = PhysicalRecord.Body.Collection
                    .GetScalarByTag(TimeTriggeredTag);

                if (timeTriggeredElement == null)
                    return DateTime.MinValue;

                return timeTriggeredElement.GetTimestamp();
            }
            set
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                ScalarElement timeTriggeredElement = collectionElement.GetOrAddScalar(TimeTriggeredTag);
                timeTriggeredElement.TypeOfValue = PhysicalType.Timestamp;
                timeTriggeredElement.SetTimestamp(value);
            }
        }

        /// <summary>
        /// Gets or sets the Disturbance Category ID
        /// </summary>
        public Guid DisturbanceCategoryID
        {
            get
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                ScalarElement? DisturbanceIDElement = collectionElement.GetScalarByTag(DisturbanceCategoryTag);

                if (DisturbanceIDElement == null)
                    return DisturbanceCategory.None;

                return DisturbanceIDElement.GetGuid();
            }
            set
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                ScalarElement vendorIDElement = collectionElement.GetOrAddScalar(DisturbanceCategoryTag);
                vendorIDElement.TypeOfValue = PhysicalType.Guid;
                vendorIDElement.SetGuid(value);
            }
        }
    

        /// <summary>
        /// Gets or sets the index into <see cref="ChannelInstancesTag"/> collection within this record which initiated the observation.
        /// </summary>
        public uint[] ChannelTriggerIndex
        {
            get
            {
                VectorElement? channelTriggerIndexElement = PhysicalRecord.Body.Collection
                    .GetVectorByTag(ChannelTriggerIndexTag);

                if (channelTriggerIndexElement == null)
                    return new uint[0];

                return Enumerable.Range(0, channelTriggerIndexElement.Size)
                    .Select(index => channelTriggerIndexElement.GetUInt4(index))
                    .ToArray();
            }
            set
            {
                CollectionElement collectionElement = PhysicalRecord.Body.Collection;
                VectorElement channelTriggerIndexElement = collectionElement.GetOrAddVector(ChannelTriggerIndexTag);
                channelTriggerIndexElement.TypeOfValue = PhysicalType.UnsignedInteger4;
                channelTriggerIndexElement.Size = value.Length;

                for (int i = 0; i < value.Length; i++)
                    channelTriggerIndexElement.SetUInt4(i, value[i]);
            }
        }

        /// <summary>
        /// Gets the channel instances in this observation record.
        /// </summary>
        /// <exception cref="InvalidDataException">ChannelInstances element not found in observation record.</exception>
        public IList<ChannelInstance> ChannelInstances
        {
            get
            {
                CollectionElement channelInstancesElement = PhysicalRecord.Body.Collection.GetCollectionByTag(ChannelInstancesTag)
                    ?? throw new InvalidDataException("ChannelInstances element not found in observation record.");

                return channelInstancesElement
                    .GetElementsByTag(OneChannelInstanceTag)
                    .Cast<CollectionElement>()
                    .Select(collection => new ChannelInstance(collection, this))
                    .ToList();
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Adds a new channel instance to the collection
        /// of channel instances in this observation record.
        /// </summary>
        /// <param name="channelDefinition">Channel definition to use for looking up channel definition index of new channel instance.</param>
        /// <returns>NEw channel instance.</returns>
        public ChannelInstance AddNewChannelInstance(ChannelDefinition channelDefinition)
        {
            CollectionElement channelInstanceElement = new CollectionElement { TagOfElement = OneChannelInstanceTag };
            ChannelInstance channelInstance = new ChannelInstance(channelInstanceElement, this);

            channelInstance.ChannelDefinitionIndex = (uint)channelDefinition.DataSource.ChannelDefinitions.IndexOf(channelDefinition);
            channelInstanceElement.AddElement(new CollectionElement { TagOfElement = ChannelInstance.SeriesInstancesTag });

            CollectionElement? channelInstancesElement = PhysicalRecord.Body.Collection.GetCollectionByTag(ChannelInstancesTag);

            if (channelInstancesElement == null)
            {
                channelInstancesElement = new CollectionElement { TagOfElement = ChannelInstancesTag };
                PhysicalRecord.Body.Collection.AddElement(channelInstancesElement);
            }

            channelInstancesElement.AddElement(channelInstanceElement);

            return channelInstance;
        }

        /// <summary>
        /// Removes the given channel instance from the collection of channel instances.
        /// </summary>
        /// <param name="channelInstance">The channel instance to be removed.</param>
        public void Remove(ChannelInstance channelInstance)
        {
            CollectionElement? channelInstancesElement = PhysicalRecord.Body.Collection.GetCollectionByTag(ChannelInstancesTag);

            if (channelInstancesElement == null)
                return;

            List<CollectionElement> channelInstanceElements = channelInstancesElement.GetElementsByTag(OneChannelInstanceTag).Cast<CollectionElement>().ToList();

            foreach (CollectionElement channelSettingElement in channelInstanceElements)
            {
                ChannelInstance instance = new ChannelInstance(channelSettingElement, this);

                if (Equals(channelInstance, instance))
                    channelInstancesElement.RemoveElement(channelSettingElement);
            }
        }

        #endregion

        #region [ Static ]

        // Static Fields

        /// <summary>
        /// Tag that identifies the name of the observation record.
        /// </summary>
        public static Guid ObservationNameTag { get; } = new Guid("3d786f8a-f76e-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the time that the observation record was created.
        /// </summary>
        public static Guid TimeCreateTag { get; } = new Guid("3d786f8b-f76e-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the start time of the data in the observation record.
        /// </summary>
        public static Guid TimeStartTag { get; } = new Guid("3d786f8c-f76e-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the type of trigger that caused the observation.
        /// </summary>
        public static Guid TriggerMethodTag { get; } = new Guid("3d786f8d-f76e-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the time the observation was triggered.
        /// </summary>
        public static Guid TimeTriggeredTag { get; } = new Guid("3d786f8e-f76e-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the index into <see cref="ChannelInstancesTag"/> collection within this record. This specifies which channel(s) initiated the observation.
        /// </summary>
        public static Guid ChannelTriggerIndexTag { get; } = new Guid("3d786f8f-f76e-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the channel instances collection.
        /// </summary>
        public static Guid ChannelInstancesTag { get; } = new Guid("3d786f91-f76e-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies a single channel instance in the collection.
        /// </summary>
        public static Guid OneChannelInstanceTag { get; } = new Guid("3d786f92-f76e-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// Tag that identifies the Disturbance Category.
        /// </summary>
        public static Guid DisturbanceCategoryTag { get; } = new Guid("b48d8597-f5f5-11cf-9d89-0080c72e70a3");


        // Static Methods

        /// <summary>
        /// Creates a new observation record from scratch with the given data source and settings.
        /// </summary>
        /// <param name="dataSource">The data source record that defines the channels in this observation record.</param>
        /// <param name="settings">The monitor settings to be applied to this observation record.</param>
        /// <returns>The new observation record.</returns>
        public static ObservationRecord CreateObservationRecord(DataSourceRecord dataSource, MonitorSettingsRecord? settings)
        {
            Guid recordTypeTag = Record.GetTypeAsTag(RecordType.Observation);
            Record physicalRecord = new Record(recordTypeTag);
            ObservationRecord observationRecord = new ObservationRecord(physicalRecord, dataSource, settings);

            DateTime now = DateTime.UtcNow;
            observationRecord.Name = now.ToString();
            observationRecord.CreateTime = now;
            observationRecord.StartTime = now;
            observationRecord.TriggerMethod = TriggerMethod.None;

            CollectionElement bodyElement = physicalRecord.Body.Collection;
            bodyElement.AddElement(new CollectionElement { TagOfElement = ChannelInstancesTag });

            return observationRecord;
        }

        /// <summary>
        /// Creates a new observation record from the given physical record
        /// if the physical record is of type observation. Returns null if
        /// it is not.
        /// </summary>
        /// <param name="physicalRecord">The physical record used to create the observation record.</param>
        /// <param name="dataSource">The data source record that defines the channels in this observation record.</param>
        /// <param name="settings">The monitor settings to be applied to this observation record.</param>
        /// <returns>The new observation record, or null if the physical record does not define a observation record.</returns>
        public static ObservationRecord? CreateObservationRecord(Record physicalRecord, DataSourceRecord dataSource, MonitorSettingsRecord? settings)
        {
            bool isValidObservationRecord = physicalRecord.Header.TypeOfRecord == RecordType.Observation;
            return isValidObservationRecord ? new ObservationRecord(physicalRecord, dataSource, settings) : null;
        }

        #endregion
    }
}
