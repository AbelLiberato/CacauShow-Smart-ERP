using CacauShowApi325219722.Models;

namespace CacauShowApi325219722
{
  public class Pedido
    {
        public int Id{get; set;}
        public int UnidadeId{get; set;}
        public int ProdutoId{get; set;}
        public int Quantidade {get; set;}
        public decimal ValorTotal{get; set;}
        
        public Franquia? Unidade {get; set;}
        public Produto? Produto {get;set;}
    }

}