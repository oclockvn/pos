namespace pos.core.Services;

public interface IStorageService
{
    Task<(string fullPath, string fileName)> SaveAsync(Stream file, string fileName, string path);
}
