using System.ComponentModel.DataAnnotations;

namespace ColetaAPI.Models
{
    public class LocalizacaoModel
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Descrição eh obrigatória")]
        [StringLength(255, ErrorMessage = "Descrição não pode ter mais de 255 caracteres")]
        public string Descricao { get; set; }

        public DateTime DateOfCreation { get; set; } = DateTime.Now.ToLocalTime();

        public ICollection<ColetaModel> Coletas { get; set; } = new List<ColetaModel>();
    }
}

