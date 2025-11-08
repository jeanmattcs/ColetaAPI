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

        // Pegar todos os dados do banco de dados
        public async Task<ServiceResponse<DatabaseDto>> GetAllData()
        {
            ServiceResponse<DatabaseDto> serviceResponse = new ServiceResponse<DatabaseDto>();
            try
            {
                var databaseDto = new DatabaseDto
                {
                    Localizacoes = await _context.Localizacoes
                        .Include(l => l.Coletas)
                        .ToListAsync(),
                    Coletas = await _context.Coletas
                        .Include(c => c.Localizacao)
                        .ToListAsync()
                };

                serviceResponse.Data = databaseDto;
                
                if (databaseDto.Localizacoes.Any() || databaseDto.Coletas.Any())
                {
                    serviceResponse.Message = $"Dados encontrados: {databaseDto.Localizacoes.Count} localizações e {databaseDto.Coletas.Count} coletas";
                }
                else
                {
                    serviceResponse.Message = "Nenhum dado encontrado no banco de dados";
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

