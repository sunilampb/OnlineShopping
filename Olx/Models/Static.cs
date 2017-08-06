using DataLayer;
using DataLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Olx.Models
{
    public class Static
    {
        public static List<Category> GetCategories()
        {
            return new AdRepository().GetCategories();
        }
        public static List<string> Cities()
        {
            var list = new List<string>();
            list.Add("Any");
            list.Add("Amravati");
            list.Add("Nagpur");
            list.Add("Akola");
            list.Add("Yavatmal");
            return list;
        }
        public static List<int> PriceRange()
        {
                var list = new List<int>();
                list.Add(100);
                list.Add(500);
                list.Add(2000);
                list.Add(5000);
                list.Add(8000);
                list.Add(12000);
                list.Add(16000);
                list.Add(20000);
                list.Add(25000);
                return list;
        }
        public static List<string> SortByItems()
        {
            var list = new List<string>();
            list.Add("Price");
            list.Add("PostedDate");
            return list;
        }
    }
}
