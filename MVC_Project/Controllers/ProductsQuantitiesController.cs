using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class ProductsQuantitiesController : Controller
    {
        private readonly DataStoreContext _context;

        public ProductsQuantitiesController(DataStoreContext context)
        {
            _context = context;
        }

        // GET: ProductsQuantities
        public async Task<IActionResult> Index()
        {
            var dataStoreContext = _context.ProductsQuantity.Include(p => p.Product).Include(p => p.ProductColor).Include(p => p.ProductSize);
            return View(await dataStoreContext.ToListAsync());
        }

        // GET: ProductsQuantities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productsQuantity = await _context.ProductsQuantity
                .Include(p => p.Product)
                .Include(p => p.ProductColor)
                .Include(p => p.ProductSize)
                .SingleOrDefaultAsync(m => m.ProdId == id);
            if (productsQuantity == null)
            {
                return NotFound();
            }

            return View(productsQuantity);
        }

        // GET: ProductsQuantities/Create
        public IActionResult Create()
        {
            ViewData["ProdId"] = new SelectList(_context.Product, "Id", "Id");
            ViewData["ColorId"] = new SelectList(_context.ProductColor, "Id", "Id");
            ViewData["SizeId"] = new SelectList(_context.ProductSize, "Id", "Id");
            return View();
        }

        public IActionResult Create(int prodID)
        {
            //ViewData["ProdId"] = new SelectList(_context.Product, "Id", "Id");
            ViewData["ProdId"] = prodID;
            ViewData["ColorId"] = new SelectList(_context.ProductColor, "Id", "Id");
            ViewData["SizeId"] = new SelectList(_context.ProductSize, "Id", "Id");
            return View();
        }

        // POST: ProductsQuantities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSpesificQuantity([Bind("ProdId,SizeId,ColorId,Quantity")] ProductsQuantity productsQuantity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productsQuantity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdId"] = new SelectList(_context.Product, "Id", "Id", productsQuantity.ProdId);
            ViewData["ColorId"] = new SelectList(_context.ProductColor, "Id", "Id", productsQuantity.ColorId);
            ViewData["SizeId"] = new SelectList(_context.ProductSize, "Id", "Id", productsQuantity.SizeId);
            return View(productsQuantity);
        }

        // GET: ProductsQuantities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productsQuantity = await _context.ProductsQuantity.SingleOrDefaultAsync(m => m.ProdId == id);
            if (productsQuantity == null)
            {
                return NotFound();
            }
            ViewData["ProdId"] = new SelectList(_context.Product, "Id", "Id", productsQuantity.ProdId);
            ViewData["ColorId"] = new SelectList(_context.ProductColor, "Id", "Id", productsQuantity.ColorId);
            ViewData["SizeId"] = new SelectList(_context.ProductSize, "Id", "Id", productsQuantity.SizeId);
            return View(productsQuantity);
        }

        // POST: ProductsQuantities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProdId,SizeId,ColorId,Quantity")] ProductsQuantity productsQuantity)
        {
            if (id != productsQuantity.ProdId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productsQuantity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsQuantityExists(productsQuantity.ProdId))
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
            ViewData["ProdId"] = new SelectList(_context.Product, "Id", "Id", productsQuantity.ProdId);
            ViewData["ColorId"] = new SelectList(_context.ProductColor, "Id", "Id", productsQuantity.ColorId);
            ViewData["SizeId"] = new SelectList(_context.ProductSize, "Id", "Id", productsQuantity.SizeId);
            return View(productsQuantity);
        }

        // GET: ProductsQuantities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productsQuantity = await _context.ProductsQuantity
                .Include(p => p.Product)
                .Include(p => p.ProductColor)
                .Include(p => p.ProductSize)
                .SingleOrDefaultAsync(m => m.ProdId == id);
            if (productsQuantity == null)
            {
                return NotFound();
            }

            return View(productsQuantity);
        }

        // POST: ProductsQuantities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productsQuantity = await _context.ProductsQuantity.SingleOrDefaultAsync(m => m.ProdId == id);
            _context.ProductsQuantity.Remove(productsQuantity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsQuantityExists(int id)
        {
            return _context.ProductsQuantity.Any(e => e.ProdId == id);
        }
    }
}
