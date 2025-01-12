using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskCancellation
{
    public class RemoteService
    {
        public async Task<int> GetIntAsync() {
            await Task.Delay(1000);
            return 1;
        }
    }
}
