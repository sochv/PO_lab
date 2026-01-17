using System;
using SklepOdziezowy;

class Program
{
    static void Main(string[] args)
    {
        SklepManager manager = new SklepManager();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== SYSTEM ZARZĄDZANIA SKLEPEM ODZIEŻOWYM ===");
            Console.WriteLine("1. Wyświetl produkty");
            Console.WriteLine("2. Dodaj produkt");
            Console.WriteLine("3. Edytuj produkt");
            Console.WriteLine("4. Usuń produkt");
            Console.WriteLine("0. Wyjście");
            Console.Write("\nWybierz opcję: ");

            string wybor = Console.ReadLine();

            switch (wybor)
            {
                case "1":
                    WyswietlProdukty(manager);
                    break;
                case "2":
                    DodajNowyProdukt(manager);
                    break;
                case "3":
                    EdytujProduktInterfejs(manager);
                    break;
                case "4":
                    UsunProduktInterfejs(manager);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Nieznana opcja.");
                    break;
            }
        }
    }

    static void WyswietlProdukty(SklepManager manager)
    {
        Console.WriteLine("\n--- LISTA PRODUKTÓW ---");
        var produkty = manager.PobierzWszystkieProdukty();
        if (produkty.Count == 0) Console.WriteLine("Magazyn pusty.");
        else foreach (var p in produkty) Console.WriteLine(p);

        Console.WriteLine("\nNaciśnij Enter...");
        Console.ReadLine();
    }

    static void DodajNowyProdukt(SklepManager manager)
    {
        Console.WriteLine("\n--- DODAWANIE ---");
        try
        {
            Console.Write("ID: "); int id = int.Parse(Console.ReadLine());
            Console.Write("Nazwa: "); string nazwa = Console.ReadLine();
            Console.Write("Marka: "); string marka = Console.ReadLine();
            Console.Write("Rozmiar: "); string rozmiar = Console.ReadLine();
            Console.Write("Kolor: "); string kolor = Console.ReadLine();
            Console.Write("Cena: "); double cena = double.Parse(Console.ReadLine());

            manager.DodajProdukt(new ProduktOdziezowy(id, nazwa, marka, rozmiar, kolor, cena));
            Console.WriteLine("Dodano!");
        }
        catch { Console.WriteLine("Błąd danych!"); }
        Console.ReadLine();
    }

    static void EdytujProduktInterfejs(SklepManager manager)
    {
        Console.WriteLine("\n--- EDYCJA ---");
        Console.Write("Podaj ID: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Console.Write("Nowa nazwa: "); string nazwa = Console.ReadLine();
            Console.Write("Nowa cena: ");
            if (double.TryParse(Console.ReadLine(), out double cena))
            {
                manager.EdytujProdukt(id, nazwa, cena);
                Console.WriteLine("Zmieniono.");
            }
        }
        Console.ReadLine();
    }

    static void UsunProduktInterfejs(SklepManager manager)
    {
        Console.WriteLine("\n--- USUWANIE ---");
        Console.Write("Podaj ID: ");
        if (int.TryParse(Console.ReadLine(), out int id)) manager.UsunProdukt(id);
        Console.ReadLine();
    }
}