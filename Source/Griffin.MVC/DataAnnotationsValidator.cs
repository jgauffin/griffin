using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Text;
using System.Web.Mvc;

namespace Griffin.MVC
{
    class CustomDataAnnotationsModelValidator : DataAnnotationsModelValidator
    {
        private class Temp : ResourceManager
        {
            public override string GetString(string name)
            {
                return base.GetString(name);
            }
            protected override string GetResourceFileName(System.Globalization.CultureInfo culture)
            {
                return base.GetResourceFileName(culture);
            }
        }
        public CustomDataAnnotationsModelValidator(ModelMetadata metadata, ControllerContext context, ValidationAttribute attribute)
            : base(metadata, context, attribute)
        {
            if (Attribute.ErrorMessageResourceType == null)
            {
                this.Attribute.ErrorMessageResourceType = ResourceType;
            }

            if (Attribute.ErrorMessageResourceName == null)
            {
                this.Attribute.ErrorMessageResourceName = ResourceNameFunc(this.Attribute);
            }
        }
        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            return base.Validate(container);
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
        private static Func<ValidationAttribute, string> _resourceNameFunc = attr => attr.GetType().Name;

    }
}
