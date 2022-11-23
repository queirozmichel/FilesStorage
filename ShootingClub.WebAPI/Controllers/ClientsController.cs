using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShootingClub.WebAPI.Context;
using ShootingClub.WebAPI.Models;

namespace ShootingClub.WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
  private readonly WebAPIContext _context;

  public ClientsController(WebAPIContext context)
  {
    _context = context;
  }

  [HttpGet("addresses")]
  public ActionResult<IEnumerable<Client>> GetClientsAddresses()
  {
    try
    {
      return _context.Clients.Include(a => a.Addresses).AsNoTracking().ToList();
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }    
  }

  [HttpGet("files")]
  public ActionResult<IEnumerable<Client>> GetClientsFiles()
  {
    try
    {
      return _context.Clients.Include(f => f.Files).AsNoTracking().ToList();
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }
  }

  [HttpGet("addresses/files")]
  public ActionResult<IEnumerable<Client>> GetClientsAddressesFiles()
  {
    try
    {
      return _context.Clients.Include(a => a.Addresses).Include(f => f.Files).AsNoTracking().ToList();
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }
  }

  [HttpGet]
  public ActionResult<IEnumerable<Client>> Get()
  {
    try
    {
      var clients = _context.Clients.AsNoTracking().ToList();
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

  [HttpGet("{id:int}", Name = "GetClient")]
  public ActionResult<Client> Get(int id)
  {
    try
    {
      var client = _context.Clients.AsNoTracking().FirstOrDefault(c => c.ClientId == id);
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

  [HttpPut("{id:int}")]
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

  [HttpDelete("{id:int}")]
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