using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceShareData
{
    internal class StageBase
    {
        protected ComputationContext Context;

        public StageBase(ComputationContext context = null)
        {
            Context = context;
        }
    }
}
