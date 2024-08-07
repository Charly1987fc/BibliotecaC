using BibliotecaV.Conexion;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaV
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ClsBibliotecaContext())
            {
                object value = context.Database.EnsureCreated(); // Crea y guarda la base de datos

                // Menú principal
                while (true)
                {
                    Console.WriteLine("Sistema de Gestión de Biblioteca");
                    Console.WriteLine("1. Agregar Libro");
                    Console.WriteLine("2. Modificar Libro");
                    Console.WriteLine("3. Eliminar Libro");
                    Console.WriteLine("4. Buscar Libro");
                    Console.WriteLine("5. Prestar Libro");
                    Console.WriteLine("6. Devolver Libro");
                    Console.WriteLine("7. Mostrar Libros Prestados");
                    Console.WriteLine("8. Mostrar Lista de Libros");
                    Console.WriteLine("9. Mostrar Libro Más Prestado");
                    Console.WriteLine("10. Gestionar Usuarios");
                    Console.WriteLine("11. Salir");
                    Console.Write("Seleccione una opción: ");

                    var opcion = Console.ReadLine();

                    switch (opcion)
                    {
                        case "1":
                            AgregarLibro(context);
                            break;
                        case "2":
                            ModificarLibro(context);
                            break;
                        case "3":
                            EliminarLibro(context);
                            break;
                        case "4":
                            BuscarLibro(context);
                            break;
                        case "5":
                            PrestarLibro(context);
                            break;
                        case "6":
                            DevolverLibro(context);
                            break;
                        case "7":
                            Console.Write("Ingrese el número de socio del usuario: ");
                            var numeroSocio = Console.ReadLine();
                            MostrarLibrosPrestados(context, numeroSocio);
                            break;
                        case "8":
                            MostrarListaLibros(context);
                            break;
                        case "9":
                            LibroMasPrestado(context);
                            break;
                        case "10":
                            GestionarUsuarios(context);
                            break;
                        case "11":
                            return;
                        default:
                            Console.WriteLine("Opción no válida. Intente de nuevo.");
                            break;
                    }
                }
            }
        }
        //Metodo
        static void AgregarLibro(ClsBibliotecaContext context)
        {
            var libro = new ClsLibro();
            Console.Write("Título: ");
            libro.Titulo = Console.ReadLine();
            Console.Write("Autor: ");
            libro.Autor = Console.ReadLine();
            Console.Write("ISBN: ");
            libro.ISBN = Console.ReadLine();
            Console.Write("Año de Publicación: ");
            libro.AnioPublicacion = int.Parse(Console.ReadLine());
            Console.Write("Número de Páginas: ");
            libro.NumeroPaginas = int.Parse(Console.ReadLine());

            context.Libros.Add(libro);
            context.SaveChanges();
            Console.WriteLine("Libro agregado exitosamente.");
        }

        static void ModificarLibro(ClsBibliotecaContext context)
        {
            Console.Write("Ingrese el Titulo del libro a modificar: ");
            var Titulo = Console.ReadLine();
            var libro = context.Libros.FirstOrDefault(l => l.Titulo == Titulo);

            if (libro != null)
            {
                Console.Write("Nuevo Título: ");
                libro.Titulo = Console.ReadLine();
                Console.Write("Nuevo Autor: ");
                libro.Autor = Console.ReadLine();
                Console.Write("Nuevo Año de Publicación: ");
                libro.AnioPublicacion = int.Parse(Console.ReadLine());
                Console.Write("Nuevo Número de Páginas: ");
                libro.NumeroPaginas = int.Parse(Console.ReadLine());

                context.SaveChanges();
                Console.WriteLine("Libro modificado exitosamente.");
            }
            else
            {
                Console.WriteLine("Libro no encontrado.");
            }
        }

        static void EliminarLibro(ClsBibliotecaContext context)
        {
            Console.Write("Ingrese el Titulo del libro a eliminar: ");
            var titulo = Console.ReadLine();
            var libro = context.Libros.FirstOrDefault(l => l.Titulo == titulo);

            if (libro != null)
            {
                context.Libros.Remove(libro);
                context.SaveChanges();
                Console.WriteLine("Libro eliminado exitosamente.");
            }
            else
            {
                Console.WriteLine("Libro no encontrado.");
            }
        }

        static void BuscarLibro(ClsBibliotecaContext context)
        {
            Console.Write("Ingrese el Titulo del libro a buscar: ");
            var titulo = Console.ReadLine();
            var libro = context.Libros.FirstOrDefault(l => l.Titulo == titulo);

            if (libro != null)
            {
                libro.MostrarInformacion();
            }
            else
            {
                Console.WriteLine("Libro no encontrado.");
            }
        }

        static void PrestarLibro(ClsBibliotecaContext context)
        {
            Console.Write("Ingrese el Titulo del libro a prestar: ");
            var titulo = Console.ReadLine();
            var libro = context.Libros.FirstOrDefault(l => l.Titulo == titulo);

            if (libro != null)
            {
                Console.Write("Ingrese el número de socio del usuario: ");
                var numeroSocio = Console.ReadLine();
                var usuario = context.Usuarios.FirstOrDefault(u => u.NumeroSocio == numeroSocio);

                if (usuario != null)
                {
                    var prestamo = new ClsPrestamo
                    {
                        Libro = libro,
                        Usuario = usuario,
                        FechaPrestamo = DateTime.Now,//Guarda el dato de la fecha exacta
                        FechaDevolucion = DateTime.Now.AddDays(14) 
                    };

                    context.Prestamos.Add(prestamo);
                    context.SaveChanges();
                    libro.Prestar();
                }
                else
                {
                    Console.WriteLine("Usuario no encontrado.");
                }
            }
            else
            {
                Console.WriteLine("Libro no encontrado.");
            }
        }

        static void DevolverLibro(ClsBibliotecaContext context)
        {
            Console.Write("Ingrese el Titulo del libro a devolver: ");
            var titulo = Console.ReadLine();
            var libro = context.Libros.FirstOrDefault(l => l.Titulo == titulo);

            if (libro != null)
            {
                // Aquí se puede buscar el préstamo en la base de datos y eliminarlo
                libro.Devolver();
                Console.WriteLine("Libro devuelto exitosamente.");
            }
            else
            {
                Console.WriteLine("Libro no encontrado.");
            }
        }
        static void MostrarListaLibros(ClsBibliotecaContext context)
        {
            var libros = context.Libros.ToList(); // Obtiene todos los libros

            if (libros.Count == 0)
            {
                Console.WriteLine("No hay libros en la biblioteca.");
                return;
            }

            Console.WriteLine("Lista de Libros en la Biblioteca:");
            foreach (var libro in libros)
            {
                Console.WriteLine($"Título: {libro.Titulo}, Autor: {libro.Autor}, ISBN: {libro.ISBN}, Año: {libro.AnioPublicacion}");
            }
        }

        static void MostrarLibrosPrestados(ClsBibliotecaContext context, string numeroSocio)
        {
            var prestamos = context.Prestamos
                .Include(p => p.Libro) 
                .Where(p => p.Usuario.NumeroSocio == numeroSocio)
                .ToList();

            if (prestamos.Count == 0)
            {
                Console.WriteLine("No hay libros prestados para este usuario.");
                return;
            }

            Console.WriteLine("Libros Prestados:");
            foreach (var prestamo in prestamos)
            {
                var diasPrestados = (DateTime.Now - prestamo.FechaPrestamo).Days;
                Console.WriteLine($"Título: {prestamo.Libro.Titulo}, Fecha de Préstamo: {prestamo.FechaPrestamo.ToShortDateString()}, Días Prestados: {diasPrestados}");
            }
        }
        static void LibroMasPrestado(ClsBibliotecaContext context)
        {//Suma del libro mas prestado
            var prestamosPorLibro = context.Prestamos
                .GroupBy(p => p.LibroId)
                .Select(g => new { LibroId = g.Key, CantidadPrestamos = g.Count() })
                .OrderByDescending(p => p.CantidadPrestamos)
                .FirstOrDefault();

            if (prestamosPorLibro == null)
            {
                Console.WriteLine("No hay información sobre préstamos.");
                return;
            }

            var libroMasPrestado = context.Libros.FirstOrDefault(l => l.Id == prestamosPorLibro.LibroId);

            Console.WriteLine($"El libro más prestado es: {libroMasPrestado.Titulo}, con {prestamosPorLibro.CantidadPrestamos} préstamos.");
        }


        static void GestionarUsuarios(ClsBibliotecaContext context)
        {//Menu para hacer la gestion de usuarios
            while (true)
            {
                Console.WriteLine("Gestión de Usuarios");
                Console.WriteLine("1. Agregar Usuario");
                Console.WriteLine("2. Modificar Usuario");
                Console.WriteLine("3. Eliminar Usuario");
                Console.WriteLine("4. Ver Datos de Usuario");
                Console.WriteLine("5. Mostrar Lista de Socios Inscritos");
                Console.WriteLine("6. Volver al menú principal");
                Console.Write("Seleccione una opción: ");

                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        AgregarUsuario(context); //Agrega usuarios
                        break;
                    case "2":
                        ModificarUsuario(context); //Modifica usuarios
                        break;
                    case "3":
                        EliminarUsuario(context);//Elimina usuarios
                        break;
                        case "4":
                        VerDatosUsuario(context);//Muestra datos de usuario por numero de usuario
                        break;
                    case "5":
                        MostrarListaSocios(context); // Llama al método para mostrar la lista de  todos los usuarios
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
            }
        }

        static void AgregarUsuario(ClsBibliotecaContext context)
        {
            var usuario = new ClsUsuario();
            Console.Write("Nombre: ");
            usuario.Nombre = Console.ReadLine();
            Console.Write("Apellido: ");
            usuario.Apellido = Console.ReadLine();
            Console.Write("Documento de identidad: ");
            usuario.DocumentoIdentidad = int.Parse(Console.ReadLine());
            Console.Write("Telefono: ");
            usuario.Telefono = int.Parse(Console.ReadLine());
            Console.Write("Número de Socio: ");
            usuario.NumeroSocio = Console.ReadLine();

            context.Usuarios.Add(usuario);
            context.SaveChanges();
            Console.WriteLine("Usuario agregado exitosamente."
                );
        }

        static void ModificarUsuario(ClsBibliotecaContext context)
        {
            Console.Write("Ingrese el número de socio del usuario a modificar: ");
            var numeroSocio = Console.ReadLine();
            var usuario = context.Usuarios.FirstOrDefault(u => u.NumeroSocio == numeroSocio);

            if (usuario != null)
            {
                Console.Write("Nuevo Nombre: ");
                usuario.Nombre = Console.ReadLine();
                Console.Write("Nuevo Apellido: ");
                usuario.Apellido = Console.ReadLine();

                context.SaveChanges();
                Console.WriteLine("Usuario modificado exitosamente.");
            }
            else
            {
                Console.WriteLine("Usuario no encontrado.");
            }
        }

        static void EliminarUsuario(ClsBibliotecaContext context)
        {
            Console.Write("Ingrese el número de socio del usuario a eliminar: ");
            var numeroSocio = Console.ReadLine();
            var usuario = context.Usuarios.FirstOrDefault(u => u.NumeroSocio == numeroSocio);

            if (usuario != null)
            {
                context.Usuarios.Remove(usuario);
                context.SaveChanges();
                Console.WriteLine("Usuario eliminado exitosamente.");
            }
            else
            {
                Console.WriteLine("Usuario no encontrado.");
            }
        }
        static void VerDatosUsuario(ClsBibliotecaContext context)
        {
            Console.Write("Ingrese el número de socio del usuario: ");
            var numeroSocio = Console.ReadLine();
            var usuario = context.Usuarios.FirstOrDefault(u => u.NumeroSocio == numeroSocio);

            if (usuario != null)
            {
                Console.WriteLine("Datos del Usuario:");
                usuario.MostrarInformacion();//Muestra informacion por polimorfismo
            }
            else
            {
                Console.WriteLine("Usuario no encontrado.");
            }
        }
        static void MostrarListaSocios(ClsBibliotecaContext context)
        {
            var usuarios = context.Usuarios.ToList(); // Obtiene todos los usuarios

            if (usuarios.Count == 0)
            {
                Console.WriteLine("No hay socios inscritos en la biblioteca.");
                return;
            }

            Console.WriteLine("Lista de Socios Inscritos:");
            foreach (var usuario in usuarios)
            {
                Console.WriteLine($"Nombre: {usuario.Nombre}, Apellido: {usuario.Apellido}, Número de Socio: {usuario.NumeroSocio}, " +
                    $"Identificacion: {usuario.DocumentoIdentidad}, Telefono: {usuario.Telefono} ");
            }
        }


    }
}

