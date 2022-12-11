using FilesStorage.WebAPI.Context;
using FilesStorage.WebAPI.Models;
using FilesStorage.WebAPI.Pagination;

namespace FilesStorage.WebAPI.Repository;

public class AddressRepository : Repository<Address>, IAddressRepository
{
  public AddressRepository(WebAPIContext context) : base(context)
  {

  }

  public PagedList<Address> GetAddresses(AddressesParameters addressesParameters)
  {
    return PagedList<Address>.ToPagedList(Get().OrderBy(a => a.AddressId), addressesParameters.PageNumber, addressesParameters.PageSize);
  }

  public IEnumerable<Address> GetAddressesByClientId(int id)
  {
    return Get().Where(x => x.ClientId == id).ToList();
  }
}
