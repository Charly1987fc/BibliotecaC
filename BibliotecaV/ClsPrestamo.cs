using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaV
{
        public class ClsPrestamo
        {
        //Atributos
            public int Id { get; set; }
            public int LibroId { get; set; }
            public int UsuarioId { get; set; }
            public DateTime FechaPrestamo { get; set; }
            public DateTime FechaDevolucion { get; set; }

            public ClsLibro Libro { get; set; }
            public ClsUsuario Usuario { get; set; }
        }

    }

