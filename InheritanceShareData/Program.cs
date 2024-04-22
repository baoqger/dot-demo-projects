using System.Numerics;

namespace InheritanceShareData
{
    public class Program
    {
        // private ComputationContext Context;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var Context = new ComputationContext();
            var Context2 = new ComputationContext();
            var stageRig = new StageRig(Context);
            var stageChannel = new StageChannel(Context2);

            Console.WriteLine("debug: " + stageChannel.Process());

            stageRig.Process();

            Console.WriteLine("debug: " + stageChannel.Process());

        }
    }
}
