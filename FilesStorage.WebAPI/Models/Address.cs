using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FilesStorage.WebAPI.Models;

public class Address
{
  public int AddressId { get; set; }
  [Required(ErrorMessage = "A rua é obrigatória")]
  [StringLength(50, MinimumLength = 2, ErrorMessage = "A rua deve ter entre {2} e {1} caracteres")]
  public string? Street { get; set; }
  [Required(ErrorMessage = "O número é obrigatório")]
  [Range(1, int.MaxValue, ErrorMessage = "O número deve ser maior que 0")]
  public int Number { get; set; }
  [Required(ErrorMessage = "O bairro é obrigatório")]
  [StringLength(50, MinimumLength = 2, ErrorMessage = "O bairro deve ter entre {2} e {1} caracteres")]
  public string? District { get; set; }
  [Required(ErrorMessage = "A cidade é obrigatória")]
  [StringLength(30, MinimumLength = 2, ErrorMessage = "A cidade deve ter entre {2} e {1} caracteres")]
  public string? City { get; set; }
  [Required(ErrorMessage = "O estado é obrigatório")]
  [StringLength(2, MinimumLength = 2, ErrorMessage = "O estado deve ter {1} caracteres")]
  [Column(TypeName = "char(2)")]
  public string? State { get; set; }
  [Required(ErrorMessage = "O CEP é obrigatório")]
  [StringLength(8, MinimumLength = 8, ErrorMessage = "O CEP deve ter {1} caracteres")]
  [Column(TypeName = "char(8)")]
  public string? CEP { get; set; }
  [Required(ErrorMessage = "O id do cliente é obrigatório")]
  [Range(1, int.MaxValue, ErrorMessage = "O id do cliente deve ser maior que 0")]
  public int ClientId { get; set; } //Propriedade de navegação
  [JsonIgnore]
  public Client? Cliente { get; set; } //Propriedade de navegação
  public DateTime? ChangeDate { get; set; }
}