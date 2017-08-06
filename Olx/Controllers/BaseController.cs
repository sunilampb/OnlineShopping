using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Olx.Controllers
{
    public class BaseController : Controller
    {
        public DataLayer.DTO.User CurrentUser
        {
            get
            {
                return UserContext.CurrentUser;
            }
        }
    }
}
