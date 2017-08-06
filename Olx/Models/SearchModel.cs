using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olx.Models
{
    public class SearchModel
    {
        public SearchModel()
        {
            Ads = new List<AdModel>();
        }
        public string SearchTerm { get; set; }
        public int? CategoryId { get; set; }
        public string City { get; set; }
        public string Locality { get; set; }
        public int PriceHigh { get; set; }
        public int PriceLow { get; set; }
        public string SortBy { get; set; }

        public List<AdModel> Ads { get; set; }
    }
}