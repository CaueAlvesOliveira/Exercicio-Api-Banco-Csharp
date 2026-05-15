using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LojaApi.Data;
using LojaApi.Models;

namespace LojaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FornecedorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fornecedor>>> Get()
        {
            return await _context.Fornecedor.ToListAsync();
        }

        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<IEnumerable<Fornecedor>>> GetPorNome(string nome)
        {
            var fornecedores = await _context.Fornecedor
                .Where(f => f.Nome.Contains(nome))
                .ToListAsync();

            return Ok(fornecedores);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Fornecedor fornecedor)
        {
            _context.Fornecedor.Add(fornecedor);
            await _context.SaveChangesAsync();
            return Ok(fornecedor);
        }

        [HttpPut("{codigo}")]
        public async Task<ActionResult> Put(int codigo, Fornecedor fornecedor)
        {
            var fornecedorBanco = await _context.Fornecedor.FindAsync(codigo);
            if (fornecedorBanco == null)
                return NotFound();

            fornecedorBanco.Nome = fornecedor.Nome;
            fornecedorBanco.Email = fornecedor.Email;
            fornecedorBanco.Cnpj = fornecedor.Cnpj;
            fornecedorBanco.Telefone = fornecedor.Telefone;
            await _context.SaveChangesAsync();
            return Ok(fornecedorBanco);
        }

        [HttpDelete("{codigo}")]
        public async Task<ActionResult> Delete(int codigo)
        {
            var fornecedor = await _context.Fornecedor.FindAsync(codigo);
            if (fornecedor == null)
                return NotFound();

            _context.Fornecedor.Remove(fornecedor);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}