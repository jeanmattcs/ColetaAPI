using System.ComponentModel.DataAnnotations;

namespace ColetaAPI.Models
{
    public class LocationModel
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
        public string Description { get; set; }

        public DateTime DateOfCreation { get; set; } = DateTime.Now.ToLocalTime();

        public ICollection<CollectionModel> Collections { get; set; } = new List<CollectionModel>();
    }
}

