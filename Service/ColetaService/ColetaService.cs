using ColetaAPI.DataContext;
using ColetaAPI.DTOs;
using ColetaAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace ColetaAPI.Service.ColetaService
{
    public class CollectionService : ICollectionInterface
    {
        // Dependency Injection
        private readonly ApplicationsDbContext _context;

        // Constructor
        public CollectionService(ApplicationsDbContext context)
        {
            _context = context;
        }

        // Helper method to map CollectionModel to CollectionResponseDto
        private CollectionResponseDto MapToResponseDto(CollectionModel collection)
        {
            return new CollectionResponseDto
            {
                Id = collection.ID,
                LocationId = collection.LocationId,
                OrderDate = collection.OrderDate,
                Collected = collection.Collected,
                DateOfCreation = collection.DateOfCreation,
                Location = collection.Location != null ? new LocationSimpleDto
                {
                    Id = collection.Location.ID,
                    Description = collection.Location.Description,
                    DateOfCreation = collection.Location.DateOfCreation
                } : null
            };
        }
        // Add Collection
        public async Task<ServiceResponse<List<CollectionResponseDto>>> AddCollection(CollectionModel collection)
        {
            ServiceResponse<List<CollectionResponseDto>> ServiceResponse = new ServiceResponse<List<CollectionResponseDto>>();
            try
            {
                if (collection.LocationId <= 0)
                {
                    ServiceResponse.Data = null;
                    ServiceResponse.Message = "LocationId is required and must be valid";
                    ServiceResponse.Success = false;
                    return ServiceResponse;
                }

                // Validate if the location exists
                var locationExists = await _context.Locations.AnyAsync(l => l.ID == collection.LocationId);
                if (!locationExists)
                {
                    ServiceResponse.Data = null;
                    ServiceResponse.Message = $"Location with ID {collection.LocationId} does not exist";
                    ServiceResponse.Success = false;
                    return ServiceResponse;
                }

                _context.Add(collection);
                await _context.SaveChangesAsync();

                var collections = await _context.Collections.Include(c => c.Location).ToListAsync();
                ServiceResponse.Data = collections.Select(c => MapToResponseDto(c)).ToList();
                ServiceResponse.Message = "Collection added successfully";
            }
            catch (Exception ex)
            {
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;
            }
            return ServiceResponse;
        }
        // Get all Collections
        public async Task<ServiceResponse<List<CollectionResponseDto>>> GetCollection()
        {
            ServiceResponse<List<CollectionResponseDto>> ServiceResponse = new ServiceResponse<List<CollectionResponseDto>>();
            try
            {
                var collections = await _context.Collections.Include(c => c.Location).ToListAsync();
                ServiceResponse.Data = collections.Select(c => MapToResponseDto(c)).ToList();

                if (ServiceResponse.Data.Any())
                {
                    ServiceResponse.Message = "Collections found";
                }
                else
                {
                    ServiceResponse.Message = "No data found";
                }
            }
            catch (Exception ex)
            {
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;
            }
            return ServiceResponse;
        }
        // Get Collection by ID
        public async Task<ServiceResponse<CollectionResponseDto>> GetSingleCollection(int id)
        {
            ServiceResponse<CollectionResponseDto> ServiceResponse = new ServiceResponse<CollectionResponseDto>();
            try
            {
                CollectionModel collection = _context.Collections.Include(c => c.Location).FirstOrDefault(c => c.ID == id);

                if (collection == null)
                {
                    ServiceResponse.Message = "Collection not found";
                    ServiceResponse.Success = false;
                }
                else
                {
                    ServiceResponse.Data = MapToResponseDto(collection);
                    ServiceResponse.Message = "Collection found";
                }
            }
            catch (Exception ex)
            {
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;
            }
            return ServiceResponse;
        }
        // Update Collection
        public async Task<ServiceResponse<CollectionResponseDto>> UpdateCollection(CollectionModel collection)
        {
            ServiceResponse<CollectionResponseDto> ServiceResponse = new ServiceResponse<CollectionResponseDto>();
            try
            {
                if (collection.LocationId <= 0)
                {
                    ServiceResponse.Data = null;
                    ServiceResponse.Message = "LocationId is required and must be valid";
                    ServiceResponse.Success = false;
                    return ServiceResponse;
                }

                // Validate if the location exists
                var locationExists = await _context.Locations.AnyAsync(l => l.ID == collection.LocationId);
                if (!locationExists)
                {
                    ServiceResponse.Data = null;
                    ServiceResponse.Message = $"Location with ID {collection.LocationId} does not exist";
                    ServiceResponse.Success = false;
                    return ServiceResponse;
                }

                _context.Update(collection);
                await _context.SaveChangesAsync();

                // Reload the collection with the location included
                var updatedCollection = await _context.Collections
                    .Include(c => c.Location)
                    .FirstOrDefaultAsync(c => c.ID == collection.ID);

                ServiceResponse.Data = MapToResponseDto(updatedCollection);
                ServiceResponse.Message = "Collection updated successfully";
            }
            catch (Exception ex)
            {
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;
            }
            return ServiceResponse;
        }
        // Delete Collection
        public async Task<ServiceResponse<List<CollectionResponseDto>>> DeleteCollection(int id)
        {
            ServiceResponse<List<CollectionResponseDto>> ServiceResponse = new ServiceResponse<List<CollectionResponseDto>>();
            try
            {
                CollectionModel collection = _context.Collections.FirstOrDefault(c => c.ID == id);
                if (collection == null)
                {
                    ServiceResponse.Message = "Collection not found";
                    ServiceResponse.Success = false;
                    return ServiceResponse;
                }
                _context.Collections.Remove(collection);
                await _context.SaveChangesAsync();

                var collections = await _context.Collections.Include(c => c.Location).ToListAsync();
                ServiceResponse.Data = collections.Select(c => MapToResponseDto(c)).ToList();
                ServiceResponse.Message = "Collection deleted successfully";
            }
            catch (Exception ex)
            {
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;
            }
            return ServiceResponse;
        }
    }
}
