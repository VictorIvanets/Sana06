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

namespace MF.Controllers
{
    public class PlaceDbController : Controller
    {
        private readonly StoreDBContext _context;

        public PlaceDbController(StoreDBContext context)
        {
            _context = context;
        }

        // GET: PlaceDbs
        public async Task<IActionResult> Index()
        {
            var storeDBContext = _context.PlaceDb.Include(p => p.Coords);
            return View(await storeDBContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {

            var obj = _context.PlaceDb.Include(u => u.Coords).First(u => u.id == id);


            Place place = new Place()
            {

                Name = obj.Name,
                Description = obj.Description,
                Lat = obj.Coords.Lat,
                Long = obj.Coords.Long
            };

            if (obj == null)
            {
                return NotFound();
            }

            return View(place);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Place place)
        {

            PlaceDb PlaceInDb = new()
            {
                Name = place.Name,
                Description = place.Description,
                Coords = new Coords() { Lat = place.Lat, Long = place.Long },
            };

            _context.PlaceDb.Add(PlaceInDb);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placeDb = await _context.PlaceDb.FindAsync(id);
            if (placeDb == null)
            {
                return NotFound();
            }
            ViewData["CoordsId"] = new SelectList(_context.Coords, "id", "id", placeDb.CoordsId);
            return View(placeDb);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Description,CoordsId")] PlaceDb placeDb)
        {

            if (id != placeDb.id)
            {
                return NotFound();
            }

            try
            {
                _context.PlaceDb.Update(placeDb);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceDbExists(placeDb.id))
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

        // GET: PlaceDbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placeDb = await _context.PlaceDb
                .Include(p => p.Coords)
                .FirstOrDefaultAsync(m => m.id == id);
            if (placeDb == null)
            {
                return NotFound();
            }

            return View(placeDb);
        }

        // POST: PlaceDbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var placeDb = await _context.PlaceDb.FindAsync(id);
            if (placeDb != null)
            {
                _context.PlaceDb.Remove(placeDb);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaceDbExists(int id)
        {
            return _context.PlaceDb.Any(e => e.id == id);
        }
    }
}
