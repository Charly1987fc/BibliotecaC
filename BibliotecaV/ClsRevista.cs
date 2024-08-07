using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaV
{
        public class ClsRevista : ClsPublicacion
        {
        //Atributos
            public int NumeroVolumenes { get; set; }
        //Metodo
        public override void MostrarInformacion()
            {
                base.MostrarInformacion();
                Console.WriteLine($"Número de Volúmenes: {NumeroVolumenes}");
            }
        }

    }

