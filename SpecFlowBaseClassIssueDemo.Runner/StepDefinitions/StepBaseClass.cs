using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SpecFlowBaseClassIssueDemo.StepDefinitions
{
    [Binding]
    public class StepBaseClass
    {
        public static ScenarioContext _scenarioContext;


        [BeforeTestRun]
        public static void StartInit()
        {
            Console.WriteLine("debug: before test run");
        }

        [BeforeScenario]
        public static void SetScenario(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            Console.WriteLine("debug: before scenario. " + _scenarioContext.ScenarioInfo.Title);
        }

        [AfterScenario]
        public static void CleanUpScenario()
        {
            Console.WriteLine("debug: after scenario. " + _scenarioContext.ScenarioInfo.Title);
        }

        public static void PrintStepText() {
            Console.WriteLine("debug: step text. " + _scenarioContext.StepContext.StepInfo.Text);
        }
    }
}
