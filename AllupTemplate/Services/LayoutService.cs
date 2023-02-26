using AllupTemplate.DataAccessLayer;
using AllupTemplate.Interfaces;
using AllupTemplate.Models;
using AllupTemplate.Views.BaketViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AllupTemplate.Services
{
    public class LayoutService:ILayoutService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LayoutService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }







        public async Task<List<BasketVM>> GetBaskets()
        {
            string cookie = _httpContextAccessor.HttpContext.Request.Cookies["basket"];




            if (!string.IsNullOrWhiteSpace(cookie))
            {
                List<BasketVM> basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(cookie);

                foreach (BasketVM basketVM in basketVMs)
                {
                    Product product = await _context.Products.FirstOrDefaultAsync(p => p.IsDeleted == false && p.Id == basketVM.Id);

                    if (product !=null)
                    {
                        basketVM.Title = product.Title;
                        basketVM.Price = product.DiscountedPrice > 0 ? product.DiscountedPrice : product.Price;
                        basketVM.Image = product.MainImage;
                        basketVM.ExTax = product.ExTax;
                    }
                }
                return basketVMs;
            }

            return new List<BasketVM>(); 
        }










        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories
                .Include(c=>c.Children.Where(c=>c.IsDeleted == false))
                .Where(c=>c.IsDeleted == false && c.IsMain).ToArrayAsync(); 
        }
            
        public async Task<IDictionary<string,string>> GetSettings() {

            IDictionary<string, string> settings = await _context.Settings.ToDictionaryAsync(s=>s.Key,s=>s.Value);

            return settings;
        }
    }
}
