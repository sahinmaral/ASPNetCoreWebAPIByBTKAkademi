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

        public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string? fieldsString)
        {
            IEnumerable<PropertyInfo> requiredProperties = GetRequiredProperties(fieldsString);

            return FetchData(entities, requiredProperties);
        }

        public ExpandoObject ShapeData(T entity, string? fieldsString)
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
        
        private ExpandoObject FetchDataForEntity(T entity,IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedObject = new ExpandoObject();
            foreach (var property in requiredProperties)
            {
                var objectPropertyValue = property.GetValue(entity);
                shapedObject.TryAdd(property.Name,objectPropertyValue);
            }
            return shapedObject;
        }

        private IEnumerable<ExpandoObject> FetchData(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedData = new List<ExpandoObject>();
            foreach (var entity in entities)
            {
                var shapedObject = FetchDataForEntity(entity, requiredProperties);
                shapedData.Add(shapedObject);
            }
            return shapedData;
        }
    }
}
