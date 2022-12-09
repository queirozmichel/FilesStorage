namespace FilesStorage.WebAPI.DTOs
{
  public class AddressDTO
  {
    public int AddressId { get; set; }
    public string? Street { get; set; }
    public int Number { get; set; }
    public string? District { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? CEP { get; set; }
    public int ClientId { get; set; } //Propriedade de navegação 
  }
}
