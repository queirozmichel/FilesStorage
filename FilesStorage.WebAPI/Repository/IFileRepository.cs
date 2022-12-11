using FilesStorage.WebAPI.Models;
using FilesStorage.WebAPI.Pagination;
using File = FilesStorage.WebAPI.Models.File;

namespace FilesStorage.WebAPI.Repository;

public interface IFileRepository : IRepository<File>
{
  IEnumerable<File> GetFilesByClientId(int id);
  PagedList<File> GetFiles(FilesParameters filesParameters);
}