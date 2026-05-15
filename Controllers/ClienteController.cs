using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LojaApi.Data;
using LojaApi.Models;
namespace LojaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClienteController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Cliente>>> Get()
        {
            return await _context.Clientes.ToListAsync();
        }
        [HttpPost]

        public async Task<ActionResult> Post(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return Ok(cliente);
        }
        [HttpPut("{codigo}")]

        public async Task<ActionResult> Put(int codigo, Cliente cliente)
        {
            var clienteBanco = await _context.Clientes.FindAsync(codigo);
            if (clienteBanco == null)
            {
                return NotFound();
            }
            clienteBanco.Nome = cliente.Nome;
            clienteBanco.Email = cliente.Email;
            clienteBanco.Telefone = cliente.Telefone;
            await _context.SaveChangesAsync();
            return Ok(clienteBanco);
        }
        [HttpDelete("{codigo}")]

        public async Task<ActionResult> Delete(int codigo)
        {
            var cliente = await _context.Clientes.FindAsync(codigo);
            if (cliente == null)
            {
                return NotFound();
            }
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}