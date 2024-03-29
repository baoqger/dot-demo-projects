using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceShareData
{
    internal class ComputationContext
    {
        private string RigState = "Slide";

        public void SetRigState(string state) { 
            RigState = state;
        }

        public string GetRigState() { 
            return RigState;
        }
    }
}
