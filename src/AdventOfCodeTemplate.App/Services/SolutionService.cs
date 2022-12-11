using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCodeTemplate.Base.Models;
using AdventOfCodeTemplate.Base.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AdventOfCodeTemplate.App.Services
{
    public class SolutionService : ISolutionService
    {
        // TODO: Refactor this entire service to better accommodate multiple years.
        struct ElfCalories
        {
            public List<int> CalorieCollection;

            public int CalorieTotal => CalorieCollection.Sum();

            public ElfCalories()
            {
                CalorieCollection = new();
            }
        }

        private readonly ILogger _logger;
        private readonly IInputService _inputService;

        public SolutionService(
            ILogger<SolutionService> logger,
            IConfiguration config,
            IInputService inputService)
        {
            _logger = logger;
            _inputService = inputService;
        }

        public async Task Run()
        {
            await DayOne();
        }

        private async Task DayOne()
        {
            _logger.LogInformation("Running DayOne.");

            DayInput input = await _inputService.GetInput(2022, 1);
            List<ElfCalories> evlesCalories = new();
            ElfCalories currentElfsCalories = new();

            int topThreeElvesCalories;

            foreach (var line in input.Data)
            {
                if (string.IsNullOrEmpty(line))
                {
                    evlesCalories.Add(currentElfsCalories); // We're finished with this one, add it to our collection.
                    currentElfsCalories = new ElfCalories(); // Start a new one.

                    continue;
                }

                if (!int.TryParse(line, out int calorieLineEntry))
                {
                    throw new ArgumentException($"Invalid input '{line}'.");
                }

                currentElfsCalories.CalorieCollection.Add(calorieLineEntry);
            }

            _logger.LogInformation("The elf with the most calories has {NumberOfCalories} calories.", evlesCalories.Max(x => x.CalorieTotal));

            topThreeElvesCalories = evlesCalories
                .OrderByDescending(calories => calories.CalorieTotal)
                .Take(3)
                .Sum(calories => calories.CalorieTotal);

            _logger.LogInformation("The sum of the top three elves with the most calories is {NumberOfCalories} calories.", topThreeElvesCalories);
        }
    }
}
