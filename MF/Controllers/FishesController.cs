using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MF.DB;
using MF.Models;
using MF.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace MF.Controllers
{
    public class FishesController : Controller
    {
        private readonly StoreDBContext _context;

        public FishesController(StoreDBContext context)
        {
            _context = context;
        }

        // GET: Fishes
        public async Task<IActionResult> Index()
        {
            var storeDBContext = _context.Fishes.Include(f => f.TypesFish);
            return View(await storeDBContext.ToListAsync());
        }

        // GET: Fishes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fishes = await _context.Fishes
                .Include(f => f.TypesFish)
                .FirstOrDefaultAsync(m => m.id == id);
            if (fishes == null)
            {
                return NotFound();
            }

            return View(fishes);
        }


        public IActionResult Create(int? id)
        {
            FishesVM FishesVM = new()
            {
                Fishes = new Fishes(),
                TypesFishSelectList = _context.TypesFish.Select(
                i => new SelectListItem
                {
                    Text = i.Type,
                    Value = i.id.ToString(),
                }),

            };

            if (id == null) { return View(FishesVM); }
            else
            {
                FishesVM.Fishes = _context.Fishes.Find(id);

                if (FishesVM.Fishes == null) { return NotFound(); }
                else { return View(FishesVM); }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FishesVM fishesVM)
        {
            _context.Fishes.Add(fishesVM.Fishes);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }




        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();

            FishesVM FishesVM = new()
            {
                Fishes = new Fishes(),
                TypesFishSelectList = _context.TypesFish.Select(
                i => new SelectListItem
                {
                    Text = i.Type,
                    Value = i.id.ToString(),
                }),

            };

            if (id == null) { return View(FishesVM); }
            else
            {
                FishesVM.Fishes = _context.Fishes.Find(id);

                if (FishesVM.Fishes == null) { return NotFound(); }
                else { return View(FishesVM); }
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FishesVM fishesVM)
        {
            
            _context.Fishes.Update(fishesVM.Fishes);
            _context.SaveChanges();
            return RedirectToAction("Index");
          
        }



        // GET: Fishes/Edit/5
        public async Task<IActionResult> Edit1(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fishes = await _context.Fishes.FindAsync(id);
            if (fishes == null)
            {
                return NotFound();
            }
            ViewData["TypesFishId"] = new SelectList(_context.TypesFish, "id", "Type", fishes.TypesFishId);
            return View(fishes);
        }

        // POST: Fishes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit1(int id, [Bind("id,Name,TypesFishId")] Fishes fishes)
        {
            if (id != fishes.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fishes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FishesExists(fishes.id))
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
            ViewData["TypesFishId"] = new SelectList(_context.TypesFish, "id", "Type", fishes.TypesFishId);
            return View(fishes);
        }

        // GET: Fishes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fishes = await _context.Fishes
                .Include(f => f.TypesFish)
                .FirstOrDefaultAsync(m => m.id == id);
            if (fishes == null)
            {
                return NotFound();
            }

            return View(fishes);
        }

        // POST: Fishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fishes = await _context.Fishes.FindAsync(id);
            if (fishes != null)
            {
                _context.Fishes.Remove(fishes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FishesExists(int id)
        {
            return _context.Fishes.Any(e => e.id == id);
        }
    }
}
