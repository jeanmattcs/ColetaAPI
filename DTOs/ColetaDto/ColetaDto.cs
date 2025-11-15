using System.ComponentModel.DataAnnotations;

namespace ColetaAPI.DTOs
{
    public class CreateCollectionDto
    {
        [Required(ErrorMessage = "LocationId is required")]
        public int LocationId { get; set; }

        [Required(ErrorMessage = "OrderDate is required")]
        public DateTime OrderDate { get; set; }

        public bool Collected { get; set; } = false;
    }

    public class UpdateCollectionDto
    {
        [Required(ErrorMessage = "ID is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "LocationId is required")]
        public int LocationId { get; set; }

        [Required(ErrorMessage = "OrderDate is required")]
        public DateTime OrderDate { get; set; }

        public bool Collected { get; set; }
    }

    public class CollectionResponseDto
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Collected { get; set; }
        public DateTime DateOfCreation { get; set; }
        public LocationSimpleDto? Location { get; set; }
    }

    public class LocationSimpleDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime DateOfCreation { get; set; }
    }
}


