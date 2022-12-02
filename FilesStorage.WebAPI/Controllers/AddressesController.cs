using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilesStorage.WebAPI.Context;
using FilesStorage.WebAPI.Models;

namespace FilesStorage.WebAPI.Controllers;

[Route("api/[controller]")] //Nome do controlador "Addresses"
[ApiController]
public class AddressesController : ControllerBase
{
  private readonly WebAPIContext _context;

  public AddressesController(WebAPIContext context)
  {
    _context = context;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Address>>> GetAsync()
  {
    try
    {
      var addresses = await _context.Addresses.AsNoTracking().ToListAsync();
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
  public async Task<ActionResult<Address>> GetAsync(int id)
  {
    try
    {
      var address = await _context.Addresses.AsNoTracking().FirstOrDefaultAsync(a => a.AddressId == id);
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

  [HttpPost]
  public ActionResult Post(Address address)
  {
    try
    {
      if (address is null)
      {
        return BadRequest("Dados inválidos.");
      }
      _context.Add(address);
      _context.SaveChanges();

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
      _context.Entry(address).State = EntityState.Modified;
      _context.SaveChanges();

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
      var address = _context.Addresses.FirstOrDefault(a => a.AddressId == id);
      if (address == null)
      {
        return NotFound($"Endereço com id {id} não encontrado.");
      }
      _context.Addresses.Remove(address);
      _context.SaveChanges();

      return Ok(address);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }

  }
}