﻿using StoreApp.Entities.Models.LinkModels;
using StoreApp.Services.Abstract;

using System.Dynamic;
using System.Reflection;

namespace StoreApp.Services
{
    public class DataShaper<T> : IDataShaper<T> where T:class
    {
        public PropertyInfo[] PropertyInfos { get; set; }

        public DataShaper()
        {
            PropertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        public IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities, string? fieldsString)
        {
            IEnumerable<PropertyInfo> requiredProperties = GetRequiredProperties(fieldsString);

            return FetchData(entities, requiredProperties);
        }

        public ShapedEntity ShapeData(T entity, string? fieldsString)
        {
            IEnumerable<PropertyInfo> requiredProperties = GetRequiredProperties(fieldsString);
            return FetchDataForEntity(entity, requiredProperties);
        }

        private IEnumerable<PropertyInfo> GetRequiredProperties(string? fieldsString)
        {
            var requiredFields = new List<PropertyInfo>();
            if (string.IsNullOrEmpty(fieldsString))
                requiredFields = PropertyInfos.ToList();
            else
            {
                var fields = fieldsString.Split(",",StringSplitOptions.RemoveEmptyEntries);

                foreach (var field in fields)
                {
                    var property = PropertyInfos.FirstOrDefault(pi => pi.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));
                    if (property is null) continue;
                    requiredFields.Add(property);
                }
            }

            return requiredFields;
        }
        
        private ShapedEntity FetchDataForEntity(T entity,IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedObject = new ShapedEntity();
            foreach (var property in requiredProperties)
            {
                var objectPropertyValue = property.GetValue(entity);
                shapedObject.Entity.TryAdd(property.Name,objectPropertyValue);
            }

            var objectProperty = entity.GetType().GetProperty("Id");
            shapedObject.Id = (int)objectProperty.GetValue(entity);

            return shapedObject;
        }

        private IEnumerable<ShapedEntity> FetchData(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedData = new List<ShapedEntity>();
            foreach (var entity in entities)
            {
                var shapedObject = FetchDataForEntity(entity, requiredProperties);
                shapedData.Add(shapedObject);
            }
            return shapedData;
        }
    }
}
