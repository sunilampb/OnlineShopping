using DataLayer;
using Olx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Olx.Controllers
{
    public class AccountController : BaseController
    {
        private IUserReposotiry _userRepository;
        public AccountController()
        {
            _userRepository = new UserReposotiry();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model,string returnUrl)
        {
            if (_userRepository.ValidateUser(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);

               if(string.IsNullOrEmpty(returnUrl))
               {
                   return RedirectToAction("search", "home");
               }
               else
               {
                  return Redirect(returnUrl);
               }
            }
            else
            {
                ViewBag.Error = "Invalid credentials";
                return View();
            }
        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(RegisterModel model)
        {
            DataLayer.DTO.User user = new DataLayer.DTO.User();
            user.City = model.City;
            user.Email = model.Email;
            user.Password = model.Password;
            user.PostalCode = model.PostalCode;
            user.State = model.State;
            user.Name = model.Name;
            user.Mobile = model.Mobile;
            int newUserId = _userRepository.AddUser(user);
            if (newUserId > 0)
            {
                FormsAuthentication.SetAuthCookie(model.Email, false);
                return RedirectToAction("PostAd", "Ad");
            }
            return View();
        }
        public ActionResult AlreadyExits(string email)
        {
            var user = _userRepository.GetUserByEmail(email);
            return Json(user != null, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
