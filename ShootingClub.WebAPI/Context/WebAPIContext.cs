using Microsoft.EntityFrameworkCore;
using ShootingClub.WebAPI.Models;
using File = ShootingClub.WebAPI.Models.File;

namespace ShootingClub.WebAPI.Context;

public class WebAPIContext : DbContext
{
  public WebAPIContext(DbContextOptions<WebAPIContext> options) : base(options)
  {

  }

  public DbSet<Client>? Clients { get; set; }
  public DbSet<Address>? Addresses { get; set; }
  public DbSet<File>? Files { get; set; }
}
