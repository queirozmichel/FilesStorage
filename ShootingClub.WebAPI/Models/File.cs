using System.ComponentModel.DataAnnotations;

namespace ShootingClub.WebAPI.Models;

public class File
{
  public int FileId { get; set; }
  [Required]
  [MaxLength(50)]
  public string? Name { get; set; }
  [Required]
  [MaxLength(10)]
  public string? Extension { get; set; }
  [Required]
  public byte[]? Data { get; set; }
  public int ClientId { get; set; }
  public Client? Client { get; set; }
  public DateTime? ChangeDate { get; set; }
}
