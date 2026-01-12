using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Lab6
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Imie { get; set; } = "";
        public string Nazwisko { get; set; } = "";
        public List<Ocena> Oceny { get; set; } = new();
    }

    public class Ocena
    {
        public int OcenaId { get; set; }
        public double Wartosc { get; set; }
        public string Przedmiot { get; set; } = "";
        public int StudentId { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            string connectionString = "Data Source=10.200.2.28;Initial Catalog=studenci_71464;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Połączono z bazą.");

                    // Tutaj możesz wywoływać metody, np.:
                    // WypiszStudentow(connection);

                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd: " + ex.Message);
            }
        }

        public static void WypiszStudentow(SqlConnection connection)
        {
            string sql = "SELECT student_id, imie, nazwisko FROM student";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("--- Lista studentów ---");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["student_id"]}: {reader["imie"]} {reader["nazwisko"]}");
                    }
                }
            }
        }

        public static void WypiszStudentaPoId(SqlConnection connection, int id)
        {
            string sql = "SELECT imie, nazwisko FROM student WHERE student_id = @id";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine($"Student {id}: {reader["imie"]} {reader["nazwisko"]}");
                    }
                    else
                    {
                        Console.WriteLine($"Nie znaleziono studenta o ID: {id}");
                    }
                }
            }
        }

        public static List<Student> PobierzStudentowZOcenami(SqlConnection connection)
        {
            List<Student> studenci = new List<Student>();
            string sqlStudent = "SELECT student_id, imie, nazwisko FROM student";

            using (SqlCommand cmd = new SqlCommand(sqlStudent, connection))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    studenci.Add(new Student
                    {
                        StudentId = (int)reader["student_id"],
                        Imie = reader["imie"].ToString(),
                        Nazwisko = reader["nazwisko"].ToString(),
                        Oceny = new List<Ocena>()
                    });
                }
            }

            foreach (var s in studenci)
            {
                string sqlOceny = "SELECT ocena_id, wartosc, przedmiot FROM ocena WHERE student_id = @sid";
                using (SqlCommand cmd = new SqlCommand(sqlOceny, connection))
                {
                    cmd.Parameters.AddWithValue("@sid", s.StudentId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            s.Oceny.Add(new Ocena
                            {
                                OcenaId = (int)reader["ocena_id"],
                                Wartosc = Convert.ToDouble(reader["wartosc"]),
                                Przedmiot = reader["przedmiot"].ToString(),
                                StudentId = s.StudentId
                            });
                        }
                    }
                }
            }
            return studenci;
        }

        public static void WypiszListeStudentow(List<Student> lista)
        {
            foreach (var s in lista)
            {
                Console.WriteLine($"{s.Imie} {s.Nazwisko} (ID: {s.StudentId})");
                foreach (var o in s.Oceny)
                {
                    Console.WriteLine($"  - {o.Przedmiot}: {o.Wartosc}");
                }
            }
        }

        public static void DodajStudenta(SqlConnection connection, Student nowyStudent)
        {
            string sql = "INSERT INTO student (imie, nazwisko) VALUES (@imie, @nazwisko)";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@imie", nowyStudent.Imie);
                command.Parameters.AddWithValue("@nazwisko", nowyStudent.Nazwisko);

                int rows = command.ExecuteNonQuery();
                Console.WriteLine($"Dodano studenta. Zmodyfikowano wierszy: {rows}");
            }
        }

        public static void DodajOcene(SqlConnection connection, Ocena nowaOcena)
        {
            bool zakresOk = nowaOcena.Wartosc >= 2.0 && nowaOcena.Wartosc <= 5.0;
            bool polowkiOk = (nowaOcena.Wartosc % 0.5) == 0;
            bool nieDwaPol = nowaOcena.Wartosc != 2.5;

            if (!zakresOk || !polowkiOk || !nieDwaPol)
            {
                Console.WriteLine($"Błąd: Ocena {nowaOcena.Wartosc} jest nieprawidłowa!");
                return;
            }

            string sql = "INSERT INTO ocena (wartosc, przedmiot, student_id) VALUES (@val, @sub, @sid)";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@val", nowaOcena.Wartosc);
                command.Parameters.AddWithValue("@sub", nowaOcena.Przedmiot);
                command.Parameters.AddWithValue("@sid", nowaOcena.StudentId);

                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Dodano ocenę.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Błąd bazy danych: " + ex.Message);
                }
            }
        }

        public static void UsunGeografie(SqlConnection connection)
        {
            string sql = "DELETE FROM ocena WHERE przedmiot = 'Geografia'";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                int usuniete = command.ExecuteNonQuery();
                Console.WriteLine($"Usunięto ocen z geografii: {usuniete}");
            }
        }

        public static void AktualizujOcene(SqlConnection connection, int ocenaId, double nowaWartosc)
        {
            bool zakresOk = nowaWartosc >= 2.0 && nowaWartosc <= 5.0;
            bool polowkiOk = (nowaWartosc % 0.5) == 0;
            bool nieDwaPol = nowaWartosc != 2.5;

            if (!zakresOk || !polowkiOk || !nieDwaPol)
            {
                Console.WriteLine($"Nie można zaktualizować. Wartość {nowaWartosc} jest błędna.");
                return;
            }

            string sql = "UPDATE ocena SET wartosc = @val WHERE ocena_id = @id";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@val", nowaWartosc);
                command.Parameters.AddWithValue("@id", ocenaId);

                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                    Console.WriteLine("Zaktualizowano ocenę.");
                else
                    Console.WriteLine("Nie znaleziono oceny o takim ID.");
            }
        }
    }
}