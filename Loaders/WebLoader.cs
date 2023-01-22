using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using WebParser.Loggers;

namespace WebParser.Loaders;

public class WebLoader : IResurceLoader
{
    private readonly ILogger _logger;

    public WebLoader(ILogger logger) => _logger = logger;

    public async Task<string?> Load(string resurcePath)
    {
        _logger.Log($"Обработка адреса {resurcePath}");
        
        var client = new HttpClient();
        using var response = await client.GetAsync(resurcePath, HttpCompletionOption.ResponseContentRead);
        
        if (response.StatusCode == HttpStatusCode.OK)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using var content = response.Content;
            return await content.ReadAsStringAsync();
        }
        
        _logger.Log($"Не удалось обработать адрес {resurcePath}. Попробуйте выполнить запрос позднее.");
        
        return null;
    }
}