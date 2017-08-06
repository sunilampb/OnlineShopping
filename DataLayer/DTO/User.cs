using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO
{
    public class User
    {
         public int UserId{ get; set; }  
         public string Name{ get; set; }      
         public string Email{ get; set; }     
         public string Password{ get; set; }  
         public string City{ get; set; }      
         public string PostalCode{ get; set; }
         public string State{ get; set; }     
         public string Mobile{ get; set; }    
    }
}
