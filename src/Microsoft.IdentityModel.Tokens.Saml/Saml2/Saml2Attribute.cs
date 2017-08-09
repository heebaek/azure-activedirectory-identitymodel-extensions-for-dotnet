//------------------------------------------------------------------------------
//
// Copyright (c) Microsoft Corporation.
// All rights reserved.
//
// This code is licensed under the MIT License.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Xml;
using static Microsoft.IdentityModel.Logging.LogHelper;

namespace Microsoft.IdentityModel.Tokens.Saml2
{
    /// <summary>
    /// Represents the Attribute element specified in [Saml2Core, 2.7.3.1].
    /// </summary>
    public class Saml2Attribute
    {
        private string _attributeValueXsiType = System.Security.Claims.ClaimValueTypes.String;
        private string _friendlyName;
        private string _name;
        private Uri _nameFormat;

        /// <summary>
        /// Initializes a new instance of the Saml2Attribute class.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        public Saml2Attribute(string name)
            : this(name, (string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Saml2Attribute class.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        public Saml2Attribute(string name, string value)
            : this(name, new string[] { value })
        {
        }

        /// <summary>
        /// Initializes a new instance of the Saml2Attribute class.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="values">The collection of values that define the attribute.</param>
        public Saml2Attribute(string name, IEnumerable<string> values)
        {
            Name = name;
            if (values == null)
                throw LogArgumentNullException(nameof(values));

            Values = new List<string>(values);
        }

        /// <summary>
        /// Gets or sets a string that provides a more human-readable form of the attribute's 
        /// name. [Saml2Core, 2.7.3.1]
        /// </summary>
        public string FriendlyName
        {
            get => _friendlyName;
            set => _friendlyName = XmlUtil.NormalizeEmptyString(value);
        }

        /// <summary>
        /// Gets or sets the name of the attribute. [Saml2Core, 2.7.3.1]
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = string.IsNullOrEmpty(value)
                ? throw LogExceptionMessage(new ArgumentNullException(nameof(value))) : value;
        }

        /// <summary>
        /// Gets or sets a URI reference representing the classification of the attribute 
        /// name for the purposes of interpreting the name. [Saml2Core, 2.7.3.1]
        /// </summary>
        public Uri NameFormat
        {
            get => _nameFormat;
            set => _nameFormat = (value != null && !value.IsAbsoluteUri)
                ? throw LogExceptionMessage(new ArgumentException(nameof(value), FormatInvariant(LogMessages.IDX11300, value)))
                : value;
        }

        /// <summary>
        /// Gets or sets the string that represents the OriginalIssuer of the this SAML Attribute.
        /// </summary>
        public string OriginalIssuer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the xsi:type of the values contained in the SAML Attribute.
        /// </summary>
        public string AttributeValueXsiType
        {
            get => _attributeValueXsiType;
            set => _attributeValueXsiType = string.IsNullOrEmpty(value)
                ? throw LogArgumentNullException(nameof(value))
                : value;
        }

        /// <summary>
        /// Gets the values of the attribute.
        /// </summary>
        public ICollection<string> Values
        {
            get;
        }
    }
}