using CacauShowApi325219722.Models;

namespace CacauShowApi325219722
{
    public class LoteProducao
    {
        public int ID{get; set;}
        public string CodigoLote{get; set;} = string.Empty;
        public DateTime DataFabricacao {get; set;}
        public int ProdutoId {get; set;}
        public string Status{get; set;} = string.Empty;

        public Produto? Produto {get; set;}
    }
}