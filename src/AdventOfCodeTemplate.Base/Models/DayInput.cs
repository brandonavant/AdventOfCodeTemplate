using System;
namespace AdventOfCodeTemplate.Base.Models
{
    public class DayInput
    {
        private const char _newLineChar = '\n';

        public string[] Data { get; set; }

        public DayInput(string data)
        {
            Data = data.Split(_newLineChar);
        }
    }
}
