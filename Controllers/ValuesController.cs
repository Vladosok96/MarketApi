using MarketApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ValuesController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<string>> FillValues()
        {

            Buyer buyer1 = new Buyer { Name = "Владислав" };
            Buyer buyer2 = new Buyer { Name = "Роман" };
            Buyer buyer3 = new Buyer { Name = "Наиль" };
            Buyer buyer4 = new Buyer { Name = "Ирина" };
            Product product1 = new Product { Name = "Чашка риса", Price = 50f };
            Product product2 = new Product { Name = "Гороховый суп", Price = 60f };
            Product product3 = new Product { Name = "Cуп с бараниной", Price = 65f };
            Product product4 = new Product { Name = "Сок яблочный", Price = 65f };
            Product product5 = new Product { Name = "Котлета", Price = 60f };
            SalesPoint salesPoint1 = new SalesPoint { Name = "столовая \"Полисмен\"" };
            SalesPoint salesPoint2 = new SalesPoint { Name = "кафе \"Как дома\"" };
            ProvidedProduct providedProduct1 = new ProvidedProduct { SalesPoint = salesPoint1, Product = product1, ProductQuantity = 30 };
            ProvidedProduct providedProduct2 = new ProvidedProduct { SalesPoint = salesPoint1, Product = product2, ProductQuantity = 30 };
            ProvidedProduct providedProduct3 = new ProvidedProduct { SalesPoint = salesPoint1, Product = product3, ProductQuantity = 30 };
            ProvidedProduct providedProduct4 = new ProvidedProduct { SalesPoint = salesPoint1, Product = product4, ProductQuantity = 30 };
            ProvidedProduct providedProduct5 = new ProvidedProduct { SalesPoint = salesPoint1, Product = product5, ProductQuantity = 30 };
            ProvidedProduct providedProduct6 = new ProvidedProduct { SalesPoint = salesPoint2, Product = product1, ProductQuantity = 25 };
            ProvidedProduct providedProduct7 = new ProvidedProduct { SalesPoint = salesPoint2, Product = product2, ProductQuantity = 25 };
            ProvidedProduct providedProduct8 = new ProvidedProduct { SalesPoint = salesPoint2, Product = product3, ProductQuantity = 25 };
            ProvidedProduct providedProduct9 = new ProvidedProduct { SalesPoint = salesPoint2, Product = product4, ProductQuantity = 25 };
            ProvidedProduct providedProduct10 = new ProvidedProduct { SalesPoint = salesPoint2, Product = product5, ProductQuantity = 25 };
            Sale sale1 = new Sale { SalesPoint = salesPoint1, Buyer = buyer1, TotalAmount = product1.Price };
            SaleData saleData1 = new SaleData { Product = product1, Sale = sale1, ProductIdAmount = product1.Price };


            // добавляем их в бд
            _context.ProvidedProducts.AddRange(providedProduct1, providedProduct2, providedProduct3);
            _context.SalesData.Add(saleData1);

            await _context.SaveChangesAsync();

            return "done";
        }
    }
}
