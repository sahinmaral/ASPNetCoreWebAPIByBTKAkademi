using System.ComponentModel.DataAnnotations;

namespace StoreApp.Entities.DTOs
{
    public abstract record BookDtoForManipulation
    {
        [Required(ErrorMessage = "Title is required")]
        [MinLength(2,ErrorMessage = "Minimum length of title must be 2")]
        [MaxLength(50, ErrorMessage = "Maximum length of title must be 50")]
        public string Title { get; init; }

        [Required(ErrorMessage = "Price is required")]
        [Range(10,1000,ErrorMessage = "Range of Price must between 10 and 100")]
        public decimal Price { get; set; }
    }
}
