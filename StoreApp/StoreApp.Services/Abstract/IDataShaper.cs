using StoreApp.Entities.Models.Abstract;
using StoreApp.Entities.Models.LinkModels;

using System.Dynamic;

namespace StoreApp.Services.Abstract
{
    public interface IDataShaper<T> where T:class
    {
        IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities,string? fieldsString);
        ShapedEntity ShapeData(T entity, string? fieldsString);
    }
}
