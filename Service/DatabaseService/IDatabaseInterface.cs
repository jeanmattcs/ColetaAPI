using ColetaAPI.DTOs;
using ColetaAPI.Models;

namespace ColetaAPI.Service.DatabaseService
{
    public interface IDatabaseInterface
    {
        // Pegar todos os dados do banco de dados
        Task<ServiceResponse<DatabaseDto>> GetAllData();
    }
}

