using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarketApi.Models;

namespace MarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewSaleController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public NewSaleController(ApplicationContext context)
        {
            _context = context;
        }

        // POST: api/NewSale
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sale>> PostSale(Sale sale)
        {
            foreach (SaleData saleData in sale.SalesData)
            {
                var provided_product = _context.ProvidedProducts.Where(pp => pp.SalesPointId == sale.SalesPointId && pp.ProductId == saleData.ProductId).First();
                if (provided_product.ProductQuantity < saleData.ProductQuantity)
                {
                    return Conflict();
                }
            }

            string localDate = DateTime.Now.ToString();
            sale.Date = localDate.Split()[0];
            sale.Time = localDate.Split()[1];

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            float totalAmount = 0;
            foreach (SaleData saleData in sale.SalesData)
            {
                var provided_product = _context.ProvidedProducts.Where(pp => pp.SalesPointId == sale.SalesPointId && pp.ProductId == saleData.ProductId).First();
                provided_product.ProductQuantity -= saleData.ProductQuantity;

                var product = _context.Products.Where(p => p.Id == saleData.ProductId).First();
                saleData.ProductIdAmount = product.Price * saleData.ProductQuantity;
                totalAmount += saleData.ProductIdAmount;
            }

            sale.TotalAmount= totalAmount;

            await _context.SaveChangesAsync();

            return sale;
        }
    }
}
