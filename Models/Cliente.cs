using System.ComponentModel.DataAnnotations;

namespace LojaApi.Models
{
    public class Cliente
    {
        [Key]
        public int Codigo { get; set; }
        public string ? Nome { get; set; }
        public string ? Email { get; set; }
        public string ? Telefone { get; set; }
    }
}