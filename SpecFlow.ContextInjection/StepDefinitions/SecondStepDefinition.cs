using SpecFlowCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlow.ContextInjection.StepDefinitions
{
    [Binding]
    public class SecondStepDefinition
    {
        private Calculator _calculator;
        public SecondStepDefinition(Calculator calculator) {
            _calculator = calculator;
        }
        private int _result;

        [When("the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            //TODO: implement act (action) logic

            _result = _calculator.Add();
        }

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(int result)
        {
            //TODO: implement assert (verification) logic

            _result.Should().Be(result);
        }
    }
}
