namespace ShootingClub.WebAPI.Models;

public class Address
{
  public Address()
  {
    this.State = new char[2];
    this.CEP = new char[8];
  }
  public int AddressId { get; set; }
  public string? Street { get; set; }
  public int Number { get; set; }
  public string? District { get; set; }
  public string? City { get; set; }
  public char[] State { get; set; }
  public char[] CEP { get; set; }
  public DateTime ChangeDate { get; set; }
}
