using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DataStoreContext _context;

        public ProductsController(DataStoreContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Product.Include(x => x.Quantity).Include(x=>x.Category);
            return View(await _context.Product.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }


            //var innerJoinQuery =
            //    from category in categories
            //    join prod in products on category.ID equals prod.CategoryID
            //    select new { ProductName = prod.Name, Category = category.Name }; //produces flat sequence

            //img:
            var imgSrc = _context.ProductsImages.Where(x => x.ProdId == id).First().ImgSrc;
            //ViewBag.ImgSrc = imgSrc;
            ViewData["ImgSrc"] = imgSrc;

            //availability,
            var isAvailable = _context.Product.First(x => x.Id == id).IsTradable && 
                              _context.ProductsQuantity.Where(x => x.ProdId == id).ToList().Select(x=>x.Quantity).Sum()>0;
            ViewBag.IsAvailable =isAvailable?"In stock":"Sold Out";

            //sizes
            var sizeQ = from product1 in _context.Product
                     join quantity in _context.ProductsQuantity on product1.Id equals quantity.ProdId
                     join size in _context.ProductSize on quantity.SizeId equals size.Id
                     select new { Value = size.Id, Text = size.Size };
            ViewData["Size"] = new SelectList(await sizeQ.ToListAsync(), "Value", "Text");

            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {      
            var q = from s in _context.ProductCategory
                    select new { Value = s.Id, Text = s.CategoryName};

            ViewData["Category"] = new SelectList(await q.ToListAsync(), "Value", "Text");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Category,Id,Name,Description,Price,IsTradable,CreatedAt")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.CreatedAt = DateTime.Now;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,IsTradable,CreatedAt")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //ViewData["Categories"];
            var product = await _context.Product.SingleOrDefaultAsync(m => m.Id == id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
