using FilesStorage.WebAPI.Context;
using FilesStorage.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilesStorage.WebAPI.Repository;

public class ClientRepository : Repository<Client>, IClientRepository
{
  public ClientRepository(WebAPIContext context) : base(context)
  {
  }

  public IEnumerable<Client> GetMaleClients()
  {
    return Get().Where(client => client.Sex == "M");
  }
}
