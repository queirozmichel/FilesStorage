using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilesStorage.WebAPI.Context;
using File = FilesStorage.WebAPI.Models.File;

namespace FilesStorage.WebAPI.Controllers;

[Route("api/[controller]")] //Nome do controlador "Files"
[ApiController]
public class FilesController : ControllerBase
{
  private readonly WebAPIContext _context;

  public FilesController(WebAPIContext context)
  {
    _context = context;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<File>>> GetAsync()
  {
    try
    {
      var files = await _context.Files.AsNoTracking().ToListAsync();
      if (files == null)
      {
        return NotFound();
      }
      return files;
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }
  }

  [HttpGet("{id}", Name = "GetFile")]
  public async Task<ActionResult<File>> GetAsync(int id)
  {
    try
    {
      var file = await _context.Files.AsNoTracking().FirstOrDefaultAsync(f => f.FileId == id);
      if (file == null)
      {
        return NotFound($"Arquivo com id {id} não encontrado.");
      }
      return file;
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }
  }

  [HttpPost]
  public ActionResult Post(IFormFile iFormFile, int clientId)
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

      _context.Add(file);
      _context.SaveChanges();

      return new CreatedAtRouteResult("GetFile", new { id = file.FileId }, file);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }
  }

  [HttpPut("{id}")]
  public ActionResult Put(int id, int clientId, IFormFile iFormFile)
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

      _context.Entry(file).State = EntityState.Modified;
      _context.SaveChanges();

      return Ok(file);
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
      var file = _context.Files.FirstOrDefault(f => f.FileId == id);

      if (file == null)
      {
        return NotFound($"Arquivo com id {id} não encontrado.");
      }
      _context.Files.Remove(file);
      _context.SaveChanges();

      return Ok("O arquivo foi apagado com sucesso!");
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tentar executar a sua solicitação.");
    }
  }
}