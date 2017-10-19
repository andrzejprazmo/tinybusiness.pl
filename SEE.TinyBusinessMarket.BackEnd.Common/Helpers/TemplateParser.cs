//-----------------------------------------------------------------------
// <copyright file="TemplateParser.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.TinyBusinessMarket.BackEnd.Common.Helpers
{
	using System;
	using System.Collections.Generic;
	using System.Text;
    using System.Reflection;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.ComponentModel.DataAnnotations;

    public class TemplateParser
    {
        public static string Inject(string template, object model)
        {
            return Inject(template, model, string.Empty);
        }

        private static string Inject(string template, object model, string className)
        {
            var modelProperties = model.GetType().GetProperties().ToList();
            foreach (var mp in modelProperties)
            {
                string propertyName = string.IsNullOrWhiteSpace(className) ? mp.Name : $"{className}.{mp.Name}";
                if (mp != null && (mp.PropertyType.GetTypeInfo().IsValueType || mp.PropertyType == typeof(string)))
                {
                    // Primitive
                    string pattern = string.Format("{{{{ *{0} *}}}}", propertyName);
                    object value = mp.GetValue(model);
                    if (value != null)
                    {
                        string data = GetData(mp, value);
                        template = Regex.Replace(template, pattern, data);
                    }
                }
                else
                {
                    // Complex
                    var complexValue = mp.GetValue(model);
                    if (complexValue != null)
                    {
                        template = Inject(template, complexValue, propertyName);
                    }
                }
            }
            return template;
        }

        private static string GetData(PropertyInfo pInfo, object value)
        {
            var displayFormatAttr = pInfo.GetCustomAttribute(typeof(DisplayFormatAttribute)) as DisplayFormatAttribute;
            if (displayFormatAttr != null)
            {
                switch (pInfo.PropertyType.Name)
                {
                    case nameof(DateTime):
                        return ((DateTime)value).ToString(displayFormatAttr.DataFormatString);
                    case nameof(Decimal):
                        return ((decimal)value).ToString(displayFormatAttr.DataFormatString);
                    case nameof(Int32):
                        return ((Int32)value).ToString(displayFormatAttr.DataFormatString);
                    case nameof(Int64):
                        return ((Int64)value).ToString(displayFormatAttr.DataFormatString);
                    default:
                        return value.ToString();
                }
            }
            return value.ToString();
        }
    }
}
