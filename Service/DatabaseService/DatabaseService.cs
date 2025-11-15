using ColetaAPI.DataContext;
using ColetaAPI.DTOs;
using ColetaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ColetaAPI.Service.DatabaseService
{
    public class DatabaseService : IDatabaseInterface
    {
        // Dependency Injection
        private readonly ApplicationsDbContext _context;

        // Constructor
        public DatabaseService(ApplicationsDbContext context)
        {
            _context = context;
        }

        // Get all data from the database
        public async Task<ServiceResponse<DatabaseDto>> GetAllData()
        {
            ServiceResponse<DatabaseDto> serviceResponse = new ServiceResponse<DatabaseDto>();
            try
            {
                var databaseDto = new DatabaseDto
                {
                    Locations = await _context.Locations
                        .Include(l => l.Collections)
                        .ToListAsync(),
                    Collections = await _context.Collections
                        .Include(c => c.Location)
                        .ToListAsync()
                };

                serviceResponse.Data = databaseDto;

                if (databaseDto.Locations.Any() || databaseDto.Collections.Any())
                {
                    serviceResponse.Message = $"Data found: {databaseDto.Locations.Count} locations and {databaseDto.Collections.Count} collections";
                }
                else
                {
                    serviceResponse.Message = "No data found in the database";
                }
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

