using FilesStorage.WebAPI.Models;

namespace FilesStorage.WebAPI.Repository;

public interface IClientRepository : IRepository<Client>
{
  IEnumerable<Client> GetMaleClients();
}
