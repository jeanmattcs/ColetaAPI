using ColetaAPI.DTOs;
using ColetaAPI.Models;

namespace ColetaAPI.Service.LocalizacaoService
{
    public interface ILocalizacaoInterface
    {
        // Pegar todas as Localizações
        Task<ServiceResponse<List<LocalizacaoResponseDto>>> GetLocalizacoes();

        // Pegar Localização por ID
        Task<ServiceResponse<LocalizacaoResponseDto>> GetLocalizacaoById(int id);

        // Adicionar Localização
        Task<ServiceResponse<List<LocalizacaoResponseDto>>> AddLocalizacao(LocalizacaoModel localizacao);

        // Atualizar Localização
        Task<ServiceResponse<LocalizacaoResponseDto>> UpdateLocalizacao(LocalizacaoModel localizacao);

        // Deletar Localização
        Task<ServiceResponse<List<LocalizacaoResponseDto>>> DeleteLocalizacao(int id);
    }
}

