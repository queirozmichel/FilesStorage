using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilesStorage.WebAPI.Models;

public class File
{
  public int FileId { get; set; }
  [Required(ErrorMessage = "O nome é obrigatório")]
  [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre {2} e {1} caracteres")]
  public string? Name { get; set; }
  [Required(ErrorMessage = "A extensão é obrigatória")]
  [StringLength(30, MinimumLength = 2, ErrorMessage = "A extensão deve ter entre {2} e {1} caracteres" )]
  public string? Extension { get; set; }
  [Required(ErrorMessage = "O binário do arquivo é obrigatório")]
  public byte[]? Data { get; set; }
  [Required(ErrorMessage = "O id do cliente a qual ele é relacionado é obrigatório")]
  [Range(1, int.MaxValue, ErrorMessage = "O id do cliente deve ser maior que 0")]
  public int ClientId { get; set; } //Propriedade de navegação
  [JsonIgnore]
  public Client? Client { get; set; } //Propriedade de navegação
  public DateTime? ChangeDate { get; set; }
}