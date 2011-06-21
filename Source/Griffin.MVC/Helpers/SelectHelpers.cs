using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Griffin.MVC.Helpers
{
    public static class SelectExtensions
    {

        public static MvcHtmlString EnumDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                           Expression<Func<TModel, TProperty>>
                                                                               expression) where TModel : class
        {
            string inputName = Helpers.GetInputName(expression);
            TProperty value = htmlHelper.ViewData.Model == null
                                  ? default(TProperty)
                                  : expression.Compile()(htmlHelper.ViewData.Model);

            return htmlHelper.DropDownList(inputName, ToSelectList(typeof (TProperty), value.ToString()));
        }

        public static IEnumerable<SelectListItem> ToSelectItems<T>(this IEnumerable<T> items, Func<T, string> valueSelector, Func<T, string> textSelector)
        {
            return items.Select(item => new SelectListItem
            {
                Text = textSelector(item),
                Value = valueSelector(item)
            }).ToList();
        }


        public static SelectList ToSelectList(Type enumType, string selectedItem)
        {
            var items = new List<SelectListItem>();
            foreach (object item in Enum.GetValues(enumType))
            {
                FieldInfo fi = enumType.GetField(item.ToString());
                object attribute = fi.GetCustomAttributes(typeof (DescriptionAttribute), true).FirstOrDefault();
                string title = attribute == null ? item.ToString() : ((DescriptionAttribute) attribute).Description;
                var listItem = new SelectListItem
                                   {
                                       Value = ((int) item).ToString(),
                                       Text = title,
                                       Selected = selectedItem == ((int) item).ToString()
                                   };
                items.Add(listItem);
            }

            return new SelectList(items, "Value", "Text");
        }
    }
}