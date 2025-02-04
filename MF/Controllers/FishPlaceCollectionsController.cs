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
    public class FishPlaceCollectionsController : Controller
    {
        private readonly StoreDBContext _context;

        public FishPlaceCollectionsController(StoreDBContext context)
        {
            _context = context;
        }

        // GET: FishPlaceCollections
        public async Task<IActionResult> Index()
        {
            var storeDBContext = _context.FishPlaceCollection.Include(f => f.Fishes).Include(f => f.PlaceDb);
            return View(await storeDBContext.ToListAsync());
        }

        // GET: FishPlaceCollections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fishPlaceCollection = await _context.FishPlaceCollection
                .Include(f => f.Fishes)
                .Include(f => f.PlaceDb)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fishPlaceCollection == null)
            {
                return NotFound();
            }

            return View(fishPlaceCollection);
        }

        // GET: FishPlaceCollections/Create

        public IActionResult Create(int id)
        {

            var Session = _context.FishingSession.Find(id);
            if (Session == null)
                return NotFound();

            var fishPlaceCollection = _context.FishPlaceCollection
            .Include(f => f.Fishes)
            .Include(f => f.PlaceDb)
            .Where(m => m.PlaceDb.id == Session.PlaceId);


            ViewData["FishesId"] = new SelectList(_context.Fishes, "id", "Name");
            ViewData["PlaceId"] = Session.PlaceId.ToString();
            ViewData["SessionId"] = id.ToString();


            FishCollectionCreateVM data = new FishCollectionCreateVM()
            {
                AllfishPlaceCollection
                = fishPlaceCollection,
            };

            return View(data);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FishCollectionCreateVM fishCollectionCreate)
        {


            var isAdded = _context.FishPlaceCollection
            .Where(m => m.PlaceId == fishCollectionCreate.fishPlaceCollection.PlaceId)
            .FirstOrDefault(m => m.FishesId == fishCollectionCreate.fishPlaceCollection.FishesId);
            ViewData["Added"] = "";
            FishPlaceCollection test = isAdded;

            if (isAdded != null)
            {

                return RedirectToAction("Create");
            }

            FishPlaceCollection addFish = new()
            {
                PlaceId = fishCollectionCreate.fishPlaceCollection.PlaceId,
                FishesId = fishCollectionCreate.fishPlaceCollection.FishesId
            };

            _context.FishPlaceCollection.Add(addFish);
            _context.SaveChanges();

            return RedirectToAction("Create");
        }




        // GET: FishPlaceCollections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fishPlaceCollection = await _context.FishPlaceCollection.FindAsync(id);
            if (fishPlaceCollection == null)
            {
                return NotFound();
            }
            ViewData["FishesId"] = new SelectList(_context.Fishes, "id", "Name", fishPlaceCollection.FishesId);
            ViewData["PlaceId"] = new SelectList(_context.PlaceDb, "id", "Name", fishPlaceCollection.PlaceId);
            return View(fishPlaceCollection);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlaceId,FishesId")] FishPlaceCollection fishPlaceCollection)
        {
            if (id != fishPlaceCollection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fishPlaceCollection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FishPlaceCollectionExists(fishPlaceCollection.Id))
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
            ViewData["FishesId"] = new SelectList(_context.Fishes, "id", "Name", fishPlaceCollection.FishesId);
            ViewData["PlaceId"] = new SelectList(_context.PlaceDb, "id", "Name", fishPlaceCollection.PlaceId);
            return View(fishPlaceCollection);
        }

        // GET: FishPlaceCollections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fishPlaceCollection = await _context.FishPlaceCollection
                .Include(f => f.Fishes)
                .Include(f => f.PlaceDb)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fishPlaceCollection == null)
            {
                return NotFound();
            }

            return View(fishPlaceCollection);
        }

        // POST: FishPlaceCollections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {


            var PlaceCollection = await _context.FishPlaceCollection
            .Include(f => f.Fishes)
            .Include(f => f.PlaceDb)
            .FirstOrDefaultAsync(m => m.Id == id);

            if (PlaceCollection == null)
            {
                return NotFound();
            }
            var Session = await _context.FishingSession
            .Include(f => f.PlaceDb)
            .FirstOrDefaultAsync(m => m.PlaceId == PlaceCollection.PlaceId);
            if (Session == null)
            {
                return NotFound();
            }


            var fishPlaceCollection = await _context.FishPlaceCollection.FindAsync(id);
            if (fishPlaceCollection != null)
            {
                _context.FishPlaceCollection.Remove(fishPlaceCollection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Create", new { id = Session.Id});
        }

        private bool FishPlaceCollectionExists(int id)
        {
            return _context.FishPlaceCollection.Any(e => e.Id == id);
        }
    }
}
