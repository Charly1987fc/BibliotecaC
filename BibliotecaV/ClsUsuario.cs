using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaV
{
        public class ClsUsuario
        {
        //Atributos
            public int DocumentoIdentidad { get; set; }
        public int Telefono {  get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string NumeroSocio { get; set; }
        public int Id { get; set; }
        //Metodo
        public void MostrarInformacion()
            {
                Console.WriteLine($"Nombre: {Nombre}, Apellido: {Apellido}, Número de Socio: {NumeroSocio}, Identificacion: {DocumentoIdentidad}," +
                    $"Telefono: {Telefono} ");
            }
        }

    }

