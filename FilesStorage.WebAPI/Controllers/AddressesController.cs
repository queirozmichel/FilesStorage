using Microsoft.AspNetCore.Mvc;
using FilesStorage.WebAPI.Models;
using FilesStorage.WebAPI.Repository;

namespace FilesStorage.WebAPI.Controllers;

[Route("api/[controller]")] //Nome do controlador "Addresses"
[ApiController]
public class AddressesController : ControllerBase
{
  private readonly IUnitOfWork _uof;

  public AddressesController(IUnitOfWork uof)
  {
    _uof = uof;
  }

  [HttpGet]
  public ActionResult<IEnumerable<Address>> Get()
  {
    try
    {
      var addresses = _uof.AddressRepository.Get().ToList();
      if (addresses is null)
      {
        return NotFound("Endereços não encontrados.");
      }

      return addresses;
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }
  }

  [HttpGet("{id}", Name = "GetAddress")]
  public ActionResult<Address> Get(int id)
  {
    try
    {
      var address = _uof.AddressRepository.GetById(a => a.AddressId == id);
      if (address is null)
      {
        return NotFound($"Endereço com id {id} não encontrado.");
      }

      return address;
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }
  }

  [HttpGet("GetAddressesByClient")]
  public ActionResult<IEnumerable<Address>> GetAddressesByClientId(int id)
  {
    try
    {
      var addresses = _uof.AddressRepository.GetAddressesByClientId(id).ToList();
      if (addresses.Count == 0)
      {
        return NotFound($"Não existem endereços com o clientId {id}.");
      }
      return addresses;
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }

  }

  [HttpPost]
  public ActionResult Post(Address address)
  {
    try
    {
      if (address is null)
      {
        return BadRequest("Dados inválidos.");
      }
      _uof.AddressRepository.Add(address);
      _uof.Commit();

      return new CreatedAtRouteResult("GetAddress", new { id = address.AddressId }, address);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }

  }

  [HttpPut("{id}")]
  public ActionResult Put(int id, Address address)
  {
    try
    {
      if (id != address.AddressId)
      {
        return NotFound($"Os id's {id} e {address.AddressId} são diferentes.");
      }
      _uof.AddressRepository.Update(address);
      _uof.Commit();

      return Ok(address);
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
      var address = _uof.AddressRepository.GetById(a => a.AddressId == id);
      if (address == null)
      {
        return NotFound($"Endereço com id {id} não encontrado.");
      }
      _uof.AddressRepository.Delete(address);
      _uof.Commit();

      return Ok(address);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }

  }
}