namespace ShootingClub.WebAPI.Models;

public class File
{
  public int FileId { get; set; }
  public string? Name { get; set; }
  public string? Extension { get; set; }
  public byte[]? Data { get; set; }
  public DateTime ChangeDate { get; set; }
}
