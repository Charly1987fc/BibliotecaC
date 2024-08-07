using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BibliotecaV.Conexion
{

public class ClsBibliotecaContext : DbContext
    {
        public DbSet<ClsLibro> Libros { get; set; }
        public DbSet<ClsUsuario> Usuarios { get; set; }
        public DbSet<ClsPrestamo> Prestamos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-SJSLEMA\\SQLEXPRESS;Initial Catalog=BIBLIOTECAV;Integrated Security=True;");
        }

       

    }

}

