using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColetaAPI.Models
{
    public class CollectionModel
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "LocationId is required")]
        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Collected { get; set; } = false;
        public DateTime DateOfCreation { get; set; } = DateTime.Now.ToLocalTime();
        public LocationModel Location { get; set; }
    }
}
