using ColetaAPI.Models;

namespace ColetaAPI.DTOs
{
    public class DatabaseDto
    {
        public List<LocationModel> Locations { get; set; } = new List<LocationModel>();
        public List<CollectionModel> Collections { get; set; } = new List<CollectionModel>();
    }
}


