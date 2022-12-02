using Microsoft.EntityFrameworkCore;
using FilesStorage.WebAPI.Models;
using File = FilesStorage.WebAPI.Models.File;

namespace FilesStorage.WebAPI.Context;

public class WebAPIContext : DbContext
{
  public WebAPIContext(DbContextOptions<WebAPIContext> options) : base(options)
  {

  }

  public DbSet<Client>? Clients { get; set; }
  public DbSet<Address>? Addresses { get; set; }
  public DbSet<File>? Files { get; set; }
}
