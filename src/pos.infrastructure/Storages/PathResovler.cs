using pos.core.Services;

namespace pos.infrastructure.Storages;

public class PathResolver : IPathResolver
{
    private readonly string rootPath;

    public PathResolver(string rootPath)
    {
        this.rootPath = rootPath;
    }

    public string GetRootPath()
    {
        return rootPath;
    }
}
