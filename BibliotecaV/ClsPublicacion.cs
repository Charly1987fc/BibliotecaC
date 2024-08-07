using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaV
{
        public abstract class ClsPublicacion
        {
        //Atributos
            public int Id { get; set; }
            public string Titulo { get; set; }
            public string Autor { get; set; }
            public string ISBN { get; set; }
            public int AnioPublicacion { get; set; }
        //Metodo
        public virtual void MostrarInformacion()
            {
                Console.WriteLine($"Título: {Titulo}, Autor: {Autor}, ISBN: {ISBN}, Año: {AnioPublicacion}");
            }
        }

    }

