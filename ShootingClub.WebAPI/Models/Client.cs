using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShootingClub.WebAPI.Models;

public class Client
{
  public Client()
  {
    Files = new Collection<File>();
  }
  public int ClientId { get; set; }
  [Required]
  [MaxLength(100)]
  public string? Name { get; set; }
  [Required]
  public int Age { get; set; }
  [Required]
  [Column(TypeName = "char(1)")]
  public string Sex { get; set; }
  [Required]
  [Column(TypeName = "char(11)")]
  public string? CPF { get; set; }
  [Required]
  [Column(TypeName = "char(11)")]
  public string? PhoneNumber { get; set; }
  public ICollection<Address>? Addresses { get; set; }
  public ICollection<File>? Files { get; set; }
  public DateTime? ChangeDate { get; set; }
}
