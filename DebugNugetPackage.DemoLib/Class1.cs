namespace DebugNugetPackage.DemoLib
{
    public class Person
    {
        public string Name { get; set; }

        public Person() {
            Name = "ChrisBao";
        }
        public void PrintName() {
            var name = "chris bao";
            Console.WriteLine("debug name " + Name);
        }
    }
}
