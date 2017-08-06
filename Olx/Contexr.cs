using DataLayer;
using DataLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olx
{
    public class UserContext
    {
        public static string UserEmail
        {
            get { return HttpContext.Current.User.Identity.Name; }
        }
         public static bool IsAuthenticated
        {
            get { return HttpContext.Current.User.Identity.IsAuthenticated ;}
        }
         public static User CurrentUser
         {
             get
             {
              return  new UserReposotiry().GetUserByEmail(UserEmail);
                            }
         }
    }
} 