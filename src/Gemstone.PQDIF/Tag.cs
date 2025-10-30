//******************************************************************************************************
//  Tag.cs - Gbtc
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
//  12/14/2015 - Stephen C. Wills
//       Generated original version of source code.
//
//******************************************************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Gemstone.PQDIF.Physical;

namespace Gemstone.PQDIF
{
    /// <summary>
    /// Represents a tag as defined by the PQDIF standard.
    /// </summary>
    public class Tag
    {
        #region [ Members ]

        // Constants

        /// <summary>
        /// Name of the file from which to retrieve the <see cref="TagDefinitions"/>.
        /// </summary>
        public const string TagDefinitionsFileName = "TagDefinitions.xml";

        #endregion

        #region [ Constructors ]

        // Tags are readonly and can only be created by
        // defining them in the TagDefinitions.xml file.
        private Tag(XDocument doc, XElement element)
        {
            ID = Guid.Parse((string?)element.Element("id") ?? Guid.Empty.ToString());
            Name = (string?)element.Element("name") ?? "undefined";
            StandardName = (string?)element.Element("standardName") ?? "undefined";
            Description = (string?)element.Element("description") ?? "";
            ElementType = GetElementType(element);
            PhysicalType = GetPhysicalType(element);
            Required = Convert.ToBoolean((string?)element.Element("required") ?? "False");
            FormatString = (string?)element.Element("formatString");
            ValidIdentifiers = Identifier.GenerateIdentifiers(doc, this);
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets the globally unique identifier for the tag.
        /// </summary>
        public Guid ID { get; }

        /// <summary>
        /// Gets the name of the tag as defined by the gemstone.pqdif library.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the name of the tag as defined by the standard.
        /// </summary>
        public string StandardName { get; }

        /// <summary>
        /// Gets a description of the tag.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the type of the element identified by the tag
        /// according to the logical structure of a PQDIF file.
        /// </summary>
        public ElementType ElementType { get; }

        /// <summary>
        /// Gets the physical type of the value of the element identified
        /// by the tag according to the logical structure of a PQDIF file.
        /// </summary>
        public PhysicalType PhysicalType { get; }

        /// <summary>
        /// Gets the flag that determines whether it is
        /// required that this tag be specified in a PQDIF file.
        /// </summary>
        public bool Required { get; }

        /// <summary>
        /// Format string specified for some tags as a
        /// hint for how the value should be displayed.
        /// </summary>
        public string? FormatString { get; }

        /// <summary>
        /// Gets the collection of valid identifiers for this tag.
        /// </summary>
        public IReadOnlyList<Identifier> ValidIdentifiers { get; }

        #endregion

        #region [ Static ]

        // Static Fields
        private static Dictionary<Guid, Tag>? TagLookup { get; set; }

        // Static Properties

        /// <summary>
        /// Gets the definitions of PQDIF tags as defined by the tag definitions file.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property first tries to load definitions from the TagDefinitions.xml
        /// file in the working directory of the application. If the file doesn't exist
        /// or cannot be parsed as a valid XML file, then this property falls back on
        /// the version hosted inside this assembly as an embedded resource.
        /// </para>
        ///
        /// <para>
        /// Applications that need to customize the set of tags supported by this library
        /// can use this property to generate the default TagDefinitions.xml file, edit
        /// the file as necessary, and then place the edited version into the working
        /// directory of the application. Alternatively, the default TagDefinitions.xml
        /// file can be obtained directly from the source code for this library.
        /// </para>
        /// </remarks>
        public static XDocument TagDefinitions
        {
            get
            {
                try
                {
                    return XDocument.Load(TagDefinitionsFileName);
                }
                catch
                {
                    Assembly pqdifAssembly = Assembly.GetExecutingAssembly();
                    using Stream resourceStream = pqdifAssembly.GetManifestResourceStream(TagDefinitionsFileName)!;
                    return XDocument.Load(resourceStream);
                }
            }
        }

        // Static Methods

        /// <summary>
        /// Gets the tag identified by the given globally unique identifier.
        /// </summary>
        /// <param name="id">The globally unique identifier for the tag to be retrieved.</param>
        /// <returns>The tag, if defined, or null if the tag is not found.</returns>
        /// <exception cref="InvalidDataException">Unable to refresh tags from TagDefinitions.xml.</exception>
        public static Tag? GetTag(Guid id)
        {
            if (TagLookup is null)
                RefreshTags(TagDefinitions);

            if (TagLookup is null)
                throw new InvalidDataException($"Unable to refresh tags from {TagDefinitionsFileName}.");

            if (!TagLookup.TryGetValue(id, out Tag? tag))
                return null;

            return tag;
        }

        /// <summary>
        /// Generates a list of tags from the given XML document.
        /// </summary>
        /// <param name="doc">The XML document containing the tag definitions.</param>
        /// <returns>The list of tags as defined by the given XML document.</returns>
        public static List<Tag> GenerateTags(XDocument doc)
        {
            return doc.Descendants("tag")
                .Select(element => new Tag(doc, element))
                .ToList();
        }

        /// <summary>
        /// Refreshes the cache of tags used to return
        /// tags from the <see cref="GetTag(Guid)"/> method.
        /// </summary>
        /// <param name="doc">The XML document containing the tag definitions.</param>
        public static void RefreshTags(XDocument doc) => TagLookup = GenerateTags(doc).ToDictionary(t => t.ID);

        // Attempts to parse the element type via the ElementType enumeration.
        // Failing that, attempts to parse it as an integer instead.
        private static ElementType GetElementType(XElement element)
        {
            string? elementTypeName = (string?)element.Element("elementType");

            if (Enum.TryParse(elementTypeName, out ElementType elementType))
                return elementType;

            if (byte.TryParse(elementTypeName, out byte elementTypeID))
                return (ElementType)elementTypeID;

            return 0;
        }

        // Attempts to parse the physical type via the PhysicalType enumeration.
        // Failing that, attempts to parse it as an integer instead.
        private static PhysicalType GetPhysicalType(XElement element)
        {
            string? physicalTypeName = (string?)element.Element("physicalType");

            if (Enum.TryParse(physicalTypeName, out PhysicalType physicalType))
                return physicalType;

            if (byte.TryParse(physicalTypeName, out byte physicalTypeID))
                return (PhysicalType)physicalTypeID;

            return 0;
        }

        #endregion
    }
}
