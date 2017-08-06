using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olx.Models
{
    public class ContactModel
    {
        public int AdId { get; set; }
        public int UserId { get; set; }
        public string AdTitle { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
    }
}