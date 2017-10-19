//-----------------------------------------------------------------------
// <copyright file="AutoValidator.cs" company="SEE Software">
//     All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SEE.Framework.Core.DTO
{
	using System;
	using System.Collections.Generic;
	using System.Text;
    using System.Reflection;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.Resources;

    public class DataValidator
    {
        public static bool Validate<TModel>(TModel model) where TModel : class
        {
            var dataProperties = model.GetType().GetProperties().Where(p => p.PropertyType.GetTypeInfo().IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(Data<>)).ToList();
            foreach (var data in dataProperties)
            {
                var dataValue = data.GetValue(model) as IData;
                if (dataValue != null)
                {
                    dataValue.ErrorMessage = string.Empty;
                    var value = dataValue.GetValue();
                    // execute validator attrs
                    var attributes = data.GetCustomAttributes(typeof(ValidationAttribute), true);
                    foreach (var attr in attributes)
                    {
                        var validationAttribute = attr as ValidationAttribute;
                        if (validationAttribute != null)
                        {
                            bool isValid = validationAttribute.IsValid(value);
                            if (!isValid)
                            {
                                dataValue.ErrorMessage = GetErrorMessage(validationAttribute);
                            }
                        }
                    }
                }
            }
            return IsValid(model);
        }

        private static string GetErrorMessage(ValidationAttribute attr)
        {
            if (!string.IsNullOrWhiteSpace(attr.ErrorMessage))
            {
                return attr.ErrorMessage;
            }
            if (!string.IsNullOrWhiteSpace(attr.ErrorMessageResourceName) && attr.ErrorMessageResourceType != null)
            {
                var resource = new ResourceManager(attr.ErrorMessageResourceType);
                return resource.GetString(attr.ErrorMessageResourceName);
            }
            return "Bad field value.";
        }

        public static bool IsValid<TModel>(TModel model) where TModel : class
        {
            if (model == null)
            {
                return false;
            }
            var dataProperties = model.GetType().GetProperties().Where(x => x.PropertyType.GetInterfaces().Any(y => y == typeof(IData)));
            foreach (var data in dataProperties)
            {
                var dataValue = data.GetValue(model) as IData;
                if (dataValue != null && dataValue.HasError)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
