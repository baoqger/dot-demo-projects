using System.Runtime.CompilerServices;

namespace SlideDetector
{
    public class Program
    {
        static void Main(string[] args)
        {
            var states = GetRigStateData();
            Console.WriteLine("Rig State count: " + states.Count);
            var s = new SlideDetector();
            states.ForEach(x => { 
                s.RigStateHandle(x);
            });
        }

        static public List<RigState> GetRigStateData() {
            var rigStateData = new List<RigState> {
                new RigState { TimeIndex = "T1", State = 0 },
                new RigState { TimeIndex = "T2", State = 1 },
                new RigState { TimeIndex = "T3", State = 0 },
                new RigState { TimeIndex = "T4", State = 0 },
                new RigState { TimeIndex = "T5", State = 1 },
                new RigState { TimeIndex = "T6", State = 2 },
                new RigState { TimeIndex = "T7", State = 3 },
                new RigState { TimeIndex = "T8", State = 4 },
                new RigState { TimeIndex = "T9", State = 1 },
                new RigState { TimeIndex = "T10", State = 6 }
            };
            return rigStateData;
        }
    }

    public class SlideDetector {
        public int currentState { get; set; } = int.MinValue;
        public string startTimeIndex = string.Empty;
        public void RigStateHandle(RigState newState) {
            if (currentState == int.MinValue) {
                currentState = newState.State;
                startTimeIndex = newState.TimeIndex;
                return;
            }
            if (currentState == 0 || currentState == 1)
            {
                if ( currentState == newState.State) {
                    return;
                }
                // slide or rotary interval detected, run the computation
                var endTimeIndex = newState.TimeIndex;
                Console.WriteLine($"State {currentState} from {startTimeIndex} to {endTimeIndex}");
                currentState = newState.State;
                startTimeIndex = endTimeIndex;
            }
            else {
                //  ignore other conditions
                currentState = newState.State;
                startTimeIndex = newState.TimeIndex;
            }
        }
    }

    public class RigState {
        public string TimeIndex { get; set; }
        public int State { get; set; }
    }
}
