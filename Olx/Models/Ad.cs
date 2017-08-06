using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Olx.Models
{
    public class AdModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "Ad description should be under 500 chars")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Locality { get; set; }
        [Required]
        [RegularExpression("\\d{1,10}")]
        public int Price { get; set; }
        public DateTime ValidTill { get; set; }
        [Display(Name="Category")]
        public int CategoryId { get; set; }

        public int UserId { get; set; }
        public string CategoryName { get; set; }
        public int AdId { get; set; }
        public DateTime PosdtedOn { get; set; }

        public List<byte[]> AdImages { get; set; }

        [Display(Name="Posted by")]
        public string PostedBy { get; set; }
         [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

    }
}
