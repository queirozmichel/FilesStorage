using FilesStorage.WebAPI.Context;
using FilesStorage.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilesStorage.WebAPI.Repository;

public class ClientRepository : Repository<Client>, IClientRepository
{
  public ClientRepository(WebAPIContext context) : base(context)
  {
  }

  public IEnumerable<Client> GetClientsWithAddresses()
  {
    return GetAll().Include(cliente => cliente.Addresses);
  }

  public IEnumerable<Client> GetMaleClients()
  {
    return GetAll().Where(client => client.Sex == "M");
  }
}
