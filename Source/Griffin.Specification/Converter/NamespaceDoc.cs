using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Griffin.Converter
{
    /// <summary>
    /// Converter namespace is used to create an abstraction between converters, mappers and your code.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The purpose is not only to get a layer that can do simple conversions like <c>string</c> to <c>DateTime</c>, but
    /// to be able to provide conversions between different objects like an domain model to a view model.
    /// </para>
    /// <para>
    /// Look at the <c>Griffin.Converter.AutoMapper</c> project to see how external libraries can be integrated 
    /// into the converter layer.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// <![CDATA[
    /// 
    /// // use it for more complex conversions
    /// var viewUsers = converterService.ConvertAll<EntityUser, UserViewModel>(users);
    /// 
    /// // or simple ones.
    /// var myDate = converterService.Convert<string, DateTime>("2011-02-10");
    /// ]]>
    /// </code>
    /// </example>
    public class NamespaceDoc
    {
    }
}
