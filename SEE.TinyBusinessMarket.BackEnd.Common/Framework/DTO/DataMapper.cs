//-----------------------------------------------------------------------
// <copyright file="AutoMapper.cs" company="SEE Software">
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
    using System.Collections;

    public class DataMapper
    {
        public static TEntity MapTo<TModel, TEntity>(TModel source, TEntity target)
        {
            var sourceProperties = source.GetType().GetProperties().Where(x => x.PropertyType.GetInterfaces().Any(y => y == typeof(IData)));
            var targetProperties = typeof(TEntity).GetProperties().Where(x => x.CanWrite && !x.PropertyType.GetInterfaces().Any(y => y == typeof(ICollection)) && sourceProperties.Any(y => y.Name == x.Name)).ToList();
            foreach (var sourceProp in sourceProperties)
            {
                var targetProp = targetProperties.FirstOrDefault(x => x.Name == sourceProp.Name);
                if (targetProp != null && (targetProp.PropertyType.GetTypeInfo().IsValueType || targetProp.PropertyType == typeof(string)))
                {
                    var data = sourceProp.GetValue(source) as IData;
                    if (data != null)
                    {
                        targetProp.SetValue(target, data.GetValue());
                    }
                }
            }
            return target;
        }

        public static TEntity MapTo<TModel, TEntity>(TModel source, TEntity target, Action<TEntity> afterMappingAction)
        {
            MapTo(source, target);
            afterMappingAction(target);
            return target;
        }

        public static void MapFrom<TEntity, TModel>(TEntity source, TModel target)
        {
            var targetProperties = target.GetType().GetProperties().Where(x => x.PropertyType.GetInterfaces().Any(y => y == typeof(IData)));
            var sourceProperties = typeof(TEntity).GetProperties().Where(x => x.CanWrite && !x.PropertyType.GetInterfaces().Any(y => y == typeof(ICollection)) && targetProperties.Any(y => y.Name == x.Name)).ToList();
            foreach (var targetProp in targetProperties)
            {
                var sourceProp = sourceProperties.FirstOrDefault(x => x.Name == targetProp.Name);
                if (sourceProp != null && (sourceProp.PropertyType.GetTypeInfo().IsValueType || sourceProp.PropertyType == typeof(string)))
                {
                    var data = sourceProp.GetValue(source);
                    if (data != null)
                    {
                        var field = targetProp.GetValue(target) as IData;
                        field.SetValue(data);
                    }
                }
            }
        }
    }
}
