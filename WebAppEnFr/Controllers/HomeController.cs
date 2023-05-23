using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppEnFr.DAL;
using WebAppEnFr.Models;

namespace WebAppEnFr.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            EscritoresDBContext context = new EscritoresDBContext();
            List<Escritor> escritores = context.Escritores.ToList();

            IndexVM model = new IndexVM()
            {
                Escritores = escritores
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult NewEscritor()
        {
            NewEscritorVM model = new NewEscritorVM();
            model.AnioNacimiento = 1971;
            model.Nombre = "";

            return View(model);         
        }

        [HttpPost]
        public IActionResult NewEscritor(NewEscritorVM model)
        {
            Escritor escritor = new Escritor()
            {
                Nombre = model.Nombre,
                AnioNacimiento = model.AnioNacimiento
            };

            EscritoresDBContext context = new EscritoresDBContext();
            context.Escritores.Add(escritor);

            context.SaveChanges();

            //return View(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditEscritor(int id)
        {
            EscritoresDBContext context = new EscritoresDBContext();
            Escritor escritor  = context.Escritores.Find(id);

            EditEscritorVM model = new EditEscritorVM()
            {
                Id = escritor.Id,
                Nombre = escritor.Nombre,
                AnioNacimiento = escritor.AnioNacimiento
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditEscritor(EditEscritorVM model, string comando)
        {
            IActionResult result;
            
            switch (comando)
            {
                case "Modificar":
                    Save(model);
                    result= RedirectToAction("index"); //Ir al listado
                    break;
                case "Recargar":
                    result = View(model); //Regresar a la ficha
                    Reload(model);
                    ModelState.Clear(); //Para que los cambio en el model tomen efecto
                    break;
                default:
                    throw new Exception("el comando no existe");
            }

            return result;
        }

        private void Reload(EditEscritorVM model)
        {
            EscritoresDBContext context = new EscritoresDBContext();
            Escritor escritor = context.Escritores.Find(model.Id);
            model.Nombre = escritor.Nombre;
            model.AnioNacimiento = escritor.AnioNacimiento;
        }

        private void Save(EditEscritorVM model)
        {
            EscritoresDBContext context = new EscritoresDBContext();
            Escritor escritor = context.Escritores.Find(model.Id);
            escritor.Nombre = model.Nombre;
            escritor.AnioNacimiento = model.AnioNacimiento;

            context.SaveChanges();
        }


        [HttpGet]
        public IActionResult DeleteEscritor(int id)
        {
            EscritoresDBContext context = new EscritoresDBContext();
            Escritor escARemover = context.Escritores.Find(id);
            context.Escritores.Remove(escARemover);

            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
