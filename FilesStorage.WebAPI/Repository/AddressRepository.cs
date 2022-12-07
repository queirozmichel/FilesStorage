using FilesStorage.WebAPI.Context;
using FilesStorage.WebAPI.Models;

namespace FilesStorage.WebAPI.Repository;

public class AddressRepository : Repository<Address>, IAddressRepository
{
  public AddressRepository(WebAPIContext context) : base(context)
  {

  }

  public IEnumerable<Address> GetAddressesByClientId(int id)
  {
    return Get().Where(x => x.ClientId == id).ToList();
  }
}
