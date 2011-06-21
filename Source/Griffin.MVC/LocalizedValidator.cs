using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Griffin.MVC
{
    public class LocalizedValidator : IServiceProvider
    {
        public LocalizedValidator()
        {
            ValidationContext cts = new ValidationContext();
            
        }

        public object GetService(Type serviceType)
        {
            
        }
    }

}
