using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeTemplate.Base.Rules
{
    public class AocEnvironmentVarMustBeSetRule : IRule
    {
        private readonly string _aocSession;
        public string Message => "Could not find expected environment variable 'AOC_SESSION'.";

        public AocEnvironmentVarMustBeSetRule(string aocSession)
        {
            _aocSession = aocSession;
        }

        public bool IsBroken()
        {
            return string.IsNullOrEmpty(_aocSession);
        }
    }
}
