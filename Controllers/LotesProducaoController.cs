using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CacauShowApi325219722.Data;
using CacauShowApi325219722.Dtos;
using CacauShowApi325219722.Models;

namespace CacauShowApi325219722.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesProducaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LotesProducaoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoteProducao>>> Get()
        {
            return await _context.LotesProducao
                .Include(l => l.Produto)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(CriarLoteDto dto)
        {
            var produto = await _context.Produtos.FindAsync(dto.ProdutoId);
            if (produto == null)
                return BadRequest("ProdutoId inválido. Produto não encontrado.");

            if (dto.DataFabricacao.Date > DateTime.Now.Date)
                return Conflict("Lote inválido: Data de fabricação não pode ser maior que a data atual.");

            var lote = new LoteProducao
            {
                CodigoLote = dto.CodigoLote,
                DataFabricacao = dto.DataFabricacao,
                ProdutoId = dto.ProdutoId,
                Status = dto.Status
            };

            _context.LotesProducao.Add(lote);
            await _context.SaveChangesAsync();

            return Ok(lote);
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult> AtualizarStatus(int id, AtualizarStatusLoteDto dto)
        {
            var lote = await _context.LotesProducao.FindAsync(id);
            if (lote == null)
                return NotFound("Lote não encontrado.");

            if (lote.Status == "Descartado" &&
                (dto.Status == "Qualidade Aprovada" || dto.Status == "Distribuído"))
            {
                return BadRequest("Regra de negócio violada: um lote descartado não pode ser alterado para Qualidade Aprovada ou Distribuído.");
            }

            lote.Status = dto.Status;
            await _context.SaveChangesAsync();

            return Ok(lote);
        }
    }
}