using System.Threading.Tasks;

namespace WebParser.Loaders;

public interface IResurceLoader
{
    Task<string?> Load(string resurcePath);
}