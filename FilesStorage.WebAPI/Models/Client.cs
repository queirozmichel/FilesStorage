using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilesStorage.WebAPI.Models;

public class Client
{
  public Client()
  {
    Files = new Collection<File>();
    Addresses = new Collection<Address>();
  }
  public int ClientId { get; set; }
  [Required(ErrorMessage = "O nome é obrigatório")]
  [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre {2} e {1} caracteres")]
  public string? Name { get; set; }
  [Required(ErrorMessage = "A idade é obrigatória")]
  [Range(18, int.MaxValue, ErrorMessage = "A idade não pode ser menor que {1}")]
  public int Age { get; set; }
  [Required(ErrorMessage = "O sexo é obrigatório")]
  [StringLength(1, MinimumLength = 1, ErrorMessage = "O sexo deve ter {1} caracter")]
  [Column(TypeName = "char(1)")]
  public string? Sex { get; set; }
  [Required(ErrorMessage = "O CPF é obrigatório")]
  [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter {1} caracteres")]
  [Column(TypeName = "char(11)")]
  public string? CPF { get; set; }
  [Required(ErrorMessage = "O telefone é obrigatório")]
  [StringLength(11, MinimumLength = 11, ErrorMessage = "O telefone deve ter {1} caracteres")]
  [Column(TypeName = "char(11)")]
  public string? PhoneNumber { get; set; }
  public ICollection<Address>? Addresses { get; set; } //Propriedade de navegação
  public ICollection<File>? Files { get; set; } //Propriedade de navegação
  public DateTime? ChangeDate { get; set; }
}