namespace StoreApp.Entities.Models.Exceptions
{
    public sealed class BookNotFoundException : NotFoundException
    {
        public BookNotFoundException(int id) : base($"Book with {id} could not found")
        {
        }

    }
}
