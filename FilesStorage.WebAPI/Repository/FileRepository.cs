using FilesStorage.WebAPI.Context;
using File = FilesStorage.WebAPI.Models.File;

namespace FilesStorage.WebAPI.Repository;

public class FileRepository : Repository<File>, IFileRepository
{
  public FileRepository(WebAPIContext context) : base(context)
  {
  }

  public IEnumerable<File> GetFilesByClientId(int id)
  {
    return Get().Where(f => f.ClientId == id);
  }
}
