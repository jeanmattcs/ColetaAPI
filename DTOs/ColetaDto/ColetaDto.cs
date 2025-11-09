using System.ComponentModel.DataAnnotations;

namespace ColetaAPI.DTOs
{
    public class CreateColetaDto
    {
        [Required(ErrorMessage = "LocalizacaoId é obrigatório")]
        public int LocalizacaoId { get; set; }

        [Required(ErrorMessage = "OrderDate é obrigatório")]
        public DateTime OrderDate { get; set; }

        public bool Collected { get; set; } = false;
    }

    public class UpdateColetaDto
    {
        [Required(ErrorMessage = "ID é obrigatório")]
        public int Id { get; set; }

        [Required(ErrorMessage = "LocalizacaoId é obrigatório")]
        public int LocalizacaoId { get; set; }

        [Required(ErrorMessage = "OrderDate é obrigatório")]
        public DateTime OrderDate { get; set; }

        public bool Collected { get; set; }
    }

    public class ColetaResponseDto
    {
        public int Id { get; set; }
        public int LocalizacaoId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Collected { get; set; }
        public DateTime DateOfCreation { get; set; }
        public LocalizacaoSimpleDto? Localizacao { get; set; }
    }

    public class LocalizacaoSimpleDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime DateOfCreation { get; set; }
    }
}


