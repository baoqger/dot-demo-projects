﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowBaseClassIssueDemo.StepDefinitions
{
    [Binding]
    public class StepDerivationAClass: StepBaseClass
    {
        //public StepDerivationAClass(ScenarioContext scenarioContext) : base(scenarioContext)
        //{
        //}

        [Given(@"boz")]
        public void GivenBoz()
        {
            Thread.Sleep(3000);
            Console.WriteLine("this is step boz");
        }

        [Given(@"foo")]
        public void GivenFoo()
        {
            Thread.Sleep(3000);
            Console.WriteLine("This is step foo");
        }
    }
}
