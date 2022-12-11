using FilesStorage.WebAPI.Context;
using FilesStorage.WebAPI.Pagination;
using Microsoft.EntityFrameworkCore;
using File = FilesStorage.WebAPI.Models.File;

namespace FilesStorage.WebAPI.Repository;

public class FileRepository : Repository<File>, IFileRepository
{
  public FileRepository(AppDbContext context) : base(context)
  {
  }

  public async Task<IEnumerable<File>> GetFilesByClientId(int id)
  {
    return await Get().Where(f => f.ClientId == id).ToListAsync();
  }

  public async Task<PagedList<File>> GetFiles(FilesParameters filesParameters)
  {
    return await PagedList<File>.ToPagedList(Get().OrderBy(f => f.Name), filesParameters.PageNumber, filesParameters.PageSize);
  }
}
