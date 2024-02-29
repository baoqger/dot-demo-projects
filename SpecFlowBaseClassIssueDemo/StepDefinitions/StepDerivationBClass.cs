using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowBaseClassIssueDemo.StepDefinitions
{
    [Binding]
    public class StepDerivationBClass: StepBaseClass
    {
        [Given(@"bar")]
        public void GivenBar()
        {
            Console.WriteLine("this is step bar");
        }

    }
}
