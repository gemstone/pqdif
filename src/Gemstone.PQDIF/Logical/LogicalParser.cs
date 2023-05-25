//******************************************************************************************************
//  LogicalParser.cs - Gbtc
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
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Gemstone.PQDIF.Physical;

namespace Gemstone.PQDIF.Logical
{
    /// <summary>
    /// Represents a parser which parses the logical structure of a PQDIF file.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class makes the data from PQD files readily available to applications and defines several
    /// redundant properties throughout the logical hierarchy of the PQDIF file to also facilitate
    /// the association of definitions with instances within the logical structure. The following
    /// list enumerates some of the more useful associations within the hierarchy.
    /// </para>
    ///
    /// <list type="bullet">
    /// <item><see cref="ObservationRecord.DataSource"/></item>
    /// <item><see cref="ObservationRecord.Settings"/></item>
    /// <item><see cref="SeriesDefinition.ChannelDefinition"/></item>
    /// <item><see cref="ChannelInstance.Definition"/></item>
    /// <item><see cref="SeriesInstance.Definition"/></item>
    /// </list>
    ///
    /// <para>
    /// Usage consists of iterating through observations (<see cref="ObservationRecord"/>) to
    /// examine each of the the measurements recorded in the file. As was noted in the list above,
    /// the data source (<see cref="DataSourceRecord"/>) and settings for the monitor
    /// (<see cref="MonitorSettingsRecord"/>) associated with each observation is exposed as a
    /// property on the observation record. Note that the same data source and monitor settings
    /// records may be referenced by multiple observation records in the file.
    /// </para>
    ///
    /// <para>
    /// The following example demonstrates how to read all observation records from a PQDIF file
    /// using the logical parser.
    /// </para>
    ///
    /// <code><![CDATA[
    /// ContainerRecord containerRecord;
    /// List<ObservationRecord> observationRecords = new List<ObservationRecord>();
    /// string filePath = args[0];
    ///
    /// await using LogicalParser parser = new LogicalParser(filePath);
    /// await parser.OpenAsync();
    /// containerRecord = parser.ContainerRecord;
    ///
    /// while (await parser.HasNextObservationRecordAsync())
    ///     observationRecords.Add(await parser.NextObservationRecordAsync());
    /// ]]></code>
    /// </remarks>
    public class LogicalParser : IAsyncDisposable, IDisposable
    {
        #region [ Members ]

        // Fields
        private readonly PhysicalParser m_physicalParser;
        private DataSourceRecord? m_currentDataSourceRecord;
        private MonitorSettingsRecord? m_currentMonitorSettingsRecord;
        private ObservationRecord? m_nextObservationRecord;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Creates a new instance of the <see cref="LogicalParser"/> class.
        /// </summary>
        public LogicalParser()
        {
            m_physicalParser = new PhysicalParser();
            DataSourceRecords = new List<DataSourceRecord>();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="LogicalParser"/> class.
        /// </summary>
        /// <param name="filePath">Path to the PQDIF file to be parsed.</param>
        public LogicalParser(string filePath)
        {
            m_physicalParser = new PhysicalParser(filePath);
            DataSourceRecords = new List<DataSourceRecord>();
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets the file path (not just the name) of the PQDIF file to be parsed.
        /// Obsolete in favor of <see cref="FilePath"/>.
        /// </summary>
        [Obsolete("Property is deprecated. Please use FilePath instead.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string? FileName
        {
            get => m_physicalParser.FilePath;
            set => m_physicalParser.FilePath = value;
        }

        /// <summary>
        /// Gets or sets the file path of the PQDIF file to be parsed.
        /// </summary>
        public string? FilePath
        {
            get => m_physicalParser.FilePath;
            set => m_physicalParser.FilePath = value;
        }

        /// <summary>
        /// Gets the container record from the PQDIF file.
        /// This is parsed as soon as the parser is opened.
        /// </summary>
        public ContainerRecord? ContainerRecord { get; private set; }

        /// <summary>
        /// Gets a list of all DataSource records from the PQDIF file.
        /// This is parsed when scanning through the observation records.
        /// </summary>
        public List<DataSourceRecord> DataSourceRecords { get; private set; }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Opens the parser and parses the <see cref="ContainerRecord"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="FilePath"/> has not been defined.</exception>
        /// <exception cref="InvalidDataException">First record of PQDIF file is not the container record.</exception>
        /// <exception cref="NotSupportedException">An unsupported compression mode was defined in the PQDIF file.</exception>
        /// <exception cref="EndOfStreamException">End of stream encountered while reading the container record.</exception>
        public async Task OpenAsync()
        {
            await m_physicalParser.OpenAsync();
            await ReadContainerRecordAsync();
        }

        /// <summary>
        /// Opens the parser and parses the <see cref="ContainerRecord"/>.
        /// </summary>
        /// <param name="stream">The stream containing the PQDIF file data.</param>
        /// <param name="leaveOpen">True if the stream should be closed when the parser is closed; false otherwise.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is null.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="stream"/> is not both readable and seekable.</exception>
        /// <exception cref="InvalidDataException">First record of PQDIF file is not the container record.</exception>
        /// <exception cref="NotSupportedException">An unsupported compression mode was defined in the PQDIF file.</exception>
        /// <exception cref="EndOfStreamException">End of stream encountered while reading the container record.</exception>
        public async Task OpenAsync(Stream stream, bool leaveOpen = false)
        {
            await m_physicalParser.OpenAsync(stream, leaveOpen);
            await ReadContainerRecordAsync();
        }

        /// <summary>
        /// Determines whether there are any more <see cref="ObservationRecord"/>s to be read from the PQDIF file.
        /// </summary>
        /// <returns>true if there is another observation record to be read from PQDIF file; false otherwise</returns>
        /// <exception cref="InvalidOperationException">
        ///     <para>The PQDIF file is not open.</para>
        ///     <para>- OR -</para>
        ///     <para>PQDIF file has more than one container record.</para>
        /// </exception>
        /// <exception cref="InvalidDataException">Observation record found with no corresponding data source record.</exception>
        /// <exception cref="EndOfStreamException">End of stream encountered while reading the next observation record.</exception>
        public async Task<bool> HasNextObservationRecordAsync()
        {
            Record physicalRecord;
            RecordType recordType;

            // Read records from the file until we encounter an observation record or end of file
            while (m_nextObservationRecord is null && m_physicalParser.HasNextRecord())
            {
                physicalRecord = await m_physicalParser.GetNextRecordAsync();
                recordType = physicalRecord.Header.TypeOfRecord;

                switch (recordType)
                {
                    case RecordType.DataSource:
                        // Keep track of the latest data source record in order to associate it with observation records
                        m_currentDataSourceRecord = DataSourceRecord.CreateDataSourceRecord(physicalRecord)
                            ?? throw new InvalidDataException("Invalid assumption: record type is data source, yet the data source record was not created.");

                        DataSourceRecords.Add(m_currentDataSourceRecord);
                        break;

                    case RecordType.MonitorSettings:
                        // Keep track of the latest monitor settings record in order to associate it with observation records
                        m_currentMonitorSettingsRecord = MonitorSettingsRecord.CreateMonitorSettingsRecord(physicalRecord);
                        break;

                    case RecordType.Observation:
                        // Found an observation record!
                        if (m_currentDataSourceRecord is null)
                            throw new InvalidDataException("Found observation record before finding data source record.");

                        m_nextObservationRecord = ObservationRecord.CreateObservationRecord(physicalRecord, m_currentDataSourceRecord, m_currentMonitorSettingsRecord);
                        break;

                    case RecordType.Container:
                        // The container record is parsed when the file is opened; it should never be encountered here
                        throw new InvalidOperationException("Found more than one container record in PQDIF file.");
                }
            }

            return m_nextObservationRecord is not null;
        }

        /// <summary>
        /// Gets the next observation record from the PQDIF file.
        /// </summary>
        /// <returns>The next observation record.</returns>
        /// <exception cref="InvalidOperationException">
        ///     <para>The PQDIF file is not open.</para>
        ///     <para>- OR -</para>
        ///     <para>PQDIF file has more than one container record.</para>
        ///     <para>- OR -</para>
        ///     <para>There are no more observation records in the PQDIF file.</para>
        /// </exception>
        /// <exception cref="InvalidDataException">Observation record found with no corresponding data source record.</exception>
        /// <exception cref="EndOfStreamException">End of stream encountered while reading the next observation record.</exception>
        public async Task<ObservationRecord> NextObservationRecordAsync()
        {
            // Call this first to read ahead to the next
            // observation record if we haven't already
            await HasNextObservationRecordAsync();

            // We need to set m_nextObservationRecord to null so that
            // subsequent calls to HasNextObservationRecord() will
            // continue to parse new records
            ObservationRecord? nextObservationRecord = m_nextObservationRecord;
            m_nextObservationRecord = null;

            if (nextObservationRecord is null)
                throw new InvalidOperationException("There are no more observation records in the PQDIF file.");

            return nextObservationRecord;
        }

        /// <summary>
        /// Resets the parser to the beginning of the PQDIF file.
        /// </summary>
        /// <exception cref="InvalidOperationException">The PQDIF file is not open.</exception>
        /// <exception cref="InvalidDataException">First record of PQDIF file is not the container record.</exception>
        /// <exception cref="EndOfStreamException">End of stream encountered while reading the container record.</exception>
        public async Task ResetAsync()
        {
            m_currentDataSourceRecord = null;
            m_currentMonitorSettingsRecord = null;
            m_nextObservationRecord = null;
            DataSourceRecords = new List<DataSourceRecord>();

            m_physicalParser.Reset();
            await ReadContainerRecordAsync();
        }

        /// <summary>
        /// Closes the PQDIF file.
        /// </summary>
        /// <exception cref="InvalidOperationException">The PQDIF file is not open.</exception>
        public Task CloseAsync() =>
            m_physicalParser.CloseAsync();

        /// <summary>
        /// Releases resources held by the parser.
        /// </summary>
        public ValueTask DisposeAsync() =>
            m_physicalParser.DisposeAsync();

        /// <summary>
        /// Releases resources held by the parser.
        /// </summary>
        public void Dispose() =>
            m_physicalParser.Dispose();

        // Attempts to read the container record and update
        // the compression settings of the physical parser.
        private async Task ReadContainerRecordAsync()
        {
            Record firstRecord = await m_physicalParser.GetNextRecordAsync();
            ContainerRecord = ContainerRecord.CreateContainerRecord(firstRecord);

            if (ContainerRecord is null)
                throw new InvalidDataException("The first record in a PQDIF file must be a container record.");

            m_physicalParser.CompressionAlgorithm = ContainerRecord.CompressionAlgorithm;
            m_physicalParser.CompressionStyle = ContainerRecord.CompressionStyle;
        }

        #endregion
    }
}
