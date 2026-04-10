using System;


class Persona
{
    protected string nombre;

    public string Nombre
    {
        get { return nombre; }
        set { nombre = value; }
    }
}

class Usuario : Persona
{
    public Usuario(string nombre)
    {
        this.nombre = nombre;
    }
}


class Material
{
    protected string titulo;
    protected int cantidad;

    public string Titulo
    {
        get { return titulo; }
        set { titulo = value; }
    }

    public int Cantidad
    {
        get { return cantidad; }
        set { cantidad = value; }
    }
}

class Libro : Material
{
    private int prestamos;

    public int Prestamos
    {
        get { return prestamos; }
        set { prestamos = value; }
    }

    public Libro(string titulo, int cantidad)
    {
        this.titulo = titulo;
        this.cantidad = cantidad;
        this.prestamos = 0;
    }
}


class Prestamo
{
    private Usuario usuario;
    private Libro libro;
    private DateTime fechaPrestamo;
    private bool devuelto;

    public Usuario Usuario
    {
        get { return usuario; }
        set { usuario = value; }
    }

    public Libro Libro
    {
        get { return libro; }
        set { libro = value; }
    }

    public DateTime FechaPrestamo
    {
        get { return fechaPrestamo; }
        set { fechaPrestamo = value; }
    }

    public bool Devuelto
    {
        get { return devuelto; }
        set { devuelto = value; }
    }

    public Prestamo(Usuario usuario, Libro libro, DateTime fechaPrestamo, bool devuelto)
    {
        this.usuario = usuario;
        this.libro = libro;
        this.fechaPrestamo = fechaPrestamo;
        this.devuelto = devuelto;
    }
}

class Program
{
    static Libro[] libros = new Libro[100];
    static Usuario[] usuarios = new Usuario[100];
    static Prestamo[] prestamos = new Prestamo[100];

    static int totalLibros = 0;
    static int totalUsuarios = 0;
    static int totalPrestamos = 0;

    static void Main()
    {
        int opcion = 0;

        do
        {
            Console.Clear();
            Console.WriteLine("==== GESTOR DE BIBLIOTECA ====");
            Console.WriteLine("1. Registrar libro");
            Console.WriteLine("2. Registrar usuario");
            Console.WriteLine("3. Realizar préstamo");
            Console.WriteLine("4. Devolver libro");
            Console.WriteLine("5. Listar libros");
            Console.WriteLine("6. Buscar libro");
            Console.WriteLine("7. Reporte libros más prestados");
            Console.WriteLine("8. Salir");
            Console.Write("Seleccione una opción: ");

            try
            {
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        RegistrarLibro();
                        break;
                    case 2:
                        RegistrarUsuario();
                        break;
                    case 3:
                        RealizarPrestamo();
                        break;
                    case 4:
                        DevolverLibro();
                        break;
                    case 5:
                        ListarLibros();
                        break;
                    case 6:
                        BuscarLibro();
                        break;
                    case 7:
                        ReporteLibrosMasPrestados();
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Error: ingrese un número válido.");
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();

        } while (opcion != 8);
    }

    static void RegistrarLibro()
    {
        Console.Write("Título: ");
        string titulo = Console.ReadLine();

        Console.Write("Cantidad: ");
        int cantidad = int.Parse(Console.ReadLine());

        libros[totalLibros++] = new Libro(titulo, cantidad);

        Console.WriteLine("Libro registrado con éxito.");
    }

    static void RegistrarUsuario()
    {
        Console.Write("Nombre: ");
        usuarios[totalUsuarios++] = new Usuario(Console.ReadLine());

        Console.WriteLine("Usuario registrado con éxito.");
    }

    static Usuario BuscarUsuario(string nombre)
    {
        for (int i = 0; i < totalUsuarios; i++)
        {
            if (usuarios[i].Nombre.ToLower() == nombre.ToLower())
                return usuarios[i];
        }
        return null;
    }

    static Libro BuscarLibroObj(string titulo)
    {
        for (int i = 0; i < totalLibros; i++)
        {
            if (libros[i].Titulo.ToLower() == titulo.ToLower())
                return libros[i];
        }
        return null;
    }

    static void RealizarPrestamo()
    {
        Console.Write("Usuario: ");
        Usuario u = BuscarUsuario(Console.ReadLine());

        Console.Write("Libro: ");
        Libro l = BuscarLibroObj(Console.ReadLine());

        if (u == null || l == null)
        {
            Console.WriteLine("Usuario o libro no existe.");
            return;
        }

        if (l.Cantidad > 0)
        {
            prestamos[totalPrestamos++] = new Prestamo(u, l, DateTime.Now, false);

            l.Cantidad--;
            l.Prestamos++;

            Console.WriteLine("Préstamo realizado con éxito.");
        }
        else
        {
            Console.WriteLine("Libro no disponible.");
        }
    }

    static void DevolverLibro()
    {
        Console.Write("Usuario: ");
        string nombre = Console.ReadLine();

        Console.Write("Libro: ");
        string titulo = Console.ReadLine();

        for (int i = 0; i < totalPrestamos; i++)
        {
            if (prestamos[i].Usuario.Nombre.ToLower() == nombre.ToLower() &&
                prestamos[i].Libro.Titulo.ToLower() == titulo.ToLower() &&
                !prestamos[i].Devuelto)
            {
                prestamos[i].Devuelto = true;
                prestamos[i].Libro.Cantidad++;

                int dias = (DateTime.Now - prestamos[i].FechaPrestamo).Days;

                if (dias > 7)
                {
                    double multa = (dias - 7) * 0.50;
                    Console.WriteLine("Libro devuelto con multa: $" + multa.ToString("F2"));
                }
                else
                {
                    Console.WriteLine("Libro devuelto sin multa.");
                }

                return;
            }
        }

        Console.WriteLine("No se encontró el préstamo.");
    }

    static void ListarLibros()
    {
        if (totalLibros == 0)
        {
            Console.WriteLine("No hay libros registrados.");
            return;
        }

        Console.WriteLine("\n=== LISTA DE LIBROS ===");
        for (int i = 0; i < totalLibros; i++)
        {
            Console.WriteLine(libros[i].Titulo + " | Cantidad: " + libros[i].Cantidad + " | Préstamos: " + libros[i].Prestamos);
        }
    }

    static void BuscarLibro()
    {
        Console.Write("Titulo: ");
        string t = Console.ReadLine();

        Libro l = BuscarLibroObj(t);

        if (l != null)
        {
            Console.WriteLine("Libro encontrado:");
            Console.WriteLine("Título: " + l.Titulo);
            Console.WriteLine("Cantidad: " + l.Cantidad);
            Console.WriteLine("Préstamos: " + l.Prestamos);
        }
        else
        {
            Console.WriteLine("Libro no existe.");
        }
    }

    static void ReporteLibrosMasPrestados()
    {
        for (int i = 0; i < totalLibros - 1; i++)
        {
            for (int j = i + 1; j < totalLibros; j++)
            {
                if (libros[j].Prestamos > libros[i].Prestamos)
                {
                    Libro aux = libros[i];
                    libros[i] = libros[j];
                    libros[j] = aux;
                }
            }
        }

        Console.WriteLine("\n=== LIBROS MÁS PRESTADOS ===");
        for (int i = 0; i < totalLibros; i++)
        {
            Console.WriteLine(libros[i].Titulo + " - " + libros[i].Prestamos);
        }
    }
}