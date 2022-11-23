using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShootingClub.WebAPI.Context;
using File = ShootingClub.WebAPI.Models.File;

namespace ShootingClub.WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
  private readonly WebAPIContext _context;

  public FilesController(WebAPIContext context)
  {
    _context = context;
  }

  [HttpGet]
  public ActionResult<IEnumerable<File>> Get()
  {
    try
    {
      var files = _context.Files.AsNoTracking().ToList();
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

  [HttpGet("{id:int}", Name = "GetFile")]
  public ActionResult<File> Get(int id)
  {
    try
    {
      var file = _context.Files.AsNoTracking().FirstOrDefault(f => f.FileId == id);
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

  [HttpPut("{id:int}")]
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

  [HttpDelete("{id:int}")]
  public ActionResult Delete(int id)
  {
    try
    {
      var file = _context.Files.FirstOrDefault(f => f.FileId == id);

      if (file == null)
      {
        return NotFound($"Cliente com id {id} não encontrado.");
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