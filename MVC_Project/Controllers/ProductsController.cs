using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using static System.Net.Mime.MediaTypeNames;

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
        public async Task<IActionResult> Index(string searchBox)
        {
            //sizes
            var sizeQ =  from size in _context.ProductSize 
                         select new { Value = size.Id, Text = size.Size };
            ViewData["Size"] = new SelectList(await sizeQ.ToListAsync(), "Value", "Text");

            //categories
            var categoryQ= from s in _context.ProductCategory
                    select new { Value = s.Id, Text = s.CategoryName };

            ViewData["Category"] = new SelectList(await categoryQ.ToListAsync(), "Value", "Text");

            var productsList = GetProductSearchResults(searchBox);
            return View(await productsList);
        }

        public async Task<PartialViewResult> IndexPartial(string searchBox, int? categoryID)
        {
            var list = _context.Product.Include(x => x.Images).Where(x => x.Images.Count > 0).Include(y => y.Quantity);

            if (!String.IsNullOrEmpty(searchBox))
            {
                list = list.Where(s => s.Name.ToLower().Contains(searchBox.ToLower()) || s.Description.ToLower().Contains(searchBox.ToLower())).Include(x => x.Images).Where(x => x.Images.Count > 0).Include(y => y.Quantity);
            }

            //var productsList = GetProductSearchResults(searchBox);
            if(categoryID!=null && categoryID!=0)
            {
                list =  list.Where(x => x.CategoryId == categoryID.Value).Include(x => x.Images).Where(x => x.Images.Count > 0).Include(y => y.Quantity);
                //productsList = productsList.Result.Where(x => x.CategoryId == categoryID.Value).ToList().AsQueryable().ToListAsync();
            }


            return PartialView(await list.ToListAsync());
        }

        private Task<List<Product>> GetProductSearchResults(string text)
        {
            var productsList = _context.Product.Include(x => x.Images).Where(x => x.Images.Count > 0).Include(y => y.Quantity).ToListAsync();
            if (!String.IsNullOrEmpty(text))
            {
                productsList = _context.Product.Where(s => s.Name.ToLower().Contains(text.ToLower()) || s.Description.ToLower().Contains(text.ToLower())).ToListAsync();
            }
            return productsList;

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


            //img:
            //var imgSrc = _context.ProductsImages.Where(x => x.ProdId == id).First().ImgSrc;
            //ViewBag.ImgSrc = imgSrc;
            ViewData["ImgSrc"] = GetImagesSrcByID(id.Value)[0];            

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
            //Colors
            var colorQ = from s in _context.ProductColor
                         select new { Value = s.Id, Text = s.Color };

            ViewData["Color"] = new SelectList(await colorQ.ToListAsync(), "Value", "Text");

            //sizes
            var sizeQ = from size in _context.ProductSize
                        select new { Value = size.Id, Text = size.Size };
            ViewData["Size"] = new SelectList(await sizeQ.ToListAsync(), "Value", "Text");

            //categories:
            var q = from s in _context.ProductCategory
                    select new  { Value = s.Id, Text = s.CategoryName};

            ViewData["Category"] = new SelectList(await q.ToListAsync(), "Value", "Text");
            //ViewData["Category"] = _context.ProductCategory.ToList();

            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("CategoryId,Id,Name,Description,Price,IsTradable,CreatedAt,Images")] Product product,Image image)
        public async Task<IActionResult> Create([Bind("CategoryId,Id,Name,Description,Price,IsTradable,CreatedAt,Images")] Product product)
        {
            if (ModelState.IsValid)
            {
                if(product.Images==null)
                {
                    product.Images = new List<ProductsImages>() { };
                    product.Images.Add(new ProductsImages() { ProdId = product.Id, ColorId = 1, ImgSrc = "Poster_not_available.jpg" });
                }
                product.CreatedAt = DateTime.Now;
                _context.Add(product);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                var createdProd= _context.Product.LastOrDefault();
                //return RedirectToAction("CreateSpesificQuantity", "ProductsQuantities", createdProd.Id);
                return RedirectToAction("Create", "ProductsQuantities");
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

        // להעביר למקשרת
        //public async Task<IActionResult> AddToCart(int idProduct)
        //{
        //    return ;
        //}

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart([Bind("Id,Price,Quantity")] ProductsQuantity product)
        {
            return RedirectToAction("Edit", "Carts",  product );
        }

        public async Task<PartialViewResult> Search(string searchBox)
        {
            Task<List<Product>> filteredItems = _context.Product.Where(s => s.Name.ToLower().Contains(searchBox.ToLower()) || s.Description.ToLower().Contains(searchBox.ToLower())).ToListAsync();
            return PartialView("Index",await filteredItems);

            //return Index(await );

            //if (searchBox != null)
            //    return Index(await _context.Product.Where(s => s.Name.ToLower().Contains(searchBox.ToLower()) || s.Description.ToLower().Contains(searchBox.ToLower())).ToListAsync());
            //    //return Json(await _context.Product.Where(s => s.Name.ToLower().Contains(searchBox.ToLower()) || s.Description.ToLower().Contains(searchBox.ToLower())).ToListAsync());
            //return Json(await _context.Product.ToListAsync());
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

        #region DB queries
        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        private List<string> GetImagesSrcByID(int id)
        {
            return _context.ProductsImages.Where(x => x.ProdId == id).Select(x => x.ImgSrc).ToList();
        }
        #endregion


    }
}
