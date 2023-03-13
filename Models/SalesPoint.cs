using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MarketApi.Models
{
    public class SalesPoint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProvidedProduct> ProvidedProducts { get; set; }
        [JsonIgnore]
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
