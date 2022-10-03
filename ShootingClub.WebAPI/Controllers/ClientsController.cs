using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShootingClub.WebAPI.Context;
using ShootingClub.WebAPI.Models;

namespace ShootingClub.WebAPI.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class ClientsController : ControllerBase
  {
    private readonly WebAPIContext _context;

    public ClientsController(WebAPIContext context)
    {
      _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Client>> Get()
    {
      var clients = _context.Clients.ToList();
      if (clients is null)
      {
        return NotFound("Clientes não encontrados.");
      }
      return clients;
    }

    [HttpGet("{id:int}", Name="ObterCliente")]
    public ActionResult<Client> Get(int id)
    {
      var client = _context.Clients.FirstOrDefault(c => c.ClientId == id);
      if (client == null)
      {
        return NotFound("Cliente não encontrado.");
      }
      return client;
    }

    [HttpPost]
    public ActionResult Post(Client client)
    {
      if (client is null)
      {
        return BadRequest();
      }
      _context.Clients.Add(client);
      _context.SaveChanges();

      return new CreatedAtRouteResult("ObterCliente", new { id = client.ClientId }, client);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Client client)
    {
      if (id != client.ClientId)
      {
        return BadRequest("Os id's são diferentes.");
      }
      _context.Entry(client).State = EntityState.Modified;
      _context.SaveChanges();

      return Ok(client);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
      var client = _context.Clients.FirstOrDefault(c => c.ClientId == id);
      if(client == null)
      {
        return NotFound("Cliente não encontrado.");
      }
      _context.Clients.Remove(client);
      _context.SaveChanges();
       
      return Ok(client);
    }
  }
}
