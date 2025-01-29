using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MF.DB;
using MF.Models;

namespace MF.Controllers
{
    public class TypesFishController : Controller
    {
        private readonly StoreDBContext _context;

        public TypesFishController(StoreDBContext context)
        {
            _context = context;
        }

        // GET: TypesFish
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypesFish.ToListAsync());
        }

        // GET: TypesFish/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typesFish = await _context.TypesFish
                .FirstOrDefaultAsync(m => m.id == id);
            if (typesFish == null)
            {
                return NotFound();
            }

            return View(typesFish);
        }

        // GET: TypesFish/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypesFish/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Type")] TypesFish typesFish)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typesFish);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typesFish);
        }

        // GET: TypesFish/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typesFish = await _context.TypesFish.FindAsync(id);
            if (typesFish == null)
            {
                return NotFound();
            }
            return View(typesFish);
        }

        // POST: TypesFish/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Type")] TypesFish typesFish)
        {
            if (id != typesFish.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typesFish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypesFishExists(typesFish.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(typesFish);
        }

        // GET: TypesFish/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typesFish = await _context.TypesFish
                .FirstOrDefaultAsync(m => m.id == id);
            if (typesFish == null)
            {
                return NotFound();
            }

            return View(typesFish);
        }

        // POST: TypesFish/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typesFish = await _context.TypesFish.FindAsync(id);
            if (typesFish != null)
            {
                _context.TypesFish.Remove(typesFish);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypesFishExists(int id)
        {
            return _context.TypesFish.Any(e => e.id == id);
        }
    }
}
