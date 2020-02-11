namespace GerenciamentoEvento.Models
{
    public class Evento
    {
        
        public int Id { get; set; }
        
       
        public string Nome { get; set; }
        
        
        public int Capacidade { get; set; }

       
        public System.DateTime Data { get; set; }

        public double Preco { get; set; }

        
         public Local CasaDeShow {get; set;}

       
         public string Estilo { get; set; }

        
         public string Imagem { get; set; }
    }
}