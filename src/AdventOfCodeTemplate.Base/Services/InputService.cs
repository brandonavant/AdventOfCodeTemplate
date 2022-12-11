using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AdventOfCodeTemplate.Base.Exceptions;
using AdventOfCodeTemplate.Base.Models;
using AdventOfCodeTemplate.Base.Rules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AdventOfCodeTemplate.Base.Services
{
    public class InputService : IInputService
    {
        private readonly string _cacheLocation;
        private readonly string _aocSession;
        private readonly ILogger<InputService> _logger;

        public InputService(ILogger<InputService> logger)
        {
            _logger = logger;
            _aocSession = Environment.GetEnvironmentVariable("AOC_SESSION"); // TODO: Move to constant
        }

        public async Task<DayInput> GetInput(int year, int day)
        {
            HttpResponseMessage response;
            CookieContainer cookieContainer;
            HttpClientHandler httpClientHandler;
            FileInfo cachedFile;

            var baseUri = new Uri("https://adventofcode.com"); // TODO: Move this URL to appsettings.

            string data;

            CheckRule(new YearAndDayMustBeValidRule(year, day));
            CheckRule(new AocEnvironmentVarMustBeSetRule(_aocSession));

            data = await TryGetCachedInput(year, day);

            if (string.IsNullOrEmpty(data))
            {
                _logger.LogInformation("No cache for {Year}/{Day}. Retrieving from server.", year, day);

                cookieContainer = new CookieContainer();
                httpClientHandler = new HttpClientHandler() { CookieContainer = cookieContainer };

                using (var httpClient = new HttpClient(httpClientHandler) { BaseAddress = baseUri })
                {
                    cookieContainer.Add(
                        baseUri,
                        new Cookie("session", _aocSession) // TODO: Move "session" to constant.
                    );

                    response = await httpClient.GetAsync($"/{year}/day/{day}/input");
                }

                data = await response.Content.ReadAsStringAsync();

                cachedFile = new($"Cache/{year}/Day{day}.txt"); // TODO: DRY this.
                cachedFile.Directory.Create();

                await File.WriteAllTextAsync(cachedFile.FullName, data);
            }
            else
            {
                _logger.LogInformation("Found cache for {Year}/{Day}.", year, day);
            }

            return new DayInput(data);
        }

        private async Task<string> TryGetCachedInput(int year, int day)
        {
            var dayFilePath = $"Cache/{year}/Day{day}.txt"; // TODO: DRY this.

            if (!File.Exists(dayFilePath))
            {
                return string.Empty;
            }            

            return await File.ReadAllTextAsync(dayFilePath);
        }

        public void CheckRule(IRule rule)
        {
            if (rule.IsBroken())
            {
                throw new RuleValidationException(rule);
            }
        }
    }
}
