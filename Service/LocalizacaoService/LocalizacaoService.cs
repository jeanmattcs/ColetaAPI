using ColetaAPI.DataContext;
using ColetaAPI.DTOs;
using ColetaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ColetaAPI.Service.LocalizacaoService
{
    public class LocalizacaoService : ILocalizacaoInterface
    {
        // Dependency Injection
        private readonly ApplicationsDbContext _context;

        // Constructor
        public LocalizacaoService(ApplicationsDbContext context)
        {
            _context = context;
        }

        // Método auxiliar para mapear LocalizacaoModel para LocalizacaoResponseDto
        private LocalizacaoResponseDto MapToResponseDto(LocalizacaoModel localizacao)
        {
            return new LocalizacaoResponseDto
            {
                Id = localizacao.ID,
                Descricao = localizacao.Descricao,
                DateOfCreation = localizacao.DateOfCreation,
                Coletas = localizacao.Coletas?.Select(c => new ColetaSimpleDto
                {
                    Id = c.ID,
                    OrderDate = c.OrderDate,
                    Collected = c.Collected,
                    DateOfCreation = c.DateOfCreation
                }).ToList() ?? new List<ColetaSimpleDto>()
            };
        }

        // Adicionar Localização
        public async Task<ServiceResponse<List<LocalizacaoResponseDto>>> AddLocalizacao(LocalizacaoModel localizacao)
        {
            ServiceResponse<List<LocalizacaoResponseDto>> serviceResponse = new ServiceResponse<List<LocalizacaoResponseDto>>();
            try
            {
                if (string.IsNullOrWhiteSpace(localizacao.Descricao))
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Descrição é obrigatória";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }

                _context.Add(localizacao);
                await _context.SaveChangesAsync();

                var localizacoes = await _context.Localizacoes.Include(l => l.Coletas).ToListAsync();
                serviceResponse.Data = localizacoes.Select(l => MapToResponseDto(l)).ToList();
                serviceResponse.Message = "Localização adicionada com sucesso";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        // Pegar todas as Localizações
        public async Task<ServiceResponse<List<LocalizacaoResponseDto>>> GetLocalizacoes()
        {
            ServiceResponse<List<LocalizacaoResponseDto>> serviceResponse = new ServiceResponse<List<LocalizacaoResponseDto>>();
            try
            {
                var localizacoes = await _context.Localizacoes
                    .Include(l => l.Coletas)
                    .ToListAsync();

                serviceResponse.Data = localizacoes.Select(l => MapToResponseDto(l)).ToList();

                if (serviceResponse.Data.Any())
                {
                    serviceResponse.Message = "Localizações encontradas";
                }
                else
                {
                    serviceResponse.Message = "Nenhuma localização encontrada";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        // Pegar Localização por ID
        public async Task<ServiceResponse<LocalizacaoResponseDto>> GetLocalizacaoById(int id)
        {
            ServiceResponse<LocalizacaoResponseDto> serviceResponse = new ServiceResponse<LocalizacaoResponseDto>();
            try
            {
                LocalizacaoModel localizacao = await _context.Localizacoes
                    .Include(l => l.Coletas)
                    .FirstOrDefaultAsync(l => l.ID == id);

                if (localizacao == null)
                {
                    serviceResponse.Message = "Localização não encontrada";
                    serviceResponse.Success = false;
                }
                else
                {
                    serviceResponse.Data = MapToResponseDto(localizacao);
                    serviceResponse.Message = "Localização encontrada";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        // Atualizar Localização
        public async Task<ServiceResponse<LocalizacaoResponseDto>> UpdateLocalizacao(LocalizacaoModel localizacao)
        {
            ServiceResponse<LocalizacaoResponseDto> serviceResponse = new ServiceResponse<LocalizacaoResponseDto>();
            try
            {
                if (string.IsNullOrWhiteSpace(localizacao.Descricao))
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Descrição é obrigatória";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }

                _context.Update(localizacao);
                await _context.SaveChangesAsync();

                // Recarregar a localização com as coletas incluídas
                var localizacaoAtualizada = await _context.Localizacoes
                    .Include(l => l.Coletas)
                    .FirstOrDefaultAsync(l => l.ID == localizacao.ID);

                serviceResponse.Data = MapToResponseDto(localizacaoAtualizada);
                serviceResponse.Message = "Localização atualizada com sucesso";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        // Deletar Localização
        public async Task<ServiceResponse<List<LocalizacaoResponseDto>>> DeleteLocalizacao(int id)
        {
            ServiceResponse<List<LocalizacaoResponseDto>> serviceResponse = new ServiceResponse<List<LocalizacaoResponseDto>>();
            try
            {
                LocalizacaoModel localizacao = _context.Localizacoes.FirstOrDefault(l => l.ID == id);
                if (localizacao == null)
                {
                    serviceResponse.Message = "Localização não encontrada";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }

                _context.Localizacoes.Remove(localizacao);
                await _context.SaveChangesAsync();

                var localizacoes = await _context.Localizacoes.Include(l => l.Coletas).ToListAsync();
                serviceResponse.Data = localizacoes.Select(l => MapToResponseDto(l)).ToList();
                serviceResponse.Message = "Localização deletada com sucesso";
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

