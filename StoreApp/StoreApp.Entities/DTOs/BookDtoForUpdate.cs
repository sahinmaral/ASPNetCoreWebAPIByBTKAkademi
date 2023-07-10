using System.ComponentModel.DataAnnotations;

namespace StoreApp.Entities.DTOs
{
    public record BookDtoForUpdate : BookDtoForManipulation
    {
        [Required]
        public int Id { get; init; }
    }
    
}
