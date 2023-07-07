using BookDemoWebAPI.Models;

namespace BookDemoWebAPI.Data
{
    public static class InMemoryContext
    {
        public static List<Book> Books { get; set; }
        static InMemoryContext()
        {
            Books = new List<Book>()
            {
                new Book() {Id = 1,Title = "Karagoz ve Hacivat",Price= 75},
                new Book() {Id = 2,Title = "Mesnevi",Price= 150},
                new Book() {Id = 3,Title = "Dede Korkut",Price= 75},
            };
        }
    }
}
