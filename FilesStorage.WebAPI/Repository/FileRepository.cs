using FilesStorage.WebAPI.Context;
using FilesStorage.WebAPI.Models;
using FilesStorage.WebAPI.Pagination;
using File = FilesStorage.WebAPI.Models.File;

namespace FilesStorage.WebAPI.Repository;

public class FileRepository : Repository<File>, IFileRepository
{
  public FileRepository(WebAPIContext context) : base(context)
  {
  }

  public PagedList<File> GetFiles(FilesParameters filesParameters)
  {
    return PagedList<File>.ToPagedList(Get().OrderBy(f => f.Name), filesParameters.PageNumber, filesParameters.PageSize);
  }

  public IEnumerable<File> GetFilesByClientId(int id)
  {
    return Get().Where(f => f.ClientId == id);
  }
}
