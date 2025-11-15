using ColetaAPI.DataContext;
using ColetaAPI.DTOs;
using ColetaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ColetaAPI.Service.LocalizacaoService
{
    public class LocationService : ILocationInterface
    {
        // Dependency Injection
        private readonly ApplicationsDbContext _context;

        // Constructor
        public LocationService(ApplicationsDbContext context)
        {
            _context = context;
        }

        // Helper method to map LocationModel to LocationResponseDto
        private LocationResponseDto MapToResponseDto(LocationModel location)
        {
            return new LocationResponseDto
            {
                Id = location.ID,
                Description = location.Description,
                DateOfCreation = location.DateOfCreation,
                Collections = location.Collections?.Select(c => new CollectionSimpleDto
                {
                    Id = c.ID,
                    OrderDate = c.OrderDate,
                    Collected = c.Collected,
                    DateOfCreation = c.DateOfCreation
                }).ToList() ?? new List<CollectionSimpleDto>()
            };
        }

        // Add Location
        public async Task<ServiceResponse<List<LocationResponseDto>>> AddLocation(LocationModel location)
        {
            ServiceResponse<List<LocationResponseDto>> serviceResponse = new ServiceResponse<List<LocationResponseDto>>();
            try
            {
                if (string.IsNullOrWhiteSpace(location.Description))
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Description is required";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }

                _context.Add(location);
                await _context.SaveChangesAsync();

                var locations = await _context.Locations.Include(l => l.Collections).ToListAsync();
                serviceResponse.Data = locations.Select(l => MapToResponseDto(l)).ToList();
                serviceResponse.Message = "Location added successfully";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        // Get all Locations
        public async Task<ServiceResponse<List<LocationResponseDto>>> GetLocations()
        {
            ServiceResponse<List<LocationResponseDto>> serviceResponse = new ServiceResponse<List<LocationResponseDto>>();
            try
            {
                var locations = await _context.Locations
                    .Include(l => l.Collections)
                    .ToListAsync();

                serviceResponse.Data = locations.Select(l => MapToResponseDto(l)).ToList();

                if (serviceResponse.Data.Any())
                {
                    serviceResponse.Message = "Locations found";
                }
                else
                {
                    serviceResponse.Message = "No locations found";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        // Get Location by ID
        public async Task<ServiceResponse<LocationResponseDto>> GetLocationById(int id)
        {
            ServiceResponse<LocationResponseDto> serviceResponse = new ServiceResponse<LocationResponseDto>();
            try
            {
                LocationModel location = await _context.Locations
                    .Include(l => l.Collections)
                    .FirstOrDefaultAsync(l => l.ID == id);

                if (location == null)
                {
                    serviceResponse.Message = "Location not found";
                    serviceResponse.Success = false;
                }
                else
                {
                    serviceResponse.Data = MapToResponseDto(location);
                    serviceResponse.Message = "Location found";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        // Update Location
        public async Task<ServiceResponse<LocationResponseDto>> UpdateLocation(LocationModel location)
        {
            ServiceResponse<LocationResponseDto> serviceResponse = new ServiceResponse<LocationResponseDto>();
            try
            {
                if (string.IsNullOrWhiteSpace(location.Description))
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Description is required";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }

                _context.Update(location);
                await _context.SaveChangesAsync();

                // Reload the location with the collections included
                var updatedLocation = await _context.Locations
                    .Include(l => l.Collections)
                    .FirstOrDefaultAsync(l => l.ID == location.ID);

                serviceResponse.Data = MapToResponseDto(updatedLocation);
                serviceResponse.Message = "Location updated successfully";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        // Delete Location
        public async Task<ServiceResponse<List<LocationResponseDto>>> DeleteLocation(int id)
        {
            ServiceResponse<List<LocationResponseDto>> serviceResponse = new ServiceResponse<List<LocationResponseDto>>();
            try
            {
                LocationModel location = _context.Locations.FirstOrDefault(l => l.ID == id);
                if (location == null)
                {
                    serviceResponse.Message = "Location not found";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }

                _context.Locations.Remove(location);
                await _context.SaveChangesAsync();

                var locations = await _context.Locations.Include(l => l.Collections).ToListAsync();
                serviceResponse.Data = locations.Select(l => MapToResponseDto(l)).ToList();
                serviceResponse.Message = "Location deleted successfully";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}

