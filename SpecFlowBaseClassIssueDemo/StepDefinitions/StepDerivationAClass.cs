using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowBaseClassIssueDemo.StepDefinitions
{
    [Binding]
    public class StepDerivationAClass: StepBaseClass
    {
        [Given(@"boz")]
        public void GivenBoz()
        {
            Console.WriteLine("this is step boz");
        }

    }
}
