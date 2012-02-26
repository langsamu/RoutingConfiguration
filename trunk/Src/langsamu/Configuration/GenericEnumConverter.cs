// <copyright file="GenericEnumConverter.cs" company="Samu Lang">
//      Copyright (c) 2012 Samu Lang
//
//      Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
//      The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
//      THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

[module: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "It's my name")]

namespace langsamu.Configuration
{
    using System;
    using System.ComponentModel;
    using System.Configuration;
    using System.Globalization;

    /// <summary>
    /// Converts between a string and an enumeration type.
    /// </summary>
    /// <typeparam name="T">The type of enumeration to convert.</typeparam>
    /// <remarks>Uses <see cref="System.ComponentModel.GenericEnumConverter"/> to convert the values.</remarks>
    public class GenericEnumConverter<T> : ConfigurationConverterBase
    {
        /// <summary>
        /// Converts a <see cref="System.String"/> to an <see cref="System.Enum"/> type.
        /// </summary>
        /// <param name="context">The <see cref="System.ComponentModel.ITypeDescriptorContext"/> object used for type conversions.</param>
        /// <param name="culture">The <see cref="System.Globalization.CultureInfo"/> object used during conversion.</param>
        /// <param name="value">The <see cref="System.String"/> object to convert.</param>
        /// <returns>The <see cref="System.Enum"/> type that represents the data parameter.</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return new GenericEnumConverter(typeof(T)).ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Converts an <see cref="System.Enum"/> type to a <see cref="System.String"/> value.
        /// </summary>
        /// <param name="context">The <see cref="System.ComponentModel.ITypeDescriptorContext"/> object used for type conversions.</param>
        /// <param name="culture">The <see cref="System.Globalization.CultureInfo"/> object used during conversion.</param>
        /// <param name="value">The value to convert to.</param>
        /// <param name="destinationType">The type to convert to.</param>
        /// <returns>The <see cref="System.String"/> that represents the value parameter.</returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return new GenericEnumConverter(typeof(T)).ConvertTo(context, culture, value, destinationType);
        }
    }
}
