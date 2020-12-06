using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeTemplate.Base.Rules
{
    public class YearAndDayMustBeValidRule : IRule
    {
        private readonly int _year;
        private readonly int _day;

        public string Message => "The year must not preceed 2015 " +
            "and the day must fall between 1 and 25.";

        public YearAndDayMustBeValidRule(int year, int day)
        {
            _year = year;
            _day = day;
        }

        public bool IsBroken()
        {
            return (_year < 2015 || (_day < 1 || _day > 25));
        }
    }
}
