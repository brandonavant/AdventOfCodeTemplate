using AdventOfCodeTemplate.Base.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeTemplate.Base.Exceptions
{
    class RuleValidationException : Exception
    {
        public IRule BrokenRule { get; }

        public string Details { get; }

        public RuleValidationException(IRule brokenRule) : base(brokenRule.Message)
        {
            BrokenRule = brokenRule;
            Details = brokenRule.Message;
        }

        public override string ToString()
        {
            return $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}";
        }
    }
}
