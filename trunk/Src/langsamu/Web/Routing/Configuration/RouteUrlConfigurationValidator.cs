// <copyright file="RouteUrlConfigurationValidator.cs" company="Samu Lang">
//      Copyright (c) 2012 Samu Lang
//
//      Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
//      The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
//      THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

[module: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "It's my name")]

namespace langsamu.Web.Routing.Configuration
{
    using System;
    using System.Configuration;

    /// <summary>
    /// Validates that a configuration attribute contains a valid route url.
    /// </summary>
    public class RouteUrlConfigurationValidator : ConfigurationValidatorBase
    {
        /// <summary>
        /// Determines whether the validator can validate the type of attribute.
        /// </summary>
        /// <param name="type">The type of attribute.</param>
        /// <returns>True if the attribute is a string, otherwise false.</returns>
        public override bool CanValidate(Type type)
        {
            return type == typeof(string);
        }

        /// <summary>
        /// Determines whether the value is a valid route url.
        /// </summary>
        /// <param name="value">The value of an object.</param>
        public override void Validate(object value)
        {
            try
            {
                RouteParser.Validate(value as string);
            }
            catch (Exception e)
            {
                throw new ConfigurationErrorsException(e.InnerException.Message, e);
            }
        }
    }
}
