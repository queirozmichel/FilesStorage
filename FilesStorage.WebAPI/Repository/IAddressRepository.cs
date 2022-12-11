using FilesStorage.WebAPI.Models;
using FilesStorage.WebAPI.Pagination;

namespace FilesStorage.WebAPI.Repository;

public interface IAddressRepository : IRepository<Address>
{
  IEnumerable<Address> GetAddressesByClientId(int id);
  PagedList<Address> GetAddresses(AddressesParameters addressesParameters);
}