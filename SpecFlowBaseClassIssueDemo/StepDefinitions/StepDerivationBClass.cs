﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowBaseClassIssueDemo.StepDefinitions
{
    [Binding]
    public class StepDerivationBClass: StepBaseClass
    {
        //public StepDerivationBClass(ScenarioContext scenarioContext) : base(scenarioContext)
        //{
        //}

        [Given(@"bar")]
        public void GivenBar()
        {
            Thread.Sleep(3000);
            Console.WriteLine("this is step bar");
        }

    }
}
