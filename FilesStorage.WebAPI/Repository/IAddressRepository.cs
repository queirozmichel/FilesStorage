using FilesStorage.WebAPI.Models;

namespace FilesStorage.WebAPI.Repository;

public interface IAddressRepository : IRepository<Address>
{
  IEnumerable<Address> GetAddressesByClientId();
}
