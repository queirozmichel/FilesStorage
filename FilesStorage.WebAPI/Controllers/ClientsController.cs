using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilesStorage.WebAPI.Models;
using FilesStorage.WebAPI.Repository;
using AutoMapper;
using FilesStorage.WebAPI.DTOs;
using FilesStorage.WebAPI.Pagination;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace FilesStorage.WebAPI.Controllers;

[Route("api/[controller]")] //Nome do controlador "Clients"
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Produces("application/json")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class ClientsController : ControllerBase
{
  private readonly IUnitOfWork _uof;
  private readonly IMapper _mapper;

  public ClientsController(IUnitOfWork uof, IMapper mapper)
  {
    _uof = uof;
    _mapper = mapper;
  }

  /// <summary>
  /// Obtém todos os clientes por paginação
  /// </summary>
  /// <returns></returns>
  [HttpGet]
  public async Task<ActionResult<IEnumerable<ClientDTO>>> Get([FromQuery] ClientsParameters clientsParameters)
  {
    try
    {
      var clients = await _uof.ClientRepository.GetClients(clientsParameters);
      var metadata = new
      {
        clients.TotalCount,
        clients.PageSize,
        clients.CurrentPage,
        clients.TotalPages,
        clients.HasNext,
        clients.HasPrevious,
      };

      Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

      var clientsDTO = _mapper.Map<List<ClientDTO>>(clients);
      if (clients is null)
      {
        return NotFound("Clientes não encontrados.");
      }
      return clientsDTO;
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }

  }

  /// <summary>
  /// Obtém todos os clientes com os respectivos endereços
  /// </summary>
  /// <returns></returns>
  [HttpGet("addresses")]
  public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClientsAddresses()
  {
    try
    {
      var clients = await _uof.ClientRepository.Get().Include(a => a.Addresses).AsNoTracking().ToListAsync();
      var clientsDTO = _mapper.Map<List<ClientDTO>>(clients);
      return clientsDTO;
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }
  }

  /// <summary>
  /// Obtém todos os clientes com os respectivos arquivos
  /// </summary>
  /// <returns></returns>
  [HttpGet("files")]
  public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClientsFiles()
  {
    try
    {
      var clients = await _uof.ClientRepository.Get().Include(f => f.Files).AsNoTracking().ToListAsync();
      var clientsDTO = _mapper.Map<List<ClientDTO>>(clients);
      return clientsDTO;
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }
  }

  /// <summary>
  /// Obtém todos os clientes com os respectivos endereços e arquivos
  /// </summary>
  /// <returns></returns>
  [HttpGet("addresses/files")]
  public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClientsAddressesFiles()
  {
    try
    {
      var clients = await _uof.ClientRepository.Get().Include(a => a.Addresses).Include(f => f.Files).AsNoTracking().ToListAsync();
      var clientsDTO = _mapper.Map<List<ClientDTO>>(clients);
      return clientsDTO;
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }
  }

  /// <summary>
  /// Obtém todos os clientes do sexo masculino
  /// </summary>
  /// <returns></returns>
  [HttpGet("GetMaleClients")]
  public async Task<ActionResult<IEnumerable<ClientDTO>>> GetMaleClients()
  {
    try
    {
      var clients = await _uof.ClientRepository.GetMaleClients();
      var clientsDTO = _mapper.Map<List<ClientDTO>>(clients);
      if (clients.Count() == 0)
      {
        return NotFound("Não existem clientes masculinos");
      }
      return clientsDTO;
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }
  }

  /// <summary>
  /// Obtém um cliente pelo seu respectivo Id
  /// </summary>
  /// <param name="id">Id do cliente</param>
  /// <returns></returns>
  [HttpGet("{id}", Name = "GetClient")]
  public async Task<ActionResult<ClientDTO>> Get(int id)
  {
    try
    {
      var client = await _uof.ClientRepository.Get().Include(a => a.Addresses).Include(f => f.Files).AsNoTracking().FirstOrDefaultAsync(c => c.ClientId == id);
      var clientDTO = _mapper.Map<ClientDTO>(client);
      if (client == null)
      {
        return NotFound($"Cliente com id {id} não encontrado.");
      }
      return clientDTO;
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }

  }

  /// <summary>
  /// Inclui um novo cliente
  /// </summary>
  /// <param name="clientDto">Um objeto ClienteDTO</param>
  /// <returns></returns>
  [HttpPost]
  public async Task<ActionResult> Post(ClientDTO clientDto)
  {
    try
    {
      if (clientDto is null)
      {
        return BadRequest("Dados inválidos.");
      }
      var client = _mapper.Map<Client>(clientDto);
      _uof.ClientRepository.Add(client);
      await _uof.Commit();

      var clientDTO = _mapper.Map<ClientDTO>(client);

      return new CreatedAtRouteResult("GetClient", new { id = client.ClientId }, clientDTO);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }

  }

  /// <summary>
  /// Atualiza um cliente pelo seu respectivo Id
  /// </summary>
  /// <param name="id">Id do cliente</param>
  /// <param name="clientDto">Um objeto ClienteDTO</param>
  /// <returns></returns>
  [HttpPut("{id}")]
  public async Task<ActionResult> Put(int id, ClientDTO clientDto)
  {
    try
    {
      if (id != clientDto.ClientId)
      {
        return BadRequest($"Os id's, {id} e {clientDto.ClientId} são diferentes.");
      }
      var client = _mapper.Map<Client>(clientDto);
      _uof.ClientRepository.Update(client);
      await _uof.Commit();

      return Ok();
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }

  }

  /// <summary>
  /// Apaga um cliente pelo seu respectivo Id
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  [HttpDelete("{id}")]
  public async Task<ActionResult<ClientDTO>> Delete(int id)
  {
    try
    {
      var client = _uof.ClientRepository.Get().FirstOrDefault(c => c.ClientId == id);
      if (client == null)
      {
        return NotFound($"Cliente com id {id} não encontrado.");
      }
      _uof.ClientRepository.Delete(client);
      await _uof.Commit();

      var clientDto = _mapper.Map<ClientDTO>(client);

      return Ok(clientDto);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }

  }
}