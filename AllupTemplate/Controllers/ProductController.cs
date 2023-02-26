using AllupTemplate.DataAccessLayer;
using AllupTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AllupTemplate.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Modal(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Product products = await _context.Products.Include(s => s.ProductImages).FirstOrDefaultAsync(s => s.IsDeleted == false && s.Id == id);
            if (products == null)
            {
                return BadRequest();
            }
            return Json(products);
        }

        public async Task<IActionResult> Search(string search, int? categoryId)
        {
            IEnumerable<Product> products = await _context.Products
                .Where( p=>
                p.IsDeleted == false && 
                (categoryId != null  &&  categoryId > 0 &&  _context.Categories.Any(c=>c.IsDeleted == false && c.Id == categoryId) ? p.CategoryId == categoryId : true ) && 
                (p.Title.ToLower().Contains(search.Trim().ToLower()) ||
                        p.Brand.Name.ToLower().Contains(search.Trim().ToLower()) || 
                         p.Category.Name.ToLower().Contains(search.Trim().ToLower()))
                ).OrderByDescending(p=>p.Id).ToListAsync();

            return PartialView("_SearchPartial", products);



            //if (categoryId != null && categoryId > 0)
            //{
            //    if (!await _context.Categories.AnyAsync(c => c.IsDeleted == false && c.Id == categoryId))

            //    {
            //        return BadRequest();
            //    }

            //    IEnumerable<Product> products = await _context.Products
            //    .Where(p => p.IsDeleted == false && p.CategoryId == categoryId &&
            //    (p.Title.ToLower().Contains(search.Trim().ToLower()) ||
            //    p.Brand.Name.ToLower().Contains(search.Trim().ToLower())))
            //    .ToListAsync();

            //    return PartialView("_SearchPartial", products);
            //}
            //else
            //{
            //    IEnumerable<Product> products = await _context.Products
            //   .Where(p => p.IsDeleted == false &&
            //   (p.Title.ToLower().Contains(search.Trim().ToLower()) ||
            //   p.Brand.Name.ToLower().Contains(search.Trim().ToLower()) || 
            //   p.Category.Name.ToLower().Contains(search.Trim().ToLower()))

            //   )

            //   .ToListAsync();

            //    return PartialView("_SearchPartial", products);




            //}

            //return Ok();
        }
    }
}
