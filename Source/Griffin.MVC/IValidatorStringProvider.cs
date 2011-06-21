using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Griffin.MVC
{
    public interface IValidatorStringProvider
    {
        /// <summary>
        /// Get the name of a resource to validate
        /// </summary>
        /// <param name="name">Name of string</param>
        /// <returns></returns>
        string GetString(string name);
        string GetResourceFileName(System.Globalization.CultureInfo culture);
    }
}
