using ColetaAPI.DTOs;
using ColetaAPI.Models;

namespace ColetaAPI.Service.LocalizacaoService
{
    public interface ILocationInterface
    {
        // Get all Locations
        Task<ServiceResponse<List<LocationResponseDto>>> GetLocations();

        // Get Location by ID
        Task<ServiceResponse<LocationResponseDto>> GetLocationById(int id);

        // Add Location
        Task<ServiceResponse<List<LocationResponseDto>>> AddLocation(LocationModel location);

        // Update Location
        Task<ServiceResponse<LocationResponseDto>> UpdateLocation(LocationModel location);

        // Delete Location
        Task<ServiceResponse<List<LocationResponseDto>>> DeleteLocation(int id);
    }
}

