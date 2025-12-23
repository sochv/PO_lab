using System;

    class Zwierze
    {
        protected string nazwa;

        public Zwierze(string nazwa)
        {
            this.nazwa = nazwa;
        }

        public virtual void daj_glos()
        {
            Console.WriteLine("...");
        }
    }

    class Pies : Zwierze
    {
        public Pies(string nazwa) : base(nazwa) { }

        public override void daj_glos()
        {
            string imię = nazwa;
            Console.WriteLine($"{imię} robi woof woof!");
        }
    }

    class Kot : Zwierze
    {
        public Kot(string nazwa) : base(nazwa) { }

        public override void daj_glos()
        {
            string imię = nazwa;
            Console.WriteLine($"{imię} robi miau miau!");
        }
    }

    class Waz : Zwierze
    {
        public Waz(string nazwa) : base(nazwa) { }

        public override void daj_glos()
        {
            string imię = nazwa;
            Console.WriteLine($"{imię} robi ssssssss!");
        }
    }

    abstract class Pracownik
    {
        public abstract void Pracuj();
    }

    class Piekarz : Pracownik
    {
        public override void Pracuj()
        {
            Console.WriteLine("Trwa pieczenie...");
        }
    }

    class A
    {
        public A()
        {
            Console.WriteLine("To jest konstruktor A");
        }
    }

    class B : A
    {
        public B() : base()
        {
            Console.WriteLine("To jest konstruktor B");
        }
    }

    class C : B
    {
        public C() : base()
        {
            Console.WriteLine("To jest konstruktor C");
        }
    }

    class Program
    {
        static void powiedz_cos(Zwierze zwierze)
        {
            zwierze.daj_glos();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("=== ZWIERZĘTA ===");
            Zwierze z = new Zwierze("Zwierze");
            Pies p = new Pies("Pimpek");
            Kot k = new Kot("Puszek");
            Waz w = new Waz("Jęzor");

            Console.Write($"[{z.GetType().Name}]: "); powiedz_cos(z);
            Console.Write($"[{p.GetType().Name}]: "); powiedz_cos(p);
            Console.Write($"[{k.GetType().Name}]: "); powiedz_cos(k);
            Console.Write($"[{w.GetType().Name}]: "); powiedz_cos(w);

            Console.WriteLine("\n=== PRACOWNIK ===");
            Piekarz piekarz = new Piekarz();
            piekarz.Pracuj();

            Console.WriteLine("\n[Info do zad. 11]: Nie można utworzyć obiektu 'Pracownik', bo jest abstrakcyjny.");

            Console.WriteLine("\n=== KONSTRUKTORY (A -> B -> C) ===");
            Console.WriteLine("-> Tworzę A:");
            new A();

            Console.WriteLine("\n-> Tworzę B:");
            new B();

            Console.WriteLine("\n-> Tworzę C:");
            new C();

            Console.WriteLine("\nNaciśnij dowolny klawisz, aby zamknąć...");
            Console.ReadKey();
        }
    }