using System;

    public interface IModular
    {
        double Module();
    }

public class ComplexNumber : ICloneable, IEquatable<ComplexNumber>, IModular
{
    private double re;
    private double im;

    public double Re
    {
        get { return re; }
        set { re = value; }
    }

    public double Im
    {
        get { return im; }
        set { im = value; }
    }

    public ComplexNumber(double re, double im)
    {
        this.re = re;
        this.im = im;
    }

    public override string ToString()
    {
        char znak = (im < 0) ? '-' : '+';
        return $"{re} {znak} {Math.Abs(im)}i";
    }

    public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(a.Re + b.Re, a.Im + b.Im);
    }

    public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(a.Re - b.Re, a.Im - b.Im);
    }

    public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
    {
        double newRe = (a.Re * b.Re) - (a.Im * b.Im);
        double newIm = (a.Re * b.Im) + (a.Im * b.Re);
        return new ComplexNumber(newRe, newIm);
    }

    public static ComplexNumber operator -(ComplexNumber a)
    {
        return new ComplexNumber(a.Re, -a.Im);
    }

    public object Clone()
    {
        return new ComplexNumber(this.re, this.im);
    }

    public double Module()
    {
        return Math.Sqrt(Math.Pow(re, 2) + Math.Pow(im, 2));
    }

    public bool Equals(ComplexNumber? other)
    {
        if (other is null) return false;
        return this.re == other.re && this.im == other.im;
    }

    public override bool Equals(object? obj)
    {
        if (obj is ComplexNumber other)
        {
            return Equals(other);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(re, im);
    }

    public static bool operator ==(ComplexNumber? a, ComplexNumber? b)
    {
        if (ReferenceEquals(a, null))
        {
            return ReferenceEquals(b, null);
        }
        return a.Equals(b);
    }

    public static bool operator !=(ComplexNumber? a, ComplexNumber? b)
    {
        return !(a == b);
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- TEST LICZB ZESPOLONYCH ---\n");

            ComplexNumber z1 = new ComplexNumber(3, 4);
            ComplexNumber z2 = new ComplexNumber(1, -2);

            Console.WriteLine($"Liczba z1: {z1}");
            Console.WriteLine($"Liczba z2: {z2}");

            ComplexNumber suma = z1 + z2;
            Console.WriteLine($"\nDodawanie ({z1}) + ({z2}) = {suma}");

            ComplexNumber roznica = z1 - z2;
            Console.WriteLine($"Odejmowanie ({z1}) - ({z2}) = {roznica}");

            ComplexNumber iloczyn = z1 * z2;
            Console.WriteLine($"Mnożenie ({z1}) * ({z2}) = {iloczyn}");

            ComplexNumber sprzezenie = -z1;
            Console.WriteLine($"\nSprzężenie liczby {z1} to: {sprzezenie}");

            Console.WriteLine($"Moduł liczby {z1} wynosi: {z1.Module()}");

            ComplexNumber z3 = new ComplexNumber(3, 4);
            Console.WriteLine($"\nCzy z1 jest równe z3? (z3 to nowa instancja 3+4i): {z1 == z3}");
            Console.WriteLine($"Czy z1 jest równe z2?: {z1.Equals(z2)}");

            ComplexNumber klon = (ComplexNumber)z1.Clone();
            Console.WriteLine($"\nKlon liczby {z1}: {klon}");

            klon.Re = 100;
            Console.WriteLine($"Po zmianie Re w klonie na 100:");
            Console.WriteLine($"Oryginał: {z1}");
            Console.WriteLine($"Klon: {klon}");

            Console.ReadKey();
        }
    }
}