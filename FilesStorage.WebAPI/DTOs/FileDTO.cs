using FilesStorage.WebAPI.Models;

namespace FilesStorage.WebAPI.DTOs
{
  public class FileDTO
  {
    public int FileId { get; set; }
    public string? Name { get; set; }
    public string? Extension { get; set; }
    public byte[]? Data { get; set; }
    public int ClientId { get; set; } //Propriedade de navegação
    public Client? Client { get; set; } //Propriedade de navegação
  }
}