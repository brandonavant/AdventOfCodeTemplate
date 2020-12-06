using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AdventOfCodeTemplate.Base.Models;

namespace AdventOfCodeTemplate.Base.Services
{
    public class InputService : IInputService
    {
        public async Task<DayInput> GetInput(int year, int day)
        {
            HttpResponseMessage response;
            Uri baseUri = new Uri("https://adventofcode.com");
            string aocSession = Environment.GetEnvironmentVariable("AOC_SESSION");

            if (year < 2015 || (day < 1 || day > 25))
            {
                throw new ArgumentOutOfRangeException("The year must not preceed 2015 " +
                    "and the day must fall between 1 and 25.");
            }

            if (string.IsNullOrEmpty(aocSession))
            {
                throw new ArgumentNullException("Could not find expected environment variable 'AOC_SESSION'.");
            }

            // TODO: Use CheckRule(new RuleToCheckGoesHere(var1, var2));

            var cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler() { CookieContainer = cookieContainer };

            using (var httpClient = new HttpClient(handler) { BaseAddress = baseUri })
            {
                cookieContainer.Add(
                    baseUri,
                    new Cookie("session", aocSession)
                );

                response = await httpClient.GetAsync($"/{year}/day/{day}/input");
            }
            var data = await response.Content.ReadAsStringAsync();

            return new DayInput(data);
        }
    }
}
