using FilesStorage.WebAPI.Context;
using FilesStorage.WebAPI.Models;
using FilesStorage.WebAPI.Pagination;
using Microsoft.EntityFrameworkCore;

namespace FilesStorage.WebAPI.Repository;

public class AddressRepository : Repository<Address>, IAddressRepository
{
  public AddressRepository(WebAPIContext context) : base(context)
  {

  }

  public async Task<IEnumerable<Address>> GetAddressesByClientId(int id)
  {
    return await Get().Where(x => x.ClientId == id).ToListAsync();
  }

  public async Task<PagedList<Address>> GetAddresses(AddressesParameters addressesParameters)
  {
    return await PagedList<Address>.ToPagedList(Get().OrderBy(a => a.AddressId), addressesParameters.PageNumber, addressesParameters.PageSize);
  }
}
