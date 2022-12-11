using FilesStorage.WebAPI.Pagination;
using File = FilesStorage.WebAPI.Models.File;

namespace FilesStorage.WebAPI.Repository;

public interface IFileRepository : IRepository<File>
{
  Task<IEnumerable<File>> GetFilesByClientId(int id);
  Task<PagedList<File>> GetFiles(FilesParameters filesParameters);
}