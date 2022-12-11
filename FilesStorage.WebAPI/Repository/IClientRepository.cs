using FilesStorage.WebAPI.Models;
using FilesStorage.WebAPI.Pagination;

namespace FilesStorage.WebAPI.Repository;

public interface IClientRepository : IRepository<Client>
{
  IEnumerable<Client> GetMaleClients();
  PagedList<Client> GetClients(ClientsParameters clientsParameters);
}