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
    public class CartsController : Controller
    {
        private readonly DataStoreContext _context;

        public CartsController(DataStoreContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            var dataStoreContext = _context.Cart.Include(c => c.Product).Include(c => c.ProductColor).Include(c => c.ProductSize).Include(c => c.User);
            return View(await dataStoreContext.ToListAsync());
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.Product)
                .Include(c => c.ProductColor)
                .Include(c => c.ProductSize)
                .Include(c => c.User)
                .SingleOrDefaultAsync(m => m.UserId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        //public IActionResult Create()
        //{
        //    ViewData["ProdId"] = new SelectList(_context.Product, "Id", "Id");
        //    ViewData["ColorId"] = new SelectList(_context.ProductColor, "Id", "Id");
        //    ViewData["SizeId"] = new SelectList(_context.ProductSize, "Id", "Id");
        //    ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
        //    return View();
        //}

        // POST: Carts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdId"] = new SelectList(_context.Product, "Id", "Id", cart.ProdId);
            ViewData["ColorId"] = new SelectList(_context.ProductColor, "Id", "Id", cart.ColorId);
            ViewData["SizeId"] = new SelectList(_context.ProductSize, "Id", "Id", cart.SizeId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", cart.UserId);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.SingleOrDefaultAsync(m => m.UserId == id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["ProdId"] = new SelectList(_context.Product, "Id", "Id", cart.ProdId);
            ViewData["ColorId"] = new SelectList(_context.ProductColor, "Id", "Id", cart.ColorId);
            ViewData["SizeId"] = new SelectList(_context.ProductSize, "Id", "Id", cart.SizeId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", cart.UserId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,ProdId,SizeId,ColorId,Quantity")] Cart cart)
        {
            if (id != cart.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.UserId))
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
            ViewData["ProdId"] = new SelectList(_context.Product, "Id", "Id", cart.ProdId);
            ViewData["ColorId"] = new SelectList(_context.ProductColor, "Id", "Id", cart.ColorId);
            ViewData["SizeId"] = new SelectList(_context.ProductSize, "Id", "Id", cart.SizeId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", cart.UserId);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.Product)
                .Include(c => c.ProductColor)
                .Include(c => c.ProductSize)
                .Include(c => c.User)
                .SingleOrDefaultAsync(m => m.UserId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Cart.SingleOrDefaultAsync(m => m.UserId == id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.UserId == id);
        }
    }
}
