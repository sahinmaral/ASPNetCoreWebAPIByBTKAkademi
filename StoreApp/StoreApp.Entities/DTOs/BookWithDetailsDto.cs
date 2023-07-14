namespace StoreApp.Entities.DTOs
{
    public record BookWithDetailsDto: BookDto
    {
        public string CategoryName { get; set; }
    }
}
