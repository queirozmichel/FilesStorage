using Microsoft.AspNetCore.Mvc;
using FilesStorage.WebAPI.Models;
using FilesStorage.WebAPI.Repository;
using AutoMapper;
using FilesStorage.WebAPI.DTOs;
using FilesStorage.WebAPI.Pagination;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace FilesStorage.WebAPI.Controllers;

[Route("api/[controller]")] //Nome do controlador "Addresses"
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
  public async Task<ActionResult<IEnumerable<AddressDTO>>> Get([FromQuery] AddressesParameters addressesParameters)
  {
    try
    {      
      var addresses = await _uof.AddressRepository.GetAddresses(addressesParameters);
      var metadata = new
      {
        addresses.TotalCount,
        addresses.PageSize,
        addresses.CurrentPage,
        addresses.TotalPages,
        addresses.HasNext,
        addresses.HasPrevious,
      };

      Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

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
  public async Task<ActionResult<AddressDTO>> Get(int id)
  {
    try
    {
      var address = await _uof.AddressRepository.GetById(a => a.AddressId == id);
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
  public async Task<ActionResult<IEnumerable<AddressDTO>>> GetAddressesByClientId(int id)
  {
    try
    {
      var addresses = await _uof.AddressRepository.GetAddressesByClientId(id);
      var addressesDto = _mapper.Map<List<AddressDTO>>(addresses);
      if (addresses.Count() == 0)
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
  public async Task<ActionResult> Post(AddressDTO addressDto)
  {
    try
    {
      if (addressDto is null)
      {
        return BadRequest("Dados inválidos.");
      }
      var address = _mapper.Map<Address>(addressDto);
      _uof.AddressRepository.Add(address);
      await _uof.Commit();

      var addressesDTO = _mapper.Map<AddressDTO>(address);

      return new CreatedAtRouteResult("GetAddress", new { id = address.AddressId }, addressesDTO);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }

  }

  [HttpPut("{id}")]
  public async Task<ActionResult> Put(int id, AddressDTO addressDto)
  {
    try
    {
      if (id != addressDto.AddressId)
      {
        return NotFound($"Os id's {id} e {addressDto.AddressId} são diferentes.");
      }
      var address = _mapper.Map<Address>(addressDto);
      _uof.AddressRepository.Update(address);
      await _uof.Commit();

      return Ok();
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }

  }

  [HttpDelete("{id}")]
  public async Task<ActionResult<AddressDTO>> Delete(int id)
  {
    try
    {
      var address = await _uof.AddressRepository.GetById(a => a.AddressId == id);
      if (address == null)
      {
        return NotFound($"Endereço com id {id} não encontrado.");
      }
      _uof.AddressRepository.Delete(address);
      await _uof.Commit();

      var addressDto = _mapper.Map<AddressDTO>(address);

      return Ok(addressDto);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }

  }
}