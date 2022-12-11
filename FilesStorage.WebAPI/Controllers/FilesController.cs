using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using File = FilesStorage.WebAPI.Models.File;
using FilesStorage.WebAPI.Repository;
using FilesStorage.WebAPI.DTOs;
using AutoMapper;
using FilesStorage.WebAPI.Pagination;
using Newtonsoft.Json;

namespace FilesStorage.WebAPI.Controllers;

[Route("api/[controller]")] //Nome do controlador "Files"
[ApiController]
public class FilesController : ControllerBase
{
  private readonly IUnitOfWork _uof;
  private readonly IMapper _mapper;

  public FilesController(IUnitOfWork uof, IMapper mapper)
  {
    _uof = uof;
    _mapper = mapper;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<FileDTO>>> Get([FromQuery] FilesParameters filesParameters)
  {
    try
    {
      var files = await _uof.FileRepository.GetFiles(filesParameters);
      var metadata = new
      {
        files.TotalCount,
        files.PageSize,
        files.CurrentPage,
        files.TotalPages,
        files.HasNext,
        files.HasPrevious,
      };

      Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

      var filesDto = _mapper.Map<List<FileDTO>>(files);
      if (files == null)
      {
        return NotFound();
      }
      return filesDto;
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }
  }

  [HttpGet("{id}", Name = "GetFile")]
  public async Task<ActionResult<FileDTO>> Get(int id)
  {
    try
    {
      var file = await _uof.FileRepository.Get().AsNoTracking().FirstOrDefaultAsync(f => f.FileId == id);
      var fileDto = _mapper.Map<FileDTO>(file);
      if (file == null)
      {
        return NotFound($"Arquivo com id {id} não encontrado.");
      }
      return fileDto;
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }
  }

  [HttpGet("GetFilesByClientId")]
  public async Task<ActionResult<IEnumerable<FileDTO>>> GetFilesByClientId(int id)
  {
    try
    {
      var files = await _uof.FileRepository.GetFilesByClientId(id);
      var filesDto = _mapper.Map<List<FileDTO>>(files);
      //if (files.Count == 0)
      //{
      //  return NotFound($"Não existem arquivos com o clientId {id}.");
      //}
      return filesDto;
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }
  }

  [HttpPost]
  public async Task<ActionResult> Post(IFormFile iFormFile, int clientId)
  {
    try
    {
      if (iFormFile is null)
      {
        return BadRequest("Dados inválidos.");
      }

      File file = new File();
      file.Name = iFormFile.FileName;
      file.ClientId = clientId;
      file.Extension = iFormFile.ContentType;
      using (var stream = new MemoryStream())
      {
        iFormFile.CopyTo(stream);
        file.Data = stream.ToArray();
      }

      _uof.FileRepository.Add(file);
      await _uof.Commit();

      return new CreatedAtRouteResult("GetFile", new { id = file.FileId }, file);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }
  }

  [HttpPut("{id}")]
  public async Task<ActionResult> Put(int id, int clientId, IFormFile iFormFile)
  {
    try
    {
      File file = new File();
      file.FileId = id;
      file.Name = iFormFile.FileName;
      file.ClientId = clientId;
      file.Extension = iFormFile.ContentType;
      using (var stream = new MemoryStream())
      {
        iFormFile.CopyTo(stream);
        file.Data = stream.ToArray();
      }

      _uof.FileRepository.Update(file);
      await _uof.Commit();

      return Ok(file);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult> Delete(int id)
  {
    try
    {
      var file = await _uof.FileRepository.GetById(f => f.FileId == id);

      if (file == null)
      {
        return NotFound($"Arquivo com id {id} não encontrado.");
      }
      _uof.FileRepository.Delete(file);
      await _uof.Commit();

      return Ok("O arquivo foi apagado com sucesso!");
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }
  }
}