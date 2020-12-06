using System;
namespace AdventOfCodeTemplate.Base.Models
{
    public class DayInput
    {
        // TODO: Environment.NewLine didn't work on Windows. Need to try on macOS and diagnose.
        private const char _newLineChar = '\n';

        public string[] Data { get; set; }

        public DayInput(string data)
        {
            Data = data.Split(_newLineChar);
        }
    }
}
