﻿using AllupTemplate.DataAccessLayer;
using AllupTemplate.Models;
using AllupTemplate.Views.BaketViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Newtonsoft.Json;
using NuGet.ContentModel;
using System.Collections.Generic;
using System.Globalization;

namespace AllupTemplate.Controllers
{
	public class BasketController : Controller
	{
		private readonly AppDbContext _context;

		public BasketController(AppDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> AddBaket(int? id)
		{

			if (id == null) return BadRequest();
			if (await _context.Products.AnyAsync(p => p.IsDeleted == false && p.Id == id)) return NotFound();

			//Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
			//if (product == null) return NotFound();

			string cookie = HttpContext.Request.Cookies["basket"];

			List<BasketVM> basketVMs = null;


            if (string.IsNullOrWhiteSpace(cookie))
			{
				basketVMs = new List<BasketVM>
				{
					new BasketVM{Id = (int) id,Count = 1}
				};
            }
            else
			{
				basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(cookie);
				if (basketVMs.Exists(p=>p.Id == id))
				{
					basketVMs.Find(b=>b.Id == id).Count += 1;
				}
				else
				{
					basketVMs.Add(new BasketVM { Id = (int)id, Count = 1 });
				};
				
            }
            cookie = JsonConvert.SerializeObject(basketVMs);
            HttpContext.Response.Cookies.Append("basket", cookie);


            foreach (BasketVM basketVM in basketVMs)
            {
                Product product = await _context.Products.FirstOrDefaultAsync(p => p.IsDeleted == false && p.Id == basketVM.Id);

                if (product != null)
                {
                    basketVM.Title = product.Title;
                    basketVM.Price = product.DiscountedPrice > 0 ? product.DiscountedPrice : product.Price;
                    basketVM.Image = product.MainImage;
                    basketVM.ExTax = product.ExTax;
                }
            }

            return PartialView("_BasketCartPartial", basketVMs);

        }
        public async Task<IActionResult> GetBasket()
		{
			string basket = HttpContext.Request.Cookies["basket"];
			List<BasketVM> basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
			return Json(basketVMs);
		}
	}
}
