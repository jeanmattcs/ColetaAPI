using ColetaAPI.Models;

namespace ColetaAPI.Service.LocalizacaoService
{
    public interface ILocalizacaoInterface
    {
        // Pegar todas as Localizações
        Task<ServiceResponse<List<LocalizacaoModel>>> GetLocalizacoes();

        // Pegar Localização por ID
        Task<ServiceResponse<LocalizacaoModel>> GetLocalizacaoById(int id);

        // Adicionar Localização
        Task<ServiceResponse<List<LocalizacaoModel>>> AddLocalizacao(LocalizacaoModel localizacao);

        // Atualizar Localização
        Task<ServiceResponse<LocalizacaoModel>> UpdateLocalizacao(LocalizacaoModel localizacao);

        // Deletar Localização
        Task<ServiceResponse<List<LocalizacaoModel>>> DeleteLocalizacao(int id);
    }
}

