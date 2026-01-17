using System;

namespace SklepOdziezowy
{
    public class ProduktOdziezowy
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Marka { get; set; }
        public string Rozmiar { get; set; }
        public string Kolor { get; set; }
        public double Cena { get; set; }

        public ProduktOdziezowy() { }

        public ProduktOdziezowy(int id, string nazwa, string marka, string rozmiar, string kolor, double cena)
        {
            Id = id;
            Nazwa = nazwa;
            Marka = marka;
            Rozmiar = rozmiar;
            Kolor = kolor;
            Cena = cena;
        }

        public override string ToString()
        {
            return $"ID: {Id} | {Nazwa} ({Marka}) | Rozmiar: {Rozmiar}, Kolor: {Kolor} | Cena: {Cena:F2} PLN";
        }
    }
}