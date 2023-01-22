using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using WebParser.Loaders;
using WebParser.Loggers;

namespace WebParser.Parser;

public class ContentParser : IContentParser
{
    private readonly ILogger _logger;
    private readonly IResurceLoader _resurceLoader;

    public ContentParser(ILogger logger, IResurceLoader resurceLoader)
    {
        _logger = logger;
        _resurceLoader = resurceLoader;
    }

    public async Task Parse(string? path)
    {
        ArgumentNullException.ThrowIfNull(path);
        
        try
        {
            var resurces = await _resurceLoader.Load(path);
            var words = GetWords(resurces);

            var result = words?.GroupBy(x => x)
                               .Select(x => (Word: x.Key, Frequency: x.Count()))
                               .OrderByDescending(x => x.Frequency)
                               .ToList();


            LogResult(path, result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ошибка при обработке адреса {path}", ex);
        }
    }

    private IEnumerable<string>? GetWords(string? rawString)
    {
        if (!string.IsNullOrWhiteSpace(rawString))
        {
            rawString = Regex.Replace(rawString, @"<script[^>]*>[\s\S]*?</script>", string.Empty);
            rawString = Regex.Replace(rawString, @"<style[^>]*>[\s\S]*?</style>", string.Empty);
            rawString = Regex.Replace(rawString, @"<[^>]*>", string.Empty).ToUpper();
        }

        var delimiterChars = new[] {' ', ',', '.', '!', '?', '"', ';', ':', '&', '[', ']', '(', ')', '\n', '\r', '\t'};
        var words = rawString?.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

        return words;
    }

    private void LogResult(string? path, List<(string Word, int Frequency)>? result)
    {
        _logger.Log($"Обработка ресурса {path} завершена. Результаты обработки:");
        
        if (result != null)
            foreach (var item in result)
            {
                _logger.Log($"Слово: {item.Word}\tКоличество повторов: {item.Frequency.ToString()}");
            }
        else
            _logger.Log("В указанном ресурсе не найдены слова");
    }
}