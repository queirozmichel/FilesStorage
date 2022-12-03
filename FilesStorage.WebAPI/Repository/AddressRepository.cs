using FilesStorage.WebAPI.Context;
using FilesStorage.WebAPI.Models;

namespace FilesStorage.WebAPI.Repository;

public class AddressRepository : Repository<Address>, IAddressRepository
{
  public AddressRepository(WebAPIContext context) : base(context)
  {

  }

  public IEnumerable<Address> GetAddressesByClientId()
  {
    return GetAll().OrderBy(x => x.ClientId).ToList();
  }
}
