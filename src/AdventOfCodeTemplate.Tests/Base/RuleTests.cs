using AdventOfCodeTemplate.Base.Exceptions;
using AdventOfCodeTemplate.Base.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCodeTemplate.Tests.Base
{
    public class RuleTests
    {
        [Fact]
        public void AocEnvironmentVarMustBeSetRule_EmptyString_BreaksRuleAndShowsException()
        {
            // Arrange
            var rule = new AocEnvironmentVarMustBeSetRule(string.Empty);
            var classUsingRule = new ClassUsingRule();

            // Assert
            Assert.Throws<RuleValidationException>(() => classUsingRule.CheckRule(rule));
        }

        public class ClassUsingRule
        {
            public void CheckRule(IRule rule)
            {
                if (rule.IsBroken())
                {
                    throw new RuleValidationException(rule);
                }
            }
        }
    }
}
