using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceShareData
{
    internal class StageChannel: StageBase
    {
        public StageChannel(ComputationContext context): base(context) { }

        public string Process()
        {
            return Context.GetRigState();
        }
    }
}
