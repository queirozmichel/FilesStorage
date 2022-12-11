using Microsoft.EntityFrameworkCore;
using FilesStorage.WebAPI.Models;
using File = FilesStorage.WebAPI.Models.File;

namespace FilesStorage.WebAPI.Context;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {

  }

  public DbSet<Client>? Clients { get; set; }
  public DbSet<Address>? Addresses { get; set; }
  public DbSet<File>? Files { get; set; }
}
