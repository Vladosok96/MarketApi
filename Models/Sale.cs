using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MarketApi.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

        public int SalesPointId { get; set; }
        [JsonIgnore]
        public SalesPoint SalesPoint { get; set; }

        public int? BuyerId { get; set; }
        [JsonIgnore]
        public Buyer Buyer { get; set; }
        
        
        public virtual ICollection<SaleData> SalesData { get; set; }

        public float TotalAmount { get; set; }
    }
}
