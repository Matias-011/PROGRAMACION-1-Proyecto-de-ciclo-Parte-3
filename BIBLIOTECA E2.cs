using System;

class Libro
{
    private string titulo;
    private int cantidad;
    private int prestamos;

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

class Usuario
{
    private string nombre;

    public string Nombre
    {
        get { return nombre; }
        set { nombre = value; }
    }

    public Usuario(string nombre)
    {
        this.nombre = nombre;
    }
}

class Prestamo
{
    private string usuario;
    private string libro;
    private DateTime fechaPrestamo;
    private bool devuelto;

    public string Usuario
    {
        get { return usuario; }
        set { usuario = value; }
    }

    public string Libro
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

    public Prestamo(string usuario, string libro, DateTime fechaPrestamo, bool devuelto)
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
                    case 8:
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Error: debe ingresar un número válido.");
            }

            if (opcion != 8)
            {
                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
            }

        } while (opcion != 8);
    }

    static void RegistrarLibro()
    {
        try
        {
            if (totalLibros >= libros.Length)
            {
                Console.WriteLine("No hay espacio para más libros.");
                return;
            }

            Console.Write("Título del libro: ");
            string titulo = Console.ReadLine();

            Console.Write("Cantidad: ");
            int cantidad = int.Parse(Console.ReadLine());

            libros[totalLibros] = new Libro(titulo, cantidad);
            totalLibros++;

            Console.WriteLine("Libro registrado correctamente.");
        }
        catch
        {
            Console.WriteLine("Error al registrar libro. Verifique los datos ingresados.");
        }
    }

    static void RegistrarUsuario()
    {
        try
        {
            if (totalUsuarios >= usuarios.Length)
            {
                Console.WriteLine("No hay espacio para más usuarios.");
                return;
            }

            Console.Write("Nombre del usuario: ");
            string nombre = Console.ReadLine();

            usuarios[totalUsuarios] = new Usuario(nombre);
            totalUsuarios++;

            Console.WriteLine("Usuario registrado correctamente.");
        }
        catch
        {
            Console.WriteLine("Error al registrar usuario.");
        }
    }

    static void RealizarPrestamo()
    {
        try
        {
            if (totalPrestamos >= prestamos.Length)
            {
                Console.WriteLine("No hay espacio para más préstamos.");
                return;
            }

            Console.Write("Nombre del usuario: ");
            string nombreUsuario = Console.ReadLine();

            Console.Write("Libro a prestar: ");
            string tituloLibro = Console.ReadLine();

            int posicionLibro = -1;
            int posicionUsuario = -1;

            for (int i = 0; i < totalUsuarios; i++)
            {
                if (usuarios[i].Nombre.ToLower() == nombreUsuario.ToLower())
                {
                    posicionUsuario = i;
                    break;
                }
            }

            for (int i = 0; i < totalLibros; i++)
            {
                if (libros[i].Titulo.ToLower() == tituloLibro.ToLower())
                {
                    posicionLibro = i;
                    break;
                }
            }

            if (posicionUsuario == -1)
            {
                Console.WriteLine("El usuario no está registrado.");
                return;
            }

            if (posicionLibro == -1)
            {
                Console.WriteLine("El libro no existe.");
                return;
            }

            if (libros[posicionLibro].Cantidad > 0)
            {
                prestamos[totalPrestamos] = new Prestamo(
                    usuarios[posicionUsuario].Nombre,
                    libros[posicionLibro].Titulo,
                    DateTime.Now,
                    false
                );

                totalPrestamos++;
                libros[posicionLibro].Cantidad = libros[posicionLibro].Cantidad - 1;
                libros[posicionLibro].Prestamos = libros[posicionLibro].Prestamos + 1;

                Console.WriteLine("Préstamo realizado correctamente.");
            }
            else
            {
                Console.WriteLine("Libro no disponible.");
            }
        }
        catch
        {
            Console.WriteLine("Error al realizar el préstamo.");
        }
    }

    static void DevolverLibro()
    {
        try
        {
            Console.Write("Nombre del usuario: ");
            string nombreUsuario = Console.ReadLine();

            Console.Write("Libro a devolver: ");
            string tituloLibro = Console.ReadLine();

            int posicionPrestamo = -1;
            int posicionLibro = -1;

            for (int i = 0; i < totalPrestamos; i++)
            {
                if (prestamos[i].Usuario.ToLower() == nombreUsuario.ToLower() &&
                    prestamos[i].Libro.ToLower() == tituloLibro.ToLower() &&
                    prestamos[i].Devuelto == false)
                {
                    posicionPrestamo = i;
                    break;
                }
            }

            for (int i = 0; i < totalLibros; i++)
            {
                if (libros[i].Titulo.ToLower() == tituloLibro.ToLower())
                {
                    posicionLibro = i;
                    break;
                }
            }

            if (posicionPrestamo != -1 && posicionLibro != -1)
            {
                prestamos[posicionPrestamo].Devuelto = true;
                libros[posicionLibro].Cantidad = libros[posicionLibro].Cantidad + 1;

                int dias = (DateTime.Now - prestamos[posicionPrestamo].FechaPrestamo).Days;

                if (dias > 7)
                {
                    double multa = (dias - 7) * 0.50;
                    Console.WriteLine("Libro devuelto con multa: $" + multa.ToString("F2"));
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
        catch
        {
            Console.WriteLine("Error al devolver el libro.");
        }
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
            Console.WriteLine("Título: " + libros[i].Titulo +
                              " | Cantidad: " + libros[i].Cantidad +
                              " | Préstamos: " + libros[i].Prestamos);
        }
    }

    static void BuscarLibro()
    {
        Console.Write("Ingrese el título del libro a buscar: ");
        string tituloBuscar = Console.ReadLine();
        bool encontrado = false;

        for (int i = 0; i < totalLibros; i++)
        {
            if (libros[i].Titulo.ToLower() == tituloBuscar.ToLower())
            {
                Console.WriteLine("\nLibro encontrado:");
                Console.WriteLine("Título: " + libros[i].Titulo);
                Console.WriteLine("Cantidad disponible: " + libros[i].Cantidad);
                Console.WriteLine("Cantidad de préstamos: " + libros[i].Prestamos);
                encontrado = true;
                break;
            }
        }

        if (!encontrado)
        {
            Console.WriteLine("Libro no encontrado.");
        }
    }

    static void ReporteLibrosMasPrestados()
    {
        if (totalLibros == 0)
        {
            Console.WriteLine("No hay libros registrados.");
            return;
        }

        for (int i = 0; i < totalLibros - 1; i++)
        {
            for (int j = i + 1; j < totalLibros; j++)
            {
                if (libros[j].Prestamos > libros[i].Prestamos)
                {
                    Libro auxiliar = libros[i];
                    libros[i] = libros[j];
                    libros[j] = auxiliar;
                }
            }
        }

        Console.WriteLine("\n=== LIBROS MÁS PRESTADOS ===");
        for (int i = 0; i < totalLibros; i++)
        {
            Console.WriteLine(libros[i].Titulo + " - Préstamos: " + libros[i].Prestamos);
        }
    }
}