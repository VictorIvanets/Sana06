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
    public class StoresController : Controller
    {
        private readonly StoreDBContext _context;

        public StoresController(StoreDBContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {



            //List<string> UserFulNme = new();

            //IEnumerable<AppUser> UserItem = _context.ApplicationUser;

            //IEnumerable<AppUser> testur = UserItem;

            //foreach (AppUser user in testur) {

            //    UserFulNme.Add(user.FullName);
            
            //}


            var storeDBContext = _context.PlaceDb.Include(p => p.Coords);
            return View(await storeDBContext.ToListAsync());
        }

    }
}
