namespace pos.core.Services;

public interface IStorageService
{
    /// <summary>
    /// Save the given file with given name
    /// </summary>
    /// <param name="file">File stream</param>
    /// <param name="fileName">Name of file</param>
    /// <returns>Full path of saved file</returns>
    Task<string> SaveAsync(Stream file, string fileName);
}
