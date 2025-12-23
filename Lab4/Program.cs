using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab4
{
    public interface IModular
    {
        double Module();
    }

    public class ComplexNumber : ICloneable, IEquatable<ComplexNumber?>, IModular, IComparable<ComplexNumber?>
    {
        private double re;
        private double im;
        public double Re { get => re; set => re = value; }
        public double Im { get => im; set => im = value; }

        public ComplexNumber(double re, double im)
        {
            this.re = re; this.im = im;
        }

        public override string ToString()
        {
            string sign = im >= 0 ? "+" : "-";
            return $"{re} {sign} {Math.Abs(im)}i";
        }

        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
            => new ComplexNumber(a.re + b.re, a.im + b.im);
        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
            => new ComplexNumber(a.re - b.re, a.im - b.im);
        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
            => new ComplexNumber(a.re * b.re - a.im * b.im, a.re * b.im + a.im * b.re);
        public static ComplexNumber operator -(ComplexNumber a)
            => new ComplexNumber(a.re, -a.im);

        public object Clone() => new ComplexNumber(re, im);

        // Zmiana: parametr może być nullem (ComplexNumber?)
        public bool Equals(ComplexNumber? other)
        {
            if (other is null) return false;
            return re == other.re && im == other.im;
        }

        public override bool Equals(object? obj)
            => obj is ComplexNumber other && Equals(other);

        public override int GetHashCode()
            => HashCode.Combine(re, im);

        public static bool operator ==(ComplexNumber? a, ComplexNumber? b)
        {
            if (ReferenceEquals(a, null)) return ReferenceEquals(b, null);
            return a.Equals(b);
        }

        public static bool operator !=(ComplexNumber? a, ComplexNumber? b)
            => !(a == b);

        public double Module()
            => Math.Sqrt(re * re + im * im);

        public int CompareTo(ComplexNumber? other)
        {
            if (other is null) return 1; 
            return this.Module().CompareTo(other.Module());
        }
    }

    class Program
    {
        static void WypiszKolekcje<T>(IEnumerable<T> kolekcja, string opis)
        {
            Console.WriteLine($"\n--- {opis} ---");
            foreach (var item in kolekcja)
            {
                Console.WriteLine(item?.ToString() ?? "null");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("=== ZADANIE 2: TABLICA ===");

            ComplexNumber[] tablica = new ComplexNumber[]
            {
                new ComplexNumber(3, 4),
                new ComplexNumber(1, -1),
                new ComplexNumber(0, 5),
                new ComplexNumber(2, 2),
                new ComplexNumber(-1, -2)
            };

            foreach (var item in tablica) Console.WriteLine(item);

            Array.Sort(tablica);
            WypiszKolekcje(tablica, "Posortowana tablica");

            Console.WriteLine($"Minimum: {tablica.Min()}");
            Console.WriteLine($"Maksimum: {tablica.Max()}");

            var ujemneUrojone = tablica.Where(z => z.Im < 0).ToArray();
            WypiszKolekcje(ujemneUrojone, "Ujemna czesc urojona");

            Console.WriteLine("\n=== ZADANIE 3: LISTA ===");

            List<ComplexNumber> lista = new List<ComplexNumber>
            {
                new ComplexNumber(10, 1),
                new ComplexNumber(2, 2),
                new ComplexNumber(5, 5),
                new ComplexNumber(1, -3),
                new ComplexNumber(0, 0)
            };

            lista.Sort();
            WypiszKolekcje(lista, "Posortowana lista");
            Console.WriteLine($"Min: {lista.Min()}, Max: {lista.Max()}");

            if (lista.Count > 1)
            {
                lista.RemoveAt(1);
                WypiszKolekcje(lista, "Lista po usunieciu 2. elementu");
            }

            ComplexNumber? minElement = lista.Min();

            if (minElement != null)
            {
                lista.Remove(minElement);
            }
            WypiszKolekcje(lista, "Lista po usunieciu najmniejszego elementu");

            lista.Clear();
            Console.WriteLine($"Liczba elementow po Clear(): {lista.Count}");

            Console.WriteLine("\n=== ZADANIE 4: HASHSET ===");

            var z1 = new ComplexNumber(6, 7);
            var z2 = new ComplexNumber(1, 2);
            var z3 = new ComplexNumber(6, 7);
            var z4 = new ComplexNumber(1, -2);
            var z5 = new ComplexNumber(-5, 9);

            HashSet<ComplexNumber> zbior = new HashSet<ComplexNumber>();
            zbior.Add(z1);
            zbior.Add(z2);
            zbior.Add(z3);
            zbior.Add(z4);
            zbior.Add(z5);

            WypiszKolekcje(zbior, "Zawartosc zbioru");

            Console.WriteLine($"Min: {zbior.Min()}");
            Console.WriteLine($"Max: {zbior.Max()}");

            var zbiorFiltrowany = zbior.Where(z => z.Im < 0);
            WypiszKolekcje(zbiorFiltrowany, "Filtrowanie zbioru");

            var posortowanyZbior = zbior.OrderBy(z => z).ToList();
            WypiszKolekcje(posortowanyZbior, "Posortowany zbior");

            Console.WriteLine("\n=== ZADANIE 5: DICTIONARY ===");

            Dictionary<string, ComplexNumber> slownik = new Dictionary<string, ComplexNumber>();
            slownik.Add("z1", new ComplexNumber(6, 7));
            slownik.Add("z2", new ComplexNumber(1, 2));
            slownik.Add("z3", new ComplexNumber(6, 7));
            slownik.Add("z4", new ComplexNumber(1, -2));
            slownik.Add("z5", new ComplexNumber(-5, 9));

            foreach (var kvp in slownik)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }

            Console.WriteLine("\nKlucze:");
            foreach (var k in slownik.Keys) Console.Write(k + " ");

            Console.WriteLine("\n\nWartosci:");
            foreach (var v in slownik.Values) Console.WriteLine(v);

            Console.WriteLine($"\nCzy istnieje klucz 'z6'?: {slownik.ContainsKey("z6")}");

            Console.WriteLine($"Min ze slownika: {slownik.Values.Min()}");
            var filtrowanySlownik = slownik.Values.Where(z => z.Im < 0);
            WypiszKolekcje(filtrowanySlownik, "Filtrowanie wartosci slownika");

            slownik.Remove("z3");
            Console.WriteLine($"Usunieto 'z3'. Rozmiar: {slownik.Count}");

            if (slownik.Count >= 2)
            {
                string kluczDoUsuniecia = slownik.Keys.ElementAt(1);
                slownik.Remove(kluczDoUsuniecia);
                Console.WriteLine($"Usunieto drugi element. Rozmiar: {slownik.Count}");
            }

            slownik.Clear();
            Console.WriteLine($"Wyczyszczono slownik. Rozmiar: {slownik.Count}");

            Console.ReadKey();
        }
    }
}