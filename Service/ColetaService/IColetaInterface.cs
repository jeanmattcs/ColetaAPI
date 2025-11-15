using ColetaAPI.DTOs;
using ColetaAPI.Models;

namespace ColetaAPI.Service.ColetaService
{
    public interface ICollectionInterface
    {
        // Get all Collections
        Task<ServiceResponse<List<CollectionResponseDto>>> GetCollection();
        Task<ServiceResponse<CollectionResponseDto>> GetSingleCollection(int id);
        Task<ServiceResponse<List<CollectionResponseDto>>> AddCollection(CollectionModel collection);
        Task<ServiceResponse<CollectionResponseDto>> UpdateCollection(CollectionModel collection);
        Task<ServiceResponse<List<CollectionResponseDto>>> DeleteCollection(int id);
    }
}
