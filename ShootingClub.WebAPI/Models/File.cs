using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShootingClub.WebAPI.Models;

public class File
{
  public int FileId { get; set; }
  [Required]
  [MaxLength(100)]
  public string? Name { get; set; }
  [Required]
  [MaxLength(30)]
  public string? Extension { get; set; }
  [Required]
  public byte[]? Data { get; set; }
  [Required]
  public int ClientId { get; set; } //Propriedade de navegação
  [JsonIgnore]
  public Client? Client { get; set; } //Propriedade de navegação
  public DateTime? ChangeDate { get; set; }
}