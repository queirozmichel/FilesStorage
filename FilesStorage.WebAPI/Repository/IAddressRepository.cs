using FilesStorage.WebAPI.Models;
using FilesStorage.WebAPI.Pagination;

namespace FilesStorage.WebAPI.Repository;

public interface IAddressRepository : IRepository<Address>
{
  Task<IEnumerable<Address>> GetAddressesByClientId(int id);
  Task<PagedList<Address>> GetAddresses(AddressesParameters addressesParameters);
}