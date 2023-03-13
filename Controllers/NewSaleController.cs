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
        public async Task<ActionResult<IEnumerable<SaleData>>> PostSale(Sale sale)
        {
            //var buyer = await _context.Buyers.FindAsync(sale.BuyerId);
            //sale.Buyer.SalesIds.Add(sale);
            await _context.SaveChangesAsync();

            foreach (SaleData saleData in sale.SalesData)
            {
                var provided_product = _context.ProvidedProducts.Where(pp => pp.SalesPointId == sale.SalesPointId && pp.ProductId == saleData.ProductId).First();
                if (provided_product.ProductQuantity < saleData.ProductQuantity)
                {
                    return Conflict();
                }
            }

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            
            foreach (SaleData saleData in sale.SalesData)
            {
                var provided_product = _context.ProvidedProducts.Where(pp => pp.SalesPointId == sale.SalesPointId && pp.ProductId == saleData.ProductId).First();
                provided_product.ProductQuantity -= saleData.ProductQuantity;
            }

            await _context.SaveChangesAsync();

            // var provided_products = _context.SalesPoints.Where(p => p.Id == sale.SalesPointId).SelectMany(p => p.ProvidedProducts);
            // var sale_data = _context.SalesData.Where(s => s.SaleId == sale.Id);




            return new List<SaleData>(sale.SalesData);
            //return CreatedAtAction("GetSale", new { id = sale.Id }, sale);
        }

        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }
    }
}
