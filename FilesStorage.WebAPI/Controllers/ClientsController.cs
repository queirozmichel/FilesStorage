using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilesStorage.WebAPI.Models;
using FilesStorage.WebAPI.Repository;

namespace FilesStorage.WebAPI.Controllers;

[Route("api/[controller]")] //Nome do controlador "Clients"
[ApiController]
public class ClientsController : ControllerBase
{
  private readonly IUnitOfWork _uof;

  public ClientsController(IUnitOfWork uof)
  {
    _uof = uof;
  }

  [HttpGet("addresses")]
  public ActionResult<IEnumerable<Client>> GetClientsAddresses()
  {
    try
    {
      return _uof.ClientRepository.Get().Include(a => a.Addresses).AsNoTracking().ToList();
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
      return _uof.ClientRepository.Get().Include(f => f.Files).AsNoTracking().ToList();
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
      return _uof.ClientRepository.Get().Include(a => a.Addresses).Include(f => f.Files).AsNoTracking().ToList();
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }
  }

  [HttpGet("GetMaleClients")]
  public ActionResult<IEnumerable<Client>> GetMaleClients()
  {
    try
    {
      var clients = _uof.ClientRepository.GetMaleClients().ToList();
      if (clients.Count == 0)
      {
        return NotFound("Não existem clientes masculinos");
      }
      return clients;
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
      var clients = _uof.ClientRepository.Get().AsNoTracking().ToList();
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
  public ActionResult<Client> Get(int id)
  {
    try
    {
      var client = _uof.ClientRepository.Get().Include(a => a.Addresses).Include(f => f.Files).AsNoTracking().FirstOrDefault(c => c.ClientId == id);
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
      _uof.ClientRepository.Add(client);
      _uof.Commit();

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
      _uof.ClientRepository.Update(client);
      _uof.Commit();

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
      var client = _uof.ClientRepository.Get().FirstOrDefault(c => c.ClientId == id);
      if (client == null)
      {
        return NotFound($"Cliente com id {id} não encontrado.");
      }
      _uof.ClientRepository.Delete(client);
      _uof.Commit();

      return Ok(client);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }

  }
}