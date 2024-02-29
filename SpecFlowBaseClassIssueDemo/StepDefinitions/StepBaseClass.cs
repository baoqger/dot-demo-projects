using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowBaseClassIssueDemo.StepDefinitions
{
    public class StepBaseClass
    {
        [Given(@"foo")]
        public void GivenFoo()
        {
            Console.WriteLine("This is step foo");
        }

    }
}
