using System.Threading.Tasks;
using AdventOfCodeTemplate.Base.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AdventOfCodeTemplate.App.Services
{
    public class SolutionService : ISolutionService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private readonly IInputService _inputService;

        public SolutionService(
            ILogger<SolutionService> logger,
            IConfiguration config,
            IInputService inputService)
        {
            _logger = logger;
            _config = config;
            _inputService = inputService;
        }

        public async Task Run()
        {
            var x = await  _inputService.GetInput(2020, 1);
        }
    }
}
