using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CacauShowApi325219722.Data;
using CacauShowApi325219722.Dtos;
using CacauShowApi325219722.Models;

namespace CacauShowApi325219722.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PedidosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> Get()
        {
            return await _context.Pedidos
                .Include(p => p.Produto)
                .Include(p => p.Unidade)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(CriarPedidoDto dto)
        {
            var franquia = await _context.Franquias.FindAsync(dto.UnidadeId);
            if (franquia == null)
                return BadRequest("Unidade/Franquia não encontrada.");

            var produto = await _context.Produtos.FindAsync(dto.ProdutoId);
            if (produto == null)
                return BadRequest("Produto não encontrado.");

            var totalItensJaPedidos = await _context.Pedidos
                .Where(p => p.UnidadeId == dto.UnidadeId)
                .SumAsync(p => (int?)p.Quantidade) ?? 0;

            if (totalItensJaPedidos + dto.Quantidade > franquia.CapacidadeEstoque)
            {
                return BadRequest("Capacidade logística da loja excedida. Não é possível receber mais produtos.");
            }

            decimal valorTotal = dto.Quantidade * produto.PrecoBase;

            if (produto.Tipo == "Sazonal")
            {
                valorTotal += 15m;
                Console.WriteLine("Produto sazonal detectado: Adicionando embalagem de presente premium!");
            }

            var pedido = new Pedido
            {
                UnidadeId = dto.UnidadeId,
                ProdutoId = dto.ProdutoId,
                Quantidade = dto.Quantidade,
                ValorTotal = valorTotal
            };

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return Ok(pedido);
        }
    }
}