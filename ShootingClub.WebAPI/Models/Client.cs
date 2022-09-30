namespace ShootingClub.WebAPI.Models;

public class Client
{
  public Client()
  {
    this.CPF = new char[11];
    this.PhoneNumber = new char[11];
  }
  public int ClientId { get; set; }
  public string? Name { get; set; }
  public int Age { get; set; }
  public char Sex { get; set; }
  public char[]? CPF { get; set; }
  public char[]? PhoneNumber { get; set; }
  public DateTime ChangeDate { get; set; }
}
