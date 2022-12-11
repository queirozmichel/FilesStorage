using FilesStorage.WebAPI.Models;
using FilesStorage.WebAPI.Pagination;

namespace FilesStorage.WebAPI.Repository;

public interface IClientRepository : IRepository<Client>
{
  Task<IEnumerable<Client>> GetMaleClients();
  Task<PagedList<Client>> GetClients(ClientsParameters clientsParameters);
}