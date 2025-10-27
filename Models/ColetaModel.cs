using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColetaAPI.Models
{
    public class ColetaModel
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "LocalizacaoId é obrigatório")]
        [ForeignKey("Localizacao")]
        public int LocalizacaoId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Collected { get; set; } = false;
        public DateTime DateOfCreation { get; set; } = DateTime.Now.ToLocalTime();
        public LocalizacaoModel Localizacao { get; set; }
    }
}
