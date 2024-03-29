using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceShareData
{
    internal class StageRig: StageBase
    {
        public StageRig(ComputationContext context) :base(context) { }

        public void Process() {
            Context.SetRigState("Rotate");
        }
    }
}
