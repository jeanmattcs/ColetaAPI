using ColetaAPI.DTOs;
using ColetaAPI.Models;

namespace ColetaAPI.Service.ColetaService
{
    public interface IColetaInterface
    {
        // Pegar todas as Coletas
        Task<ServiceResponse<List<ColetaResponseDto>>> GetColeta();
        Task<ServiceResponse<ColetaResponseDto>> GetSingleColeta(int id);
        Task<ServiceResponse<List<ColetaResponseDto>>> AddColeta(ColetaModel coleta);
        Task<ServiceResponse<ColetaResponseDto>> UpdateColeta(ColetaModel coleta);
        Task<ServiceResponse<List<ColetaResponseDto>>> DeleteColeta(int id);
    }
}
