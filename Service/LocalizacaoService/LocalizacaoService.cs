using ColetaAPI.DataContext;
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

        // Adicionar Localização
        public async Task<ServiceResponse<List<LocalizacaoModel>>> AddLocalizacao(LocalizacaoModel localizacao)
        {
            ServiceResponse<List<LocalizacaoModel>> serviceResponse = new ServiceResponse<List<LocalizacaoModel>>();
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
                serviceResponse.Data = await _context.Localizacoes.ToListAsync();
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
        public async Task<ServiceResponse<List<LocalizacaoModel>>> GetLocalizacoes()
        {
            ServiceResponse<List<LocalizacaoModel>> serviceResponse = new ServiceResponse<List<LocalizacaoModel>>();
            try
            {
                serviceResponse.Data = await _context.Localizacoes.ToListAsync();
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
        public async Task<ServiceResponse<LocalizacaoModel>> GetLocalizacaoById(int id)
        {
            ServiceResponse<LocalizacaoModel> serviceResponse = new ServiceResponse<LocalizacaoModel>();
            try
            {
                LocalizacaoModel localizacao = _context.Localizacoes.FirstOrDefault(l => l.ID == id);

                if (localizacao == null)
                {
                    serviceResponse.Message = "Localização não encontrada";
                    serviceResponse.Success = false;
                }
                else
                {
                    serviceResponse.Data = localizacao;
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
        public async Task<ServiceResponse<LocalizacaoModel>> UpdateLocalizacao(LocalizacaoModel localizacao)
        {
            ServiceResponse<LocalizacaoModel> serviceResponse = new ServiceResponse<LocalizacaoModel>();
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
                serviceResponse.Data = localizacao;
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
        public async Task<ServiceResponse<List<LocalizacaoModel>>> DeleteLocalizacao(int id)
        {
            ServiceResponse<List<LocalizacaoModel>> serviceResponse = new ServiceResponse<List<LocalizacaoModel>>();
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
                serviceResponse.Data = await _context.Localizacoes.ToListAsync();
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

