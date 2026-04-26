namespace CacauShowApi325219722.Dtos
{
    public class CriarLoteDto
    {
        public string CodigoLote {get; set;} = string.Empty;
        public DateTime DataFabricacao {get; set;}
        public int ProdutoId {get; set;}
        public string Status {get; set;} = string.Empty;
    }
}