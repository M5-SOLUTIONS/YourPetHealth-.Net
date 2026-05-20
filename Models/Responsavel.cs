using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourPetHealth.Models
{
    [Table("T_RESPONSAVEIS")]
    public class Responsavel
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [Column("NOME")]
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        [Column("EMAIL")]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [Column("SENHA")]
        [MaxLength(255)]
        public string Senha { get; set; } = string.Empty;

        [Column("TELEFONE")]
        [MaxLength(20)]
        public string? Telefone { get; set; }

        public ICollection<Pet> Pets { get; set; } = new List<Pet>();
    }
}