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
using Microsoft.AspNetCore.Identity;

namespace MF.Controllers
{
    public class FishingSessionsController : Controller
    {
        private readonly StoreDBContext _context;

        public FishingSessionsController(StoreDBContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            string LogInUser = User.Identity!.Name!;

            if (LogInUser == null)
            {
                return View();
            }

            var user = _context.ApplicationUser.First(m => m.UserName == User.Identity.Name);

            var storeDBContext = _context.FishingSession
                    .Where(f => f.UserId == user.Id)
                    .Include(f => f.ApplicationUser)
                    .Include(f => f.PlaceDb)
                    .Include(f => f.Weather);

            return View(await storeDBContext.ToListAsync());

        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fishingSession = await _context.FishingSession
                .Include(f => f.ApplicationUser)
                .Include(f => f.PlaceDb)
                .Include(f => f.PlaceDb.Coords)
                .Include(f => f.Weather)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fishingSession == null)
            {
                return NotFound();
            }

            return View(fishingSession);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FishingSessionVM fishingSessionVM)
        {

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction(nameof(Index));


            var UserID = _context.ApplicationUser.FirstOrDefault(u => u.UserName == User.Identity.Name);

            FishingSession newsession = new()
            {

                Name = fishingSessionVM.Name,
                Description = fishingSessionVM.Description,
                Date = fishingSessionVM.Date,
                Weather = new Weather()
                {
                    Temp = fishingSessionVM.temp,
                    Pressure = fishingSessionVM.pressure,
                    Humidity = fishingSessionVM.humidity,
                    Windspeed = fishingSessionVM.speed,
                    Winddeg = fishingSessionVM.deg,
                    Sky = fishingSessionVM.sky,
                },
                Rating = fishingSessionVM.Rating,
                PlaceDb = new PlaceDb()
                {
                    Name = fishingSessionVM.PlaceName,
                    Description = fishingSessionVM.PlaceDescription,
                    Coords = new Coords()
                    {
                        Lat = fishingSessionVM.Lat,
                        Long = fishingSessionVM.Lng
                    },
                },
                ApplicationUser = UserID
            };

            FishingSession Testnewsession = newsession;

            _context.FishingSession.Add(newsession);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));



        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fishingSession = await _context.FishingSession.FindAsync(id);
            if (fishingSession == null)
            {
                return NotFound();
            }
            return View(fishingSession);
        }

        // POST: FishingSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Description,Date,Rating,PlaceId,UserId,WeatherId")] FishingSession fishingSession)
        {
            if (id != fishingSession.Id)
            {
                return NotFound();
            }


            try
            {
                _context.FishingSession.Update(fishingSession);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FishingSessionExists(fishingSession.Id))
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



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fishingSession = await _context.FishingSession
                .Include(f => f.ApplicationUser)
                .Include(f => f.PlaceDb)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fishingSession == null)
            {
                return NotFound();
            }

            return View(fishingSession);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fishingSession = await _context.FishingSession.FindAsync(id);

            if (fishingSession != null)
            {
            var Place = await _context.PlaceDb.FindAsync(fishingSession.PlaceId);
            var Weather = await _context.Weather.FindAsync(fishingSession.WeatherId);

                _context.FishingSession.Remove(fishingSession);
                if(Place != null && Weather != null) {
                    _context.PlaceDb.Remove(Place);
                    _context.Weather.Remove(Weather);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool FishingSessionExists(int id)
        {
            return _context.FishingSession.Any(e => e.Id == id);
        }
    }
}
