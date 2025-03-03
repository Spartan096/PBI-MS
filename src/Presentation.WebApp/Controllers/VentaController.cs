using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using Infrastructure;
using Domain;

namespace Presentation.WebApp.Controllers
{
    public class VentaController : Controller
    {
        private readonly IM252VentaDbContext _IM252VentaDbContext;

        // Constructor para la inyección de dependencias
        public VentaController(IM252VentaDbContext context)
        {
            _IM252VentaDbContext = context;
        }

        // Acción para mostrar todas las ventas
        public IActionResult Index()
        {
            // Aquí omitimos el Include si no necesitamos cargar Cliente y Producto para la vista Index
            var ventas = _IM252VentaDbContext.IM252Venta
                .ToList();  // Si no necesitas los detalles de Cliente o Producto, puedes omitir Include.

            return View(ventas);  // Pasar los datos a la vista
        }

        // Acción para mostrar los detalles de una venta
        public IActionResult Details(Guid id)
        {
            var venta = _IM252VentaDbContext.IM252Venta
                .Include(v => v.Cliente)
                .Include(v => v.Producto)
                .FirstOrDefault(v => v.Id == id);

            if (venta == null)
            {
                return NotFound();
            }
            return View(venta);  // Pasar los detalles a la vista
        }

        // Acción para mostrar el formulario de creación de una nueva venta
        public IActionResult Create()
        {
            return View();  // Renderizar la vista para crear
        }

        // Acción para guardar la venta
        [HttpPost]
        public IActionResult Create(IM252Venta venta)
        {
            if (ModelState.IsValid)
            {
                _IM252VentaDbContext.IM252Venta.Add(venta);  // Agregar nueva venta
                _IM252VentaDbContext.SaveChanges();  // Guardar cambios
                return RedirectToAction("Index");  // Redirigir a la lista de ventas
            }
            return View(venta);  // Si no es válida, volver a mostrar el formulario
        }

        // Acción para mostrar el formulario de edición de una venta
        public IActionResult Edit(Guid id)
        {
            var venta = _IM252VentaDbContext.IM252Venta
                .Include(v => v.Cliente)
                .Include(v => v.Producto)
                .FirstOrDefault(v => v.Id == id);

            if (venta == null)
            {
                return NotFound();
            }
            return View(venta);  // Pasar la venta a la vista para editar
        }

        // Acción para guardar los cambios en la venta
        [HttpPost]
        public IActionResult Edit(IM252Venta venta)
        {
            if (ModelState.IsValid)
            {
                _IM252VentaDbContext.IM252Venta.Update(venta);  // Actualizar venta
                _IM252VentaDbContext.SaveChanges();  // Guardar cambios
                return RedirectToAction("Index");  // Redirigir a la lista de ventas
            }
            return View(venta);  // Si no es válida, volver a mostrar el formulario
        }

        // Acción para mostrar la confirmación de eliminación de una venta
        public IActionResult Delete(Guid id)
        {
            var venta = _IM252VentaDbContext.IM252Venta
                .Include(v => v.Cliente)  // Traer información del Cliente
                .Include(v => v.Producto) // Traer información del Producto
                .FirstOrDefault(v => v.Id == id);

            if (venta == null)
            {
                return NotFound();
            }
            return View(venta);  // Pasar la venta a la vista para confirmar eliminación
        }

        // Acción para confirmar la eliminación de una venta
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var venta = _IM252VentaDbContext.IM252Venta
                .FirstOrDefault(v => v.Id == id);  // Obtener la venta por ID

            if (venta == null)
            {
                return NotFound();
            }

            _IM252VentaDbContext.IM252Venta.Remove(venta);  // Eliminar la venta
            _IM252VentaDbContext.SaveChanges();  // Guardar los cambios
            return RedirectToAction("Index");  // Redirigir a la lista de ventas
        }
    }
}
