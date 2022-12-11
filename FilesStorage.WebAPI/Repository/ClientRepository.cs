using FilesStorage.WebAPI.Context;
using FilesStorage.WebAPI.Models;
using FilesStorage.WebAPI.Pagination;

namespace FilesStorage.WebAPI.Repository;

public class ClientRepository : Repository<Client>, IClientRepository
{
  public ClientRepository(WebAPIContext context) : base(context)
  {
  }

  public PagedList<Client> GetClients(ClientsParameters clientsParameters)
  {
    return PagedList<Client>.ToPagedList(Get().OrderBy(c => c.Name), clientsParameters.PageNumber, clientsParameters.PageSize);
  }

  public IEnumerable<Client> GetMaleClients()
  {
    return Get().Where(client => client.Sex == "M");
  }
}
