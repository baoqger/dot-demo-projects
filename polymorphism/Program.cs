using System;

class Shape: IShape
{
    public virtual double CalculateArea()
    {
        return 0; // Default implementation for the base class
    }

    public void Print() {
        throw new NotImplementedException();
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

    public new void Print() {
        Console.WriteLine("This is shape Rectangle");
    }
}

class Circle : Shape
{
    public double Radius { get; set; }

    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius; // Specific implementation for Circle
    }

    public new void Print()
    {
        Console.WriteLine("This is shape Circle");
    }
}

interface IShape {
    public void Print();

    public double CalculateArea();
}


class Program
{
    static void Main(string[] args)
    {
        // Rectangle shape1 = new Rectangle { Width = 5, Height = 4 };
        Circle shape2 = new Circle { Radius = 3 };

        // CalculateAndPrintArea(shape1);
        CalculateAndPrintArea(shape2);
    }

    static void CalculateAndPrintArea(IShape  shape)
    {
        double area = shape.CalculateArea();
        Console.WriteLine("Area of the shape: " + area);
        shape.Print();
    }
}


