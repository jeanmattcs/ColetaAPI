using System.ComponentModel.DataAnnotations;

namespace ColetaAPI.DTOs
{
    public class CreateLocalizacaoDto
    {
        [Required(ErrorMessage = "Descrição é obrigatória")]
        [StringLength(255, ErrorMessage = "Descrição não pode ter mais de 255 caracteres")]
        public string Descricao { get; set; }
    }

    public class UpdateLocalizacaoDto
    {
        [Required(ErrorMessage = "ID é obrigatório")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória")]
        [StringLength(255, ErrorMessage = "Descrição não pode ter mais de 255 caracteres")]
        public string Descricao { get; set; }
    }
    public class LocalizacaoResponseDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DateOfCreation { get; set; }
        public List<ColetaSimpleDto> Coletas { get; set; } = new List<ColetaSimpleDto>();
    }

    public class ColetaSimpleDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Collected { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}


