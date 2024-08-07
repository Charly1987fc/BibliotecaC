using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaV
{
        public class ClsDVD : ClsPublicacion
        {
        //Atributos
            public int Duracion { get; set; } // Duración en minutos
                                              //Metodo
        public override void MostrarInformacion()
            {
                base.MostrarInformacion();
                Console.WriteLine($"Duración: {Duracion} minutos");
            }
        }

    }

