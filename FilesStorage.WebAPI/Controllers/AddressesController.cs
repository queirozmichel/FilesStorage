using Microsoft.AspNetCore.Mvc;
using FilesStorage.WebAPI.Models;
using FilesStorage.WebAPI.Repository;
using AutoMapper;
using FilesStorage.WebAPI.DTOs;

namespace FilesStorage.WebAPI.Controllers;

[Route("api/[controller]")] //Nome do controlador "Addresses"
[ApiController]
public class AddressesController : ControllerBase
{
  private readonly IUnitOfWork _uof;
  private readonly IMapper _mapper;

  public AddressesController(IUnitOfWork uof, IMapper mapper)
  {
    _uof = uof;
    _mapper = mapper;
  }

  [HttpGet]
  public ActionResult<IEnumerable<AddressDTO>> Get()
  {
    try
    {      
      var addresses = _uof.AddressRepository.Get().ToList();
      var addressesDto = _mapper.Map<List<AddressDTO>>(addresses); 

      if (addresses is null)
      {
        return NotFound("Endereços não encontrados.");
      }
      return addressesDto;
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }
  }

  [HttpGet("{id}", Name = "GetAddress")]
  public ActionResult<AddressDTO> Get(int id)
  {
    try
    {
      var address = _uof.AddressRepository.GetById(a => a.AddressId == id);
      var addressDto = _mapper.Map<AddressDTO>(address);
      if (address is null)
      {
        return NotFound($"Endereço com id {id} não encontrado.");
      }

      return addressDto;
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }
  }

  [HttpGet("GetAddressesByClient")]
  public ActionResult<IEnumerable<AddressDTO>> GetAddressesByClientId(int id)
  {
    try
    {
      var addresses = _uof.AddressRepository.GetAddressesByClientId(id).ToList();
      var addressesDto = _mapper.Map<List<AddressDTO>>(addresses);
      if (addresses.Count == 0)
      {
        return NotFound($"Não existem endereços com o clientId {id}.");
      }
      return addressesDto;
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }

  }

  [HttpPost]
  public ActionResult Post(AddressDTO addressDto)
  {
    try
    {
      if (addressDto is null)
      {
        return BadRequest("Dados inválidos.");
      }
      var address = _mapper.Map<Address>(addressDto);
      _uof.AddressRepository.Add(address);
      _uof.Commit();

      var addressesDTO = _mapper.Map<AddressDTO>(address);

      return new CreatedAtRouteResult("GetAddress", new { id = address.AddressId }, addressesDTO);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }

  }

  [HttpPut("{id}")]
  public ActionResult Put(int id, AddressDTO addressDto)
  {
    try
    {
      if (id != addressDto.AddressId)
      {
        return NotFound($"Os id's {id} e {addressDto.AddressId} são diferentes.");
      }
      var address = _mapper.Map<Address>(addressDto);
      _uof.AddressRepository.Update(address);
      _uof.Commit();

      return Ok();
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }

  }

  [HttpDelete("{id}")]
  public ActionResult<AddressDTO> Delete(int id)
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

      var addressDto = _mapper.Map<AddressDTO>(address);

      return Ok(addressDto);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }

  }
}