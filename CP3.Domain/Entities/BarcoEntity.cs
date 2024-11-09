using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CP3.Domain.Entities
{
    [Table("tb_barcos")]
    public class BarcoEntity
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do barco é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do barco não pode exceder 100 caracteres.")]
        [Column("Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O modelo do barco é obrigatório.")]
        [StringLength(100, ErrorMessage = "O modelo do barco não pode exceder 100 caracteres.")]
        [Column("Modelo")]
        public string Modelo { get; set; }

        [Range(1900, int.MaxValue, ErrorMessage = "O ano de fabricação deve ser maior ou igual a 1900.")]
        [Column("Ano")]
        public int Ano { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "O tamanho do barco deve ser maior que zero.")]
        [Column("Tamanho")]
        public double Tamanho { get; set; }
    }
}
