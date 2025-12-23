using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Serialization;

namespace Lab5
{
    public class Student
    {
        public string? Imie { get; set; }
        public string? Nazwisko { get; set; }
        public List<int>? Oceny { get; set; }

        public Student() { }

        public Student(string imie, string nazwisko, List<int> oceny)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Oceny = oceny;
        }
    }

    class Program
    {
        static string plikTekstowy = "dane.txt";
        static string plikJson = "studenci.json";
        static string plikXml = "studenci.xml";
        static string plikCsv = @"C:\Users\patka\source\repos\sochv\PO_lab\Lab5\iris.csv";
        static string plikCsvFiltered = "iris_filtered.csv";

        static void Main(string[] args)
        {
            ZapiszDoPlikuTekstowego();
            OdczytajZPlikuTekstowego();
            DopiszDoPlikuTekstowego();
            OdczytajZPlikuTekstowego();

            ZapiszStudentowJSON();
            OdczytajStudentowJSON();

            ZapiszStudentowXML();
            OdczytajStudentowXML();

            OdczytajCSV();
            PoliczSrednieCSV();
            FiltrujCSV();

            Console.ReadKey();
        }

        static void ZapiszDoPlikuTekstowego()
        {
            Console.WriteLine("Podaj 3 linie tekstu do zapisania:");
            using (StreamWriter sw = new StreamWriter(plikTekstowy))
            {
                for (int i = 0; i < 3; i++)
                {
                    string tekst = Console.ReadLine() ?? "";
                    sw.WriteLine(tekst);
                }
            }
        }

        static void OdczytajZPlikuTekstowego()
        {
            if (File.Exists(plikTekstowy))
            {
                string[] linie = File.ReadAllLines(plikTekstowy);
                foreach (var linia in linie)
                {
                    Console.WriteLine(linia);
                }
            }
        }

        static void DopiszDoPlikuTekstowego()
        {
            Console.WriteLine("Podaj 2 dodatkowe linie do dopisania:");
            using (StreamWriter sw = new StreamWriter(plikTekstowy, append: true))
            {
                for (int i = 0; i < 2; i++)
                {
                    string tekst = Console.ReadLine() ?? "";
                    sw.WriteLine(tekst);
                }
            }
        }

        static void ZapiszStudentowJSON()
        {
            List<Student> studenci = PrzygotujListeStudentow();
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(studenci, options);
            File.WriteAllText(plikJson, jsonString);
        }

        static void OdczytajStudentowJSON()
        {
            if (File.Exists(plikJson))
            {
                string jsonString = File.ReadAllText(plikJson);
                List<Student>? studenci = JsonSerializer.Deserialize<List<Student>>(jsonString);
                if (studenci != null)
                {
                    WypiszStudentow(studenci);
                }
            }
        }

        static void ZapiszStudentowXML()
        {
            List<Student> studenci = PrzygotujListeStudentow();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
            using (StreamWriter sw = new StreamWriter(plikXml))
            {
                serializer.Serialize(sw, studenci);
            }
        }

        static void OdczytajStudentowXML()
        {
            if (File.Exists(plikXml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
                using (StreamReader sr = new StreamReader(plikXml))
                {
                    List<Student>? studenci = serializer.Deserialize(sr) as List<Student>;
                    if (studenci != null)
                    {
                        WypiszStudentow(studenci);
                    }
                }
            }
        }

        static void OdczytajCSV()
        {
            if (!File.Exists(plikCsv))
            {
                Console.WriteLine("Brak pliku Iris.csv");
                return;
            }

            string[] linie = File.ReadAllLines(plikCsv);
            for (int i = 0; i < linie.Length; i++)
            {
                Console.WriteLine(linie[i].Replace(',', '\t'));
            }
        }

        static void PoliczSrednieCSV()
        {
            if (!File.Exists(plikCsv)) return;

            var lines = File.ReadAllLines(plikCsv);
            var dataLines = lines.Skip(1).ToArray();

            double sumSL = 0, sumSW = 0, sumPL = 0, sumPW = 0;
            int count = 0;

            foreach (var line in dataLines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split(',');

                if (parts.Length >= 4)
                {
                    if (double.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double sl) &&
                       double.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double sw) &&
                       double.TryParse(parts[2], NumberStyles.Any, CultureInfo.InvariantCulture, out double pl) &&
                       double.TryParse(parts[3], NumberStyles.Any, CultureInfo.InvariantCulture, out double pw))
                    {
                        sumSL += sl;
                        sumSW += sw;
                        sumPL += pl;
                        sumPW += pw;
                        count++;
                    }
                }
            }

            if (count > 0)
            {
                Console.WriteLine($"Srednia Sepal Length: {(sumSL / count):F2}");
                Console.WriteLine($"Srednia Sepal Width:  {(sumSW / count):F2}");
                Console.WriteLine($"Srednia Petal Length: {(sumPL / count):F2}");
                Console.WriteLine($"Srednia Petal Width:  {(sumPW / count):F2}");
            }
        }

        static void FiltrujCSV()
        {
            if (!File.Exists(plikCsv)) return;

            var lines = File.ReadAllLines(plikCsv);
            List<string> filteredLines = new List<string>();

            filteredLines.Add("sepal_length,sepal_width,class");

            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(',');

                if (parts.Length >= 5)
                {
                    if (double.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double sl))
                    {
                        if (sl < 5.0)
                        {
                            string newLine = $"{parts[0]},{parts[1]},{parts[4]}";
                            filteredLines.Add(newLine);
                        }
                    }
                }
            }

            File.WriteAllLines(plikCsvFiltered, filteredLines);
        }

        static List<Student> PrzygotujListeStudentow()
        {
            return new List<Student>
            {
                new Student("Jan", "Kowalski", new List<int> { 3, 4, 5 }),
                new Student("Anna", "Nowak", new List<int> { 5, 5, 4, 5 }),
                new Student("Piotr", "Wisniewski", new List<int> { 2, 3 })
            };
        }

        static void WypiszStudentow(List<Student> studenci)
        {
            foreach (var s in studenci)
            {
                string ocenyStr = s.Oceny != null ? string.Join(", ", s.Oceny) : "Brak ocen";
                Console.WriteLine($"- {s.Imie} {s.Nazwisko}, Oceny: [{ocenyStr}]");
            }
        }
    }
}