using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Resources;
using System.Web.Mvc;

namespace Griffin.MVC
{
    internal class CustomDataAnnotationsModelValidator : DataAnnotationsModelValidator
    {
        private static Func<ValidationAttribute, string> _resourceNameFunc = attr => attr.GetType().Name;

        public CustomDataAnnotationsModelValidator(ModelMetadata metadata, ControllerContext context,
                                                   ValidationAttribute attribute)
            : base(metadata, context, attribute)
        {
            if (Attribute.ErrorMessageResourceType == null)
            {
                Attribute.ErrorMessageResourceType = ResourceType;
            }

            if (Attribute.ErrorMessageResourceName == null)
            {
                Attribute.ErrorMessageResourceName = ResourceNameFunc(Attribute);
            }
        }

        /// <summary>
        /// The type of the resource which holds the error messqages
        /// </summary>
        public static Type ResourceType { get; set; }

        /// <summary>
        /// Function to get the ErrorMessageResourceName from the Attribute
        /// </summary>
        public static Func<ValidationAttribute, string> ResourceNameFunc
        {
            get { return _resourceNameFunc; }
            set { _resourceNameFunc = value; }
        }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            return base.Validate(container);
        }

        #region Nested type: Temp

        private class Temp : ResourceManager
        {
            public override string GetString(string name)
            {
                return base.GetString(name);
            }

            protected override string GetResourceFileName(CultureInfo culture)
            {
                return base.GetResourceFileName(culture);
            }
        }

        #endregion
    }
}