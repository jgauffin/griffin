using System.Xml.Linq;

namespace Griffin.Core
{
    /// <summary>
    /// Extensions making it easier to work with XElement.
    /// </summary>
    public static class XElementExtensions
    {
        /// <summary>
        /// Return default value if the node or the attribute is null.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Real attribute value or default value if not specified.</returns>
        public static string AttributeValueOrDefault(this XElement node, string attributeName, string defaultValue)
        {
            if (node == null)
                return defaultValue;
            var item = node.Attribute(attributeName);
            return item == null ? defaultValue : (item.Value ?? defaultValue);
        }

        /// <summary>
        /// Return <c>string.Empty</c> if the node or the attribute is null.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>Attribute value if the node or attribute is specified; otherwise an empty string.</returns>
        public static string AttributeValueOrEmpty(this XElement node, string attributeName)
        {
            return node.AttributeValueOrDefault(attributeName, string.Empty);
        }

        /// <summary>
        /// Get's elements value or the specified defaul value
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Element value if element is found; otherwise <paramref name="defaultValue"/></returns>
        public static string ElementValueOrDefault(this XElement node, string elementName, string defaultValue)
        {
            var item = node.Element(elementName);
            return item == null ? defaultValue : item.Value.IsNullOrEmpty() ? defaultValue : item.Value;
        }

        /// <summary>
        /// Element value if element and value exists.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <returns></returns>
        /// <returns>Element value if element is found; otherwise <c>string.Empty</c>.</returns>
        public static string ElementValueOrEmpty(this XElement node, string elementName)
        {
            return node.ElementValueOrDefault(elementName, string.Empty);
        }

        /// <summary>
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        public static string ValueOrEmpty(this XElement node)
        {
            return node == null ? string.Empty : node.Value;
        }
        /// <summary>
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        public static string ValueOrEmpty(this XAttribute node)
        {
            return node == null ? string.Empty : node.Value;
        }
    }

}
