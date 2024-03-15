namespace ReflectionPropertyInfoDemo
{
    using System;
    using System.Reflection;

    public class MyClass
    {
        public string MyProperty { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            MyClass obj = new MyClass();
            obj.MyProperty = "Hello World!";

            // Get the PropertyInfo for the property
            PropertyInfo propertyInfo = typeof(MyClass).GetProperty("MyProperty");

            // Get the value of the property
            object value = propertyInfo.GetValue(obj);

            Console.WriteLine(value);  // Output: Hello, World!
        }
    }
}
