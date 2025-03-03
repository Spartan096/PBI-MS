using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure;
using Domain;

namespace Presentation.WebApp.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IM252ClientesDbContext _context;

        public ClientesController(IM252ClientesDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var clientes = await _context.IM252Cliente.ToListAsync();
            return View(clientes);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var cliente = await _context.IM252Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IM252Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.ID = Guid.NewGuid();
                await _context.IM252Cliente.AddAsync(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var cliente = await _context.IM252Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IM252Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.IM252Cliente.Update(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var cliente = await _context.IM252Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cliente = await _context.IM252Cliente.FindAsync(id);
            if (cliente != null)
            {
                _context.IM252Cliente.Remove(cliente);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
