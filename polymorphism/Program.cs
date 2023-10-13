using System;

class Shape
{
    public virtual double CalculateArea()
    {
        return 0; // Default implementation for the base class
    }
}

class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public override double CalculateArea()
    {
        return Width * Height; // Specific implementation for Rectangle
    }
}

class Circle : Shape
{
    public double Radius { get; set; }

    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius; // Specific implementation for Circle
    }
}

class Program
{
    static void Main(string[] args)
    {
        Rectangle shape1 = new Rectangle { Width = 5, Height = 4 };
        Circle shape2 = new Circle { Radius = 3 };

        CalculateAndPrintArea(shape1);
        CalculateAndPrintArea(shape2);
    }

    static void CalculateAndPrintArea(Shape shape)
    {
        double area = shape.CalculateArea();
        Console.WriteLine("Area of the shape: " + area);
    }
}