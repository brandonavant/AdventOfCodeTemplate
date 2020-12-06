using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AdventOfCodeTemplate.Base.Exceptions;
using AdventOfCodeTemplate.Base.Models;
using AdventOfCodeTemplate.Base.Rules;

namespace AdventOfCodeTemplate.Base.Services
{
    public class InputService : IInputService
    {
        public async Task<DayInput> GetInput(int year, int day)
        {
            HttpResponseMessage response;
            CookieContainer cookieContainer;
            HttpClientHandler httpClientHandler;

            var baseUri = new Uri("https://adventofcode.com");
            var aocSession = Environment.GetEnvironmentVariable("AOC_SESSION");

            string data;

            CheckRule(new YearAndDayMustBeValidRule(year, day));
            CheckRule(new AocEnvironmentVarMustBeSetRule(aocSession));

            cookieContainer = new CookieContainer();
            httpClientHandler = new HttpClientHandler() { CookieContainer = cookieContainer };

            using (var httpClient = new HttpClient(httpClientHandler) { BaseAddress = baseUri })
            {
                cookieContainer.Add(
                    baseUri,
                    new Cookie("session", aocSession)
                );

                response = await httpClient.GetAsync($"/{year}/day/{day}/input");
            }

            data = await response.Content.ReadAsStringAsync();

            return new DayInput(data);
        }

        public void CheckRule(IRule rule)
        {
            if(rule.IsBroken())
            {
                throw new RuleValidationException(rule);
            }
        }
    }
}
