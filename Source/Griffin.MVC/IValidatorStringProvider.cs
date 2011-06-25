using System.Globalization;

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

        string GetResourceFileName(CultureInfo culture);
    }
}