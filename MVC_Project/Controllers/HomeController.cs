using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataStoreContext _context;
        public HomeController(DataStoreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var productsList = _context.Product.OrderBy(x=>x.CreatedAt).Include(x => x.Images).Where(x => x.Images.Count > 0).Include(y => y.Quantity).Take(5).ToListAsync();
            return View(await productsList);//PartialView();-if we want to ignore the general layout, we'll use partial close
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
