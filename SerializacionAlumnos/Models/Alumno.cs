using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerializacionAlumnos.Models
{
    public class Alumno
    {
        public string Dni { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Carrera { get; set; }
        public string Ciclo { get; set; }

        public Alumno()
        {
        }

        public Alumno(string dni, string nombres,
                      string apellidos, string carrera,
                      string ciclo)
        {
            Dni = dni;
            Nombres = nombres;
            Apellidos = apellidos;
            Carrera = carrera;
            Ciclo = ciclo;
        }
    }
}