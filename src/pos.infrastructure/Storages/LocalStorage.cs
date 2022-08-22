using pos.core.Services;

namespace pos.infrastructure.Storages;

public class AzureBlobStorage : IStorageService
{
    public Task<(string fullPath, string fileName)> SaveAsync(Stream file, string fileName, string path)
    {
        throw new NotImplementedException();
    }
}

public class LocalStorage : IStorageService
{
    private readonly IPathResolver pathResolver;

    public LocalStorage(IPathResolver pathResolver)
    {
        this.pathResolver = pathResolver;
    }

    public async Task<(string fullPath, string fileName)> SaveAsync(Stream file, string fileName, string path)
    {
        if (!Directory.Exists(path))
        {
            // todo: recheck path
            Directory.CreateDirectory(path);
        }

        // todo: limit file size

        var fullPath = Path.Combine(path, fileName);

        await File.WriteAllBytesAsync(fullPath, ReadBytes(file));

        return (fullPath, fileName);
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
