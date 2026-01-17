using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;           
using System.Text.Json;     

namespace SklepOdziezowy
{
    public class SklepManager
    {
        private List<ProduktOdziezowy> listaProduktow;
        private const string NazwaPliku = "magazyn_dane.json";

        public SklepManager()
        {
            listaProduktow = WczytajZPliku();
        }

        public void DodajProdukt(ProduktOdziezowy produkt)
        {
            listaProduktow.Add(produkt);
            ZapiszDoPliku(); 
        }

        public List<ProduktOdziezowy> PobierzWszystkieProdukty()
        {
            return listaProduktow;
        }

        public ProduktOdziezowy ZnajdzProduktPoId(int id)
        {
            return listaProduktow.FirstOrDefault(p => p.Id == id);
        }

        public void EdytujProdukt(int id, string nowaNazwa, double nowaCena)
        {
            var produkt = ZnajdzProduktPoId(id);
            if (produkt != null)
            {
                produkt.Nazwa = nowaNazwa;
                produkt.Cena = nowaCena;
                ZapiszDoPliku(); 
            }
        }

        public void UsunProdukt(int id)
        {
            var produkt = ZnajdzProduktPoId(id);
            if (produkt != null)
            {
                listaProduktow.Remove(produkt);
                Console.WriteLine("Produkt usunięty.");
                ZapiszDoPliku(); 
            }
        }


        private void ZapiszDoPliku()
        {
            try
            {
                var opcje = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(listaProduktow, opcje);
                File.WriteAllText(NazwaPliku, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd zapisu: {ex.Message}");
            }
        }

        private List<ProduktOdziezowy> WczytajZPliku()
        {
            if (!File.Exists(NazwaPliku))
            {
                return new List<ProduktOdziezowy>();
            }

            try
            {
                string jsonString = File.ReadAllText(NazwaPliku);
                var lista = JsonSerializer.Deserialize<List<ProduktOdziezowy>>(jsonString);
                return lista ?? new List<ProduktOdziezowy>();
            }
            catch (Exception)
            {
                return new List<ProduktOdziezowy>();
            }
        }
    }
}