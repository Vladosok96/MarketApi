using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MarketApi.Models
{
    public class Buyer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Sale> SalesIds { get; set; }

    }
}
