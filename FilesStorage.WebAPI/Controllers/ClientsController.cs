using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilesStorage.WebAPI.Context;
using FilesStorage.WebAPI.Models;

namespace FilesStorage.WebAPI.Controllers;

[Route("api/[controller]")] //Nome do controlador "Clients"
[ApiController]
public class ClientsController : ControllerBase
{
  private readonly WebAPIContext _context;

  public ClientsController(WebAPIContext context)
  {
    _context = context;
  }

  [HttpGet("addresses")]
  public async Task<ActionResult<IEnumerable<Client>>> GetClientsAddressesAsync()
  {
    try
    {
      return await _context.Clients.Include(a => a.Addresses).AsNoTracking().ToListAsync();
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }
  }

  [HttpGet("files")]
  public async Task<ActionResult<IEnumerable<Client>>> GetClientsFilesAsync()
  {
    try
    {
      return await _context.Clients.Include(f => f.Files).AsNoTracking().ToListAsync();
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }
  }

  [HttpGet("addresses/files")]
  public async Task<ActionResult<IEnumerable<Client>>> GetClientsAddressesFilesAsync()
  {
    try
    {
      return await _context.Clients.Include(a => a.Addresses).Include(f => f.Files).AsNoTracking().ToListAsync();
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Client>>> GetAsync()
  {
    try
    {
      var clients = await _context.Clients.AsNoTracking().ToListAsync();
      if (clients is null)
      {
        return NotFound("Clientes não encontrados.");
      }
      return clients;
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }

  }

  [HttpGet("{id}", Name = "GetClient")]
  public async Task<ActionResult<Client>> GetAsync(int id)
  {
    try
    {
      var client = await _context.Clients.Include(a => a.Addresses).Include(f => f.Files).AsNoTracking().FirstOrDefaultAsync(c => c.ClientId == id);
      if (client == null)
      {
        return NotFound($"Cliente com id {id} não encontrado.");
      }
      return client;
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }

  }

  [HttpPost]
  public ActionResult Post(Client client)
  {
    try
    {
      if (client is null)
      {
        return BadRequest("Dados inválidos.");
      }
      _context.Clients.Add(client);
      _context.SaveChanges();

      return new CreatedAtRouteResult("GetClient", new { id = client.ClientId }, client);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }

  }

  [HttpPut("{id}")]
  public ActionResult Put(int id, Client client)
  {
    try
    {
      if (id != client.ClientId)
      {
        return BadRequest($"Os id's, {id} e {client.ClientId} são diferentes.");
      }
      _context.Entry(client).State = EntityState.Modified;
      _context.SaveChanges();

      return Ok(client);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }

  }

  [HttpDelete("{id}")]
  public ActionResult Delete(int id)
  {
    try
    {
      var client = _context.Clients.FirstOrDefault(c => c.ClientId == id);
      if (client == null)
      {
        return NotFound($"Cliente com id {id} não encontrado.");
      }
      _context.Clients.Remove(client);
      _context.SaveChanges();

      return Ok(client);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }

  }
}