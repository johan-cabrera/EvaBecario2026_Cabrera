using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EvaBecario2026_Cabrera.Data_Access;
using EvaBecario2026_Cabrera.Models;

namespace EvaBecario2026_Cabrera.Controllers
{
    public class ReclamosController : Controller
    {
        private readonly ReclamosData reclamoDatos = new ReclamosData();

        // GET: /Reclamos/
        public ActionResult Index()
        {
            var reclamos = reclamoDatos.Listar();
            return View(reclamos);
        }

        // GET: /Reclamos/Crear
        public ActionResult Crear()
        {
            return View();
        }

        // POST: /Reclamos/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(ReclamosModel modelo)
        {
            if (reclamoDatos.ExisteDUI(modelo.DUI))
            {
                ModelState.AddModelError("DUI", "Ya existe un reclamo registrado con este número de DUI.");
            }

            if (ModelState.IsValid)
            {
                bool guardado = reclamoDatos.Agregar(modelo);
                if (guardado)
                {
                    TempData["Mensaje"] = "Reclamo registrado exitosamente.";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Ocurrió un error al registrar en la base de datos.");
            }

            return View(modelo);
        }

        // GET: /Reclamos/Editar/id
        public ActionResult Editar(int id)
        {
            var reclamo = reclamoDatos.ObtenerPorId(id);
            if (reclamo == null)
            {
                return HttpNotFound();
            }
            return View(reclamo);
        }

        // POST: /Reclamos/Editar/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(ReclamosModel modelo)
        {
            if (reclamoDatos.ExisteDUI(modelo.DUI, modelo.IdReclamo))
            {
                ModelState.AddModelError("DUI", "El número de DUI ya está registrado en otro reclamo.");
            }

            if (ModelState.IsValid)
            {
                bool actualizado = reclamoDatos.Editar(modelo);
                if (actualizado)
                {
                    TempData["Mensaje"] = "Reclamo actualizado exitosamente.";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Ocurrió un error al actualizar los datos.");
            }

            return View(modelo);
        }

        // POST: /Reclamos/Borrar/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Borrar(int id)
        {
            bool eliminado = reclamoDatos.Eliminar(id);
            if (eliminado)
            {
                TempData["Mensaje"] = "Reclamo eliminado correctamente.";
            }
            else
            {
                TempData["Error"] = "No se pudo eliminar el reclamo seleccionado.";
            }

            return RedirectToAction("Index");
        }
    }
}