using System;
namespace AdventOfCodeTemplate.Base.Models
{
    public class DayInput
    {
        public string[] Data { get; set; }

        public DayInput(string data)
        {
            Data = data.Split(Environment.NewLine);
        }
    }
}
