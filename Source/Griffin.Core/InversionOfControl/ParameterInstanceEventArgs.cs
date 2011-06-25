using System;

namespace Griffin.Core.InversionOfControl
{
    public class ParameterInstanceEventArgs : EventArgs
    {
        public ParameterInstanceEventArgs(Type typeToCreate, Type parameterType, string parameterName)
        {
            InterfaceType = typeToCreate;
            ParameterType = parameterType;
            ParameterName = parameterName;
        }

        public object Instance { get; set; }

        /// <summary>
        /// Gets or sets interface type
        /// </summary>
        public Type InterfaceType { get; private set; }

        public string ParameterName { get; private set; }
        public Type ParameterType { get; private set; }
    }
}