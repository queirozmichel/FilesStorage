using FilesStorage.WebAPI.Context;
using FilesStorage.WebAPI.Models;
using FilesStorage.WebAPI.Pagination;
using Microsoft.EntityFrameworkCore;

namespace FilesStorage.WebAPI.Repository;

public class ClientRepository : Repository<Client>, IClientRepository
{
  public ClientRepository(AppDbContext context) : base(context)
  {
  }

  public async Task<IEnumerable<Client>> GetMaleClients()
  {
    return await Get().Where(client => client.Sex == "M").ToListAsync();
  }

  public async Task<PagedList<Client>> GetClients(ClientsParameters clientsParameters)
  {
    return await PagedList<Client>.ToPagedList(Get().OrderBy(c => c.Name), clientsParameters.PageNumber, clientsParameters.PageSize);
  }
}
