using StoreApp.Entities.Models.Abstract;

namespace StoreApp.Entities.Models
{
    public class Book : IEntity
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public decimal Price { get; set; }

        public Category? Category { get; set; }
        public int CategoryId { get; set; }
    }
}
