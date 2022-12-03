using File = FilesStorage.WebAPI.Models.File;

namespace FilesStorage.WebAPI.Repository;

public interface IFileRepository : IRepository<File>
{
  IEnumerable<File> GetFilesByClientId(int id);
}
