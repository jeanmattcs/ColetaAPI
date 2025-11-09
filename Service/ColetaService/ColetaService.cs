using ColetaAPI.DataContext;
using ColetaAPI.DTOs;
using ColetaAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace ColetaAPI.Service.ColetaService
{
    public class ColetaService : IColetaInterface
    {
        // Dependency Injection
        private readonly ApplicationsDbContext _context;

        // Constructor
        public ColetaService(ApplicationsDbContext context)
        {
            _context = context;
        }

        // Método auxiliar para mapear ColetaModel para ColetaResponseDto
        private ColetaResponseDto MapToResponseDto(ColetaModel coleta)
        {
            return new ColetaResponseDto
            {
                Id = coleta.ID,
                LocalizacaoId = coleta.LocalizacaoId,
                OrderDate = coleta.OrderDate,
                Collected = coleta.Collected,
                DateOfCreation = coleta.DateOfCreation,
                Localizacao = coleta.Localizacao != null ? new LocalizacaoSimpleDto
                {
                    Id = coleta.Localizacao.ID,
                    Descricao = coleta.Localizacao.Descricao,
                    DateOfCreation = coleta.Localizacao.DateOfCreation
                } : null
            };
        }
        // Adicionar Coleta
        public async Task<ServiceResponse<List<ColetaResponseDto>>> AddColeta(ColetaModel coleta)
        {
            ServiceResponse<List<ColetaResponseDto>> ServiceResponse = new ServiceResponse<List<ColetaResponseDto>>();
            try
            {
                if (coleta.LocalizacaoId <= 0)
                {
                    ServiceResponse.Data = null;
                    ServiceResponse.Message = "LocalizacaoId é obrigatório e deve ser válido";
                    ServiceResponse.Success = false;
                    return ServiceResponse;
                }

                // Validar se a localização existe
                var localizacaoExiste = await _context.Localizacoes.AnyAsync(l => l.ID == coleta.LocalizacaoId);
                if (!localizacaoExiste)
                {
                    ServiceResponse.Data = null;
                    ServiceResponse.Message = $"Localização com ID {coleta.LocalizacaoId} não existe";
                    ServiceResponse.Success = false;
                    return ServiceResponse;
                }

                _context.Add(coleta);
                await _context.SaveChangesAsync();

                var coletas = await _context.Coletas.Include(c => c.Localizacao).ToListAsync();
                ServiceResponse.Data = coletas.Select(c => MapToResponseDto(c)).ToList();
                ServiceResponse.Message = "Coleta adicionada com sucesso";
            }
            catch (Exception ex)
            {
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;
            }
            return ServiceResponse;
        }
        // Pegar todas as Coletas
        public async Task<ServiceResponse<List<ColetaResponseDto>>> GetColeta()
        {
            ServiceResponse<List<ColetaResponseDto>> ServiceResponse = new ServiceResponse<List<ColetaResponseDto>>();
            try
            {
                var coletas = await _context.Coletas.Include(c => c.Localizacao).ToListAsync();
                ServiceResponse.Data = coletas.Select(c => MapToResponseDto(c)).ToList();

                if (ServiceResponse.Data.Any())
                {
                    ServiceResponse.Message = "Coletas encontradas";
                }
                else
                {
                    ServiceResponse.Message = "Nenhum dado encontrado";
                }
            }
            catch (Exception ex)
            {
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;
            }
            return ServiceResponse;
        }
        // Pegar Coleta por ID
        public async Task<ServiceResponse<ColetaResponseDto>> GetSingleColeta(int id)
        {
            ServiceResponse<ColetaResponseDto> ServiceResponse = new ServiceResponse<ColetaResponseDto>();
            try
            {
                ColetaModel coleta = _context.Coletas.Include(c => c.Localizacao).FirstOrDefault(c => c.ID == id);

                if (coleta == null)
                {
                    ServiceResponse.Message = "Coleta não encontrada";
                    ServiceResponse.Success = false;
                }
                else
                {
                    ServiceResponse.Data = MapToResponseDto(coleta);
                    ServiceResponse.Message = "Coleta encontrada";
                }
            }
            catch (Exception ex)
            {
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;
            }
            return ServiceResponse;
        }
        // Atualizar Coleta
        public async Task<ServiceResponse<ColetaResponseDto>> UpdateColeta(ColetaModel coleta)
        {
            ServiceResponse<ColetaResponseDto> ServiceResponse = new ServiceResponse<ColetaResponseDto>();
            try
            {
                if (coleta.LocalizacaoId <= 0)
                {
                    ServiceResponse.Data = null;
                    ServiceResponse.Message = "LocalizacaoId é obrigatório e deve ser válido";
                    ServiceResponse.Success = false;
                    return ServiceResponse;
                }

                // Validar se a localização existe
                var localizacaoExiste = await _context.Localizacoes.AnyAsync(l => l.ID == coleta.LocalizacaoId);
                if (!localizacaoExiste)
                {
                    ServiceResponse.Data = null;
                    ServiceResponse.Message = $"Localização com ID {coleta.LocalizacaoId} não existe";
                    ServiceResponse.Success = false;
                    return ServiceResponse;
                }

                _context.Update(coleta);
                await _context.SaveChangesAsync();

                // Recarregar a coleta com a localização incluída
                var coletaAtualizada = await _context.Coletas
                    .Include(c => c.Localizacao)
                    .FirstOrDefaultAsync(c => c.ID == coleta.ID);

                ServiceResponse.Data = MapToResponseDto(coletaAtualizada);
                ServiceResponse.Message = "Coleta atualizada com sucesso";
            }
            catch (Exception ex)
            {
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;
            }
            return ServiceResponse;
        }
        // Deletar Coleta
        public async Task<ServiceResponse<List<ColetaResponseDto>>> DeleteColeta(int id)
        {
            ServiceResponse<List<ColetaResponseDto>> ServiceResponse = new ServiceResponse<List<ColetaResponseDto>>();
            try
            {
                ColetaModel coleta = _context.Coletas.FirstOrDefault(c => c.ID == id);
                if (coleta == null)
                {
                    ServiceResponse.Message = "Coleta não encontrada";
                    ServiceResponse.Success = false;
                    return ServiceResponse;
                }
                _context.Coletas.Remove(coleta);
                await _context.SaveChangesAsync();

                var coletas = await _context.Coletas.Include(c => c.Localizacao).ToListAsync();
                ServiceResponse.Data = coletas.Select(c => MapToResponseDto(c)).ToList();
                ServiceResponse.Message = "Coleta deletada com sucesso";
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
