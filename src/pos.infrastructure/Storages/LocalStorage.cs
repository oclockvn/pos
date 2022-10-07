using pos.core.Services;

namespace pos.infrastructure.Storages;

public class AzureBlobStorage : IStorageService
{
    public Task<string> SaveAsync(Stream file, string fileName)
    {
        throw new NotImplementedException();
    }
}

public class LocalStorage : IStorageService
{
    private readonly IPathResolver pathResolver;
    private const string FOLDER = "LocalStorage";

    public LocalStorage(IPathResolver pathResolver)
    {
        this.pathResolver = pathResolver;
    }

    public async Task<string> SaveAsync(Stream file, string fileName)
    {
        var path = Path.Combine(pathResolver.GetRootPath(), FOLDER);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        // todo: limit file size

        var fullPath = Path.Combine(path, fileName);

        await File.WriteAllBytesAsync(fullPath, ReadBytes(file));

        return fullPath;
    }

    static byte[] ReadBytes(Stream s)
    {
        s.Position = 0;
        var bytes = new byte[s.Length];

        using var writer = new BinaryWriter(s);
        writer.Write(bytes);

        return bytes;
    }
}
