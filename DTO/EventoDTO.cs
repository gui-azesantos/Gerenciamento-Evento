using System.ComponentModel.DataAnnotations;

namespace GerenciamentoEvento.DTO
{
    public class EventoDTO
    {
        [Required]
        public int Id { get; set; }
        
        [Required(ErrorMessage="O Nome é obrigatório.")]
        [StringLength(150, ErrorMessage="Nome grande demais.")]
        [MinLength(2, ErrorMessage="Nome curto demais.")]
        public string Nome { get; set; }

        [Range(10,100000,ErrorMessage="Capacidade Inválida.")]
        public int Capacidade { get; set; }

        [Required(ErrorMessage="A data é obrigatória.")]
        public System.DateTime Data { get; set; }

        [Range(1,100000,ErrorMessage="Preço inválido.")]
        public double Preco { get; set; }
        
         public int CasaDeShowID {get; set;}
        
         public string Estilo { get; set; }

        [Required(ErrorMessage="A URL da imagem é obrigatória.")]
         public string Imagem { get; set; }
    }
}