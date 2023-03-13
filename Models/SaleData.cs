using System.Text.Json.Serialization;

namespace MarketApi.Models
{
    public class SaleData
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }

        public int SaleId { get; set; }
        [JsonIgnore]
        public virtual Sale Sale { get; set; }

        public int ProductQuantity { get; set; }
        public float ProductIdAmount { get; set; }
    }
}
