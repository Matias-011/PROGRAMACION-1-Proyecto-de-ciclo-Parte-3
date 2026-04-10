using System;
using System.Collections.Generic;
using System.Linq;

class Libro
{
    public string Titulo;
    public int Cantidad;
    public int Prestamos;

    public Libro(string titulo, int cantidad)
    {
        Titulo = titulo;
        Cantidad = cantidad;
        Prestamos = 0;
    }
}

class Usuario
{
    public string Nombre;

    public Usuario(string nombre)
    {
        Nombre = nombre;
    }
}

class Prestamo
{
    public string Usuario;
    public string Libro;
    public DateTime FechaPrestamo;
    public bool Devuelto;
}

class Program
{
    static List<Libro> libros = new List<Libro>();
    static List<Usuario> usuarios = new List<Usuario>();
    static List<Prestamo> prestamos = new List<Prestamo>();

    static void Main()
    {
        int opcion;

        do
        {
            Console.Clear();
            Console.WriteLine("==== GESTOR DE BIBLIOTECA ====");
            Console.WriteLine("1. Registrar libro");
            Console.WriteLine("2. Registrar usuario");
            Console.WriteLine("3. Realizar préstamo");
            Console.WriteLine("4. Devolver libro");
            Console.WriteLine("5. Reporte libros más prestados");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione opción: ");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1: RegistrarLibro(); break;
                case 2: RegistrarUsuario(); break;
                case 3: RealizarPrestamo(); break;
                case 4: DevolverLibro(); break;
                case 5: Reporte(); break;
                case 6: Console.WriteLine("Saliendo..."); break;
                default: Console.WriteLine("Opción inválida"); break;
            }

            if (opcion != 6)
            {
                Console.WriteLine("\nPresione una tecla...");
                Console.ReadKey();
            }

        } while (opcion != 6);
    }

    static void RegistrarLibro()
    {
        Console.Write("Título del libro: ");
        string titulo = Console.ReadLine();
        Console.Write("Cantidad: ");
        int cantidad = int.Parse(Console.ReadLine());

        libros.Add(new Libro(titulo, cantidad));
        Console.WriteLine("Libro registrado correctamente.");
    }

    static void RegistrarUsuario()
    {
        Console.Write("Nombre del usuario: ");
        string nombre = Console.ReadLine();

        usuarios.Add(new Usuario(nombre));
        Console.WriteLine("Usuario registrado correctamente.");
    }

    static void RealizarPrestamo()
    {
        Console.Write("Nombre del usuario: ");
        string usuario = Console.ReadLine();

        Console.Write("Libro a prestar: ");
        string libro = Console.ReadLine();

        var l = libros.FirstOrDefault(x => x.Titulo == libro);

        if (l != null && l.Cantidad > 0)
        {
            prestamos.Add(new Prestamo()
            {
                Usuario = usuario,
                Libro = libro,
                FechaPrestamo = DateTime.Now,
                Devuelto = false
            });

            l.Cantidad--;
            l.Prestamos++;

            Console.WriteLine("Préstamo realizado correctamente.");
        }
        else
        {
            Console.WriteLine("Libro no disponible.");
        }
    }

    static void DevolverLibro()
    {
        Console.Write("Nombre del usuario: ");
        string usuario = Console.ReadLine();

        Console.Write("Libro a devolver: ");
        string libro = Console.ReadLine();

        var p = prestamos.FirstOrDefault(x => x.Usuario == usuario && x.Libro == libro && !x.Devuelto);

        if (p != null)
        {
            p.Devuelto = true;
            var l = libros.First(x => x.Titulo == libro);
            l.Cantidad++;

            int dias = (DateTime.Now - p.FechaPrestamo).Days;
            if (dias > 7)
            {
                double multa = (dias - 7) * 0.50;
                Console.WriteLine($"Libro devuelto con multa: ${multa:F2}");
            }
            else
            {
                Console.WriteLine("Libro devuelto sin multa.");
            }
        }
        else
        {
            Console.WriteLine("No se encontró el préstamo.");
        }
    }

    static void Reporte()
    {
        Console.WriteLine("\n=== LIBROS MÁS PRESTADOS ===");
        var orden = libros.OrderByDescending(x => x.Prestamos);

        foreach (var l in orden)
        {
            Console.WriteLine($"{l.Titulo} - Préstamos: {l.Prestamos}");
        }
    }
}
