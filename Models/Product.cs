using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MarketApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProvidedProduct> ProvidedProducts { get; set; }
        [JsonIgnore]
        public virtual ICollection<SaleData> SalesData { get; set; }
    }
}
