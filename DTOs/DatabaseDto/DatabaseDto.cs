using ColetaAPI.Models;

namespace ColetaAPI.DTOs
{
    public class DatabaseDto
    {
        public List<LocalizacaoModel> Localizacoes { get; set; } = new List<LocalizacaoModel>();
        public List<ColetaModel> Coletas { get; set; } = new List<ColetaModel>();
    }
}


