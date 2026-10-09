
using Newtonsoft.Json;
using SerializacionAlumnos.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace SerializacionAlumnos.Controllers
{
    public class AlumnoController : Controller
    {
        string ruta = "";

        public AlumnoController()
        {
            ruta = System.Web.Hosting.HostingEnvironment
                   .MapPath("~/App_Data/alumnos.json");
        }

        private List<Alumno> LeerJson()
        {
            if (!System.IO.File.Exists(ruta))
            {
                System.IO.File.WriteAllText(ruta, "[]");
            }

            string json = System.IO.File.ReadAllText(ruta);

            return JsonConvert.DeserializeObject<List<Alumno>>(json)
                   ?? new List<Alumno>();
        }

        private void GuardarJson(List<Alumno> lista)
        {
            string json =
                JsonConvert.SerializeObject(lista, Formatting.Indented);

            System.IO.File.WriteAllText(ruta, json);
        }

        // INDEX
        public ActionResult Index()
        {
            List<Alumno> lista = LeerJson();
            return View(lista);
        }

        // GET: AGREGAR
        public ActionResult Agregar()
        {
            return View();
        }

        // POST: AGREGAR
        [HttpPost]
        public ActionResult Agregar(Alumno alu)
        {
            List<Alumno> lista = LeerJson();

            bool existe = lista.Any(x => x.Dni == alu.Dni);

            if (!existe)
            {
                lista.Add(alu);
                GuardarJson(lista);
            }

            return RedirectToAction("Index");
        }

        // ELIMINAR
        public ActionResult Eliminar(string id)
        {
            List<Alumno> lista = LeerJson();

            Alumno alumno = lista.Find(x => x.Dni == id);

            if (alumno != null)
            {
                lista.Remove(alumno);
                GuardarJson(lista);
            }

            return RedirectToAction("Index");
        }

        // DETALLES
        public ActionResult Detalles(string id)
        {
            List<Alumno> lista = LeerJson();

            Alumno alu = lista.Find(x => x.Dni == id);

            if (alu == null)
            {
                return HttpNotFound();
            }

            return View(alu);
        }
        // GET: ACTUALIZAR
        public ActionResult Actualizar(string id)
        {
            List<Alumno> lista = LeerJson();

            Alumno alu = lista.Find(x => x.Dni == id);

            if (alu == null)
            {
                return HttpNotFound();
            }

            return View(alu);
        }

        // POST: ACTUALIZAR
        [HttpPost]
        public ActionResult Actualizar(Alumno alu)
        {
            List<Alumno> lista = LeerJson();

            Alumno alumnoActual = lista.Find(x => x.Dni == alu.Dni);

            if (alumnoActual != null)
            {
                alumnoActual.Nombres = alu.Nombres;
                alumnoActual.Apellidos = alu.Apellidos;
                alumnoActual.Carrera = alu.Carrera;
                alumnoActual.Ciclo = alu.Ciclo;

                GuardarJson(lista);
            }

            return RedirectToAction("Index");
        }
    }
}