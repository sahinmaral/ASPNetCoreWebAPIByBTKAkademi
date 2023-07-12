using StoreApp.Entities.Models.Abstract;

using System.Dynamic;

namespace StoreApp.Services.Abstract
{
    public interface IDataShaper<T> where T:class
    {
        IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities,string? fieldsString);
        ExpandoObject ShapeData(T entity, string? fieldsString);
    }
}
