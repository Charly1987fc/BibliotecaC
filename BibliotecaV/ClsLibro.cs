using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaV
{
        public class ClsLibro : ClsPublicacion, IPrestable
        {
        //Atributos
            public int NumeroPaginas { get; set; }
        //Metodo
        public override void MostrarInformacion()
            {
                base.MostrarInformacion();
                Console.WriteLine($"Número de Páginas: {NumeroPaginas}");
            }

            public void Prestar()
            {
                Console.WriteLine($"El libro '{Titulo}' ha sido prestado.");
            }

            public void Devolver()
            {
                Console.WriteLine($"El libro '{Titulo}' ha sido devuelto.");
            }
        }

    }

