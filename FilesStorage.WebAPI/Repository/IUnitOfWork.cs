namespace FilesStorage.WebAPI.Repository;

public interface IUnitOfWork
{
  IAddressRepository AddressRepository { get; }
  IClientRepository ClientRepository { get; }
  IFileRepository FileRepository { get; }
  void Commit();
}
