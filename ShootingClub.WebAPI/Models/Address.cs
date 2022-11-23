using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ShootingClub.WebAPI.Models;

public class Address
{
  public int AddressId { get; set; }
  [Required]
  [MaxLength(50)]
  public string? Street { get; set; }
  [Required]
  public int Number { get; set; }
  [Required]
  [MaxLength(50)]
  public string? District { get; set; }
  [Required]
  [MaxLength(30)]
  public string? City { get; set; }
  [Required]
  [Column(TypeName = "char(2)")]
  public string? State { get; set; }
  [Required]
  [Column(TypeName = "char(8)")]
  public string? CEP { get; set; }
  [Required]
  public int ClientId { get; set; } //Propriedade de navegação
  [JsonIgnore]
  public Client? Cliente { get; set; } //Propriedade de navegação
  public DateTime? ChangeDate { get; set; }
}