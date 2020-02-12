using System.ComponentModel.DataAnnotations;

namespace GerenciamentoEvento.DTO
{
    public class LocalDTO
    {
       [Required]
        public int Id { get; set; }
         
        [Required(ErrorMessage="O Nome é obrigatório.")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage="O Endereço é obrigatório.")]
        public string Endereco { get; set; }
    }
}