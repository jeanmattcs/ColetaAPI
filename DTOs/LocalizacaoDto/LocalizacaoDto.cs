using System.ComponentModel.DataAnnotations;

namespace ColetaAPI.DTOs
{
    public class CreateLocationDto
    {
        [Required(ErrorMessage = "Description is required")]
        [StringLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
        public string Description { get; set; }
    }

    public class UpdateLocationDto
    {
        [Required(ErrorMessage = "ID is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
        public string Description { get; set; }
    }
    public class LocationResponseDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreation { get; set; }
        public List<CollectionSimpleDto> Collections { get; set; } = new List<CollectionSimpleDto>();
    }

    public class CollectionSimpleDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Collected { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}


