using ColetaAPI.DTOs;
using ColetaAPI.Models;

namespace ColetaAPI.Service.DatabaseService
{
    public interface IDatabaseInterface
    {
        // Get all data from the database
        Task<ServiceResponse<DatabaseDto>> GetAllData();
    }
}

