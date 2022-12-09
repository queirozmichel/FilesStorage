namespace FilesStorage.WebAPI.DTOs
{
  public class ClientDTO
  {
    public int ClientId { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
    public string? Sex { get; set; }
    public string? CPF { get; set; }
    public string? PhoneNumber { get; set; }
    public ICollection<AddressDTO>? Addresses { get; set; } //Propriedade de navegação
    public ICollection<FileDTO>? Files { get; set; } //Propriedade de navegação
  }
}
