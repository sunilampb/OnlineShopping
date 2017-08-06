using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO
{
    public class Ad
    {
        public int AdId{ get; set; }       
        public string Title{ get; set; }      
        public string Description{ get; set; }
        public string City{ get; set; }       
        public string Locality{ get; set; }   
        public int Price{ get; set; }      
        public DateTime ValidTill{ get; set; }
        public DateTime PostedDate { get; set; } 
        public int CategoryId{ get; set; } 
        public int UserId{ get; set; }
        public List<byte[]> AdImages { get; set; } 
    }
}
