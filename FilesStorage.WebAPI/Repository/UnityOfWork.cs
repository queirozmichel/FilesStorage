using FilesStorage.WebAPI.Context;

namespace FilesStorage.WebAPI.Repository;

public class UnityOfWork : IUnitOfWork
{
  private AddressRepository _addressRepository;
  private ClientRepository _clientRepository;
  private FileRepository _fileRepository;
  public WebAPIContext _context;

  public UnityOfWork(WebAPIContext context)
  {
    _context = context;
  }

  public IAddressRepository AddressRepository
  {
    get
    {
      return _addressRepository = _addressRepository ?? new AddressRepository(_context);
    }
  }

  public IClientRepository ClientRepository
  {
    get
    {
      return _clientRepository = _clientRepository ?? new ClientRepository(_context);
    }
  }

  public IFileRepository FileRepository
  {
    get
    {
      return _fileRepository = _fileRepository ?? new FileRepository(_context);
    }
  }

  public void Commit()
  {
    _context.SaveChanges();
  }

  public void Dispose()
  {
    _context.Dispose();
  }
}