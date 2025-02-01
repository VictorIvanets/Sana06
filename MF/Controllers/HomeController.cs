using MF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using MF.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using MF.DB;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;

namespace MF.Controllers
{



    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StoreDBContext _context;



        public HomeController(ILogger<HomeController> logger, StoreDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        static HttpClient client = new HttpClient();





        public async Task<IActionResult> Index()
        {
            string LogInUser = User.Identity!.Name!;

            if(LogInUser == null)
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

        public IActionResult Privacy()
        {
            return View();
        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
