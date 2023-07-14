using StoreApp.Entities.Models.Abstract;

namespace StoreApp.Entities.Models
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
