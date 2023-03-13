using System.Text.Json.Serialization;

namespace MarketApi.Models
{
    public class ProvidedProduct
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }

        public int SalesPointId { get; set; }
        [JsonIgnore]
        public virtual SalesPoint SalesPoint { get; set; }

        public int ProductQuantity { get; set; }
    }
}
