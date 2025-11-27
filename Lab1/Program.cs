using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("To jest cwiczenie 1");

        var zwierzeta = new List<Zwierze>();

        for (int i = 1; i <= 3; i++)
        {
            Console.WriteLine($"Podaj nazwę zwierzęcia #{i}:");
            string nazwa;
            while (string.IsNullOrWhiteSpace(nazwa = Console.ReadLine()?.Trim()))
            {
                Console.WriteLine("Nazwa nie może być pusta. Podaj nazwę:");
            }

            Console.WriteLine($"Podaj gatunek zwierzęcia #{i}:");
            string gatunek;
            while (string.IsNullOrWhiteSpace(gatunek = Console.ReadLine()?.Trim()))
            {
                Console.WriteLine("Gatunek nie może być pusty. Podaj gatunek:");
            }

           
            var gatunekNorm = gatunek.ToLowerInvariant();
            if (gatunekNorm == "królik")
            {
                Console.WriteLine("Królik nie jest obsługiwanym gatunkiem. Przerywam funkcję");
                return;
            }

            Console.WriteLine($"Podaj liczbę nóg zwierzęcia #{i}:");
            int liczbaNog;
            while (!int.TryParse(Console.ReadLine(), out liczbaNog) || liczbaNog < 0)
            {
                Console.WriteLine("Niepoprawna liczba. Podaj nieujemną liczbę nóg:");
            }

            zwierzeta.Add(new Zwierze(nazwa, gatunek, liczbaNog));
            Console.WriteLine();
        }

        var klon = new Zwierze(zwierzeta[0]);
        Console.WriteLine("Podaj nazwę dla sklonowanego zwierzęcia:");
        string nowaNazwa;
        while (string.IsNullOrWhiteSpace(nowaNazwa = Console.ReadLine()?.Trim()))
        {
            Console.WriteLine("Nazwa nie może być pusta. Podaj nazwę:");
        }

        klon.ZmienNazwe(nowaNazwa);
        zwierzeta.Add(klon);

        Console.WriteLine();
        foreach (var z in zwierzeta)
        {
            Console.WriteLine(z.Opis());
            z.daj_glos();
            Console.WriteLine();
        }

        Console.WriteLine($"Liczba zwierząt: {Zwierze.GetLiczbaZwierzat()}");

        Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć...");
        Console.ReadKey();
    }
}

class Zwierze
{
    private string nazwa { get; set; }
    private string gatunek { get; set; }
    private int liczba_nóg { get; set; }
    public static int liczba_zwierząt { get; private set; } = 0;

    public Zwierze()
    {
        this.nazwa = "Rex";
        this.gatunek = "Pies";
        this.liczba_nóg = 4;
        liczba_zwierząt++;
    }

    public Zwierze(string nazwa, string gatunek, int liczba_nóg)
    {
        this.nazwa = nazwa;
        this.gatunek = gatunek;
        this.liczba_nóg = liczba_nóg;
        liczba_zwierząt++;
    }

    public Zwierze(Zwierze other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));
        this.nazwa = other.nazwa;
        this.gatunek = other.gatunek;
        this.liczba_nóg = other.liczba_nóg;
        liczba_zwierząt++;
    }

    public string Opis()
    {
        return $"Nazwa: {nazwa}, Gatunek: {gatunek}, Liczba nóg: {liczba_nóg}";
    }

    public void daj_glos()
    {
        switch (gatunek.ToLowerInvariant())
        {
            case "pies":
                Console.WriteLine("Hau");
                break;
            case "kot":
                Console.WriteLine("Miau");
                break;
            case "krowa":
                Console.WriteLine("Muuu");
                break;
            default:
                Console.WriteLine("Nieznany dźwięk");
                break;
        }
    }

    public void ZmienNazwe(string nowaNazwa)
    {
        if (string.IsNullOrWhiteSpace(nowaNazwa)) throw new ArgumentException(nameof(nowaNazwa));
        this.nazwa = nowaNazwa;
    }

    public static int GetLiczbaZwierzat() => liczba_zwierząt;
}