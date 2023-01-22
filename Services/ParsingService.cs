using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using WebParser.Loggers;
using WebParser.Options;
using WebParser.Parser;

namespace WebParser.Services;

public class ParsingService : BackgroundService
{
    private readonly ILogger _logger;
    private readonly IContentParser _contentParser;
    private readonly IHostApplicationLifetime _lifeTime;
    private readonly ServiceOptions _configuration;

    public ParsingService(ILogger logger,
                          IContentParser contentParser,
                          IHostApplicationLifetime lifeTime,
                          IOptions<ServiceOptions> configuration)
    {
        _logger = logger;
        _contentParser = contentParser;
        _lifeTime = lifeTime;
        _configuration = configuration.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var urlAddress = _configuration.UrlAddress;

        if (string.IsNullOrWhiteSpace(urlAddress))
        {
            _logger.Log("Введите адрес сайта для подсчета уникальных слов");

            urlAddress = Console.ReadLine();
        }

        await _contentParser.Parse(urlAddress);

        _lifeTime.StopApplication();
    }
}