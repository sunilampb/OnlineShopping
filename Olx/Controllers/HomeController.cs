using DataLayer;
using DataLayer.DTO;
using Olx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Olx.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private IAdRepository _adRepo;
        private IUserReposotiry _userRepository;
        private IMessageRepository _msgRepo;
        public HomeController()
        {
            _adRepo = new AdRepository();
            _userRepository = new UserReposotiry();
            _msgRepo = new MessageRepository();
        }
        public ActionResult search()
        {
            return View(new SearchModel());
        }
        [HttpPost]
        public ActionResult search(SearchModel model)
        {
            List<AdModel> results = new List<AdModel>();
            string city = model.City;
            if(string.IsNullOrEmpty(model.SearchTerm))
            {
                ViewBag.Error="Enter some search text";
                model.Ads = results;
                return View(model);
            }
            if(string.IsNullOrEmpty(model.City))
            {
                city=null;
            }
            if (model.City == "Any")
            {
                city = null;
            }
            if(string.IsNullOrEmpty(model.Locality))
            {
                model.Locality=null;
            }
            var ads = _adRepo.SearchAds(model.SearchTerm, model.CategoryId, city, model.Locality,
                                         model.PriceLow,model.PriceHigh,model.SortBy);
            foreach (var ad in ads)
            {
                    AdModel adObj = new AdModel();
                    adObj.AdId = ad.AdId;
                    adObj.CategoryId = ad.CategoryId;
                    adObj.City = ad.City;
                    if (ad.Description.Length > 215)
                    {
                        ad.Description = ad.Description.Substring(0, 215) + ".....";
                    }
                    adObj.Description = ad.Description;
                    adObj.Locality = ad.Locality;
                    adObj.Price = ad.Price;
                    adObj.Title = ad.Title;
                    adObj.ValidTill = ad.ValidTill;
                    adObj.UserId = ad.UserId;
                    adObj.PosdtedOn = ad.PostedDate;
                    var categories = Static.GetCategories();
                    var cat = categories.Where(t => t.CategoryId == ad.CategoryId).FirstOrDefault();
                    adObj.CategoryName = cat.CategoryName;
                    results.Add(adObj);
            }
            model.Ads=results;
            return View(model);
        }

        public ActionResult inbox()
        {
            var messages = _msgRepo.GetMessageForUser(CurrentUser.UserId);
            return View(messages);
        }
        [HttpPost]
        public ActionResult messagebox(int adId,int toUserId,int msgId)
        {
            var model= new ContactModel();
            model.AdId=adId;
            model.UserId=toUserId;
            ViewBag.MessageId = msgId;
            return PartialView("MessageBox", model);
        }
        [HttpPost]
        public ActionResult reply(ContactModel model,int MessageId)
        {
            var msg = new Message();
            msg.AdId = model.AdId;
            msg.MessageText = model.Message;
            msg.FromUserId = model.UserId;
            int msgId = _msgRepo.AddMessage(msg);
            if (msgId > 0)
            {
                return Json(new { status = 1, id =MessageId }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = 0, id = MessageId }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
