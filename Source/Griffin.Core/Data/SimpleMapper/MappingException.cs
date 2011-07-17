using System;

namespace Griffin.Core.Data.SimpleMapper
{
   public class MappingException : Exception
    {
       public Type MappedType { get; set; }
       public string PropertyName { get; set; }

       public MappingException(string message, Type mappedType, string propertyName)
            : base(message)
       {
           MappedType = mappedType;
           PropertyName = propertyName;
       }
    }
}
