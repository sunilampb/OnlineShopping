using DataLayer;
using DataLayer.DTO;
using Olx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Olx.Controllers
{
    public class AdController : BaseController
    {
        private IAdRepository _adRepo;
        private IUserReposotiry _userRepository;
        private IMessageRepository _msgRepo;
        public AdController()
        {
            _adRepo = new AdRepository();
            _userRepository = new UserReposotiry();
            _msgRepo = new MessageRepository();
        }

        public ActionResult PostAd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PostAd(AdModel model)
        {
            Ad ad = new Ad();
            ad.CategoryId = model.CategoryId;
            ad.City = model.City;
            ad.Description = model.Description;
            ad.Locality = model.Locality;
            ad.PostedDate = DateTime.Now;
            ad.Price = model.Price;
            ad.Title = model.Title;
            ad.ValidTill = DateTime.Now.AddDays(30);
            ad.UserId = CurrentUser.UserId;
            int adId = _adRepo.InsertAd(ad);
            if (adId > 0)
            {
                ViewBag.AdId = adId;
                var files = GetFiles();
                _adRepo.SaveAdImages(files, adId);
                return View("adposted");
            }
            else
            {
                ViewBag.Message = "Sorry there was error in posting  your ad";
                return View(model);
            }
        }
        public ActionResult show(int adid)
        {
            Ad ad = _adRepo.GetAdById(adid);
            if (ad != null)
            {
                AdModel model = new AdModel();
                model.AdId = ad.AdId;
                model.CategoryId = ad.CategoryId;
                model.City = ad.City;
                if (ad.Description.Length > 215)
                {
                    ad.Description = ad.Description.Substring(0, 215) + ".....";
                }
                model.Description = ad.Description;
                model.Locality = ad.Locality;
                model.Price = ad.Price;
                model.Title = ad.Title;
                model.ValidTill = ad.ValidTill;
                model.UserId = ad.UserId;
                model.PosdtedOn = ad.PostedDate;
                var categories = Static.GetCategories();
                var cat = categories.Where(t => t.CategoryId == ad.CategoryId).FirstOrDefault();
                model.CategoryName = cat.CategoryName;
              
                return View(model);
            }
            else
            {
                ViewBag.Error = "Sorry , no such ad";
                return View("Invalid");
            }
        }
        public ActionResult Details(int adId)
        {
            Ad ad = _adRepo.GetAdById(adId);
            if (ad != null)
            {
                AdModel model = new AdModel();
                model.AdId = ad.AdId;
                model.CategoryId = ad.CategoryId;
                model.City = ad.City;
                if (ad.Description.Length > 215)
                {
                    ad.Description = ad.Description.Substring(0, 215) + ".....";
                }
                model.Description = ad.Description;
                model.Locality = ad.Locality;
                model.Price = ad.Price;
                model.Title = ad.Title;
                model.ValidTill = ad.ValidTill;
                model.UserId = ad.UserId;
                model.PosdtedOn = ad.PostedDate;
                var categories = Static.GetCategories();
                var cat = categories.Where(t => t.CategoryId == ad.CategoryId).FirstOrDefault();
                model.CategoryName = cat.CategoryName;
                model.AdImages = ad.AdImages;
                var user = _userRepository.GetUserById(ad.UserId);
                model.PostedBy = user.Name;
                model.ContactNumber = user.Mobile;
                return View(model);
            }
            else
            {
                ViewBag.Error = "Sorry , no such ad";
                return View("Invalid");
            }
        }
        public ActionResult image(int adid)
        {
            var images = _adRepo.GetAdImages(adid);
            if (images.Count > 0)
            {
                return File(images[0], "image/png");
            }
            else
            {
                return File("~/images/ad.png", "image/png");
            }
        }
        public ActionResult contact(int adId)
        {
            var ad = _adRepo.GetAdById(adId);
            if (ad != null)
            {
                var model = new ContactModel();
                model.AdId = adId;
                model.AdTitle = ad.Title;
                var owner = _userRepository.GetUserById(ad.UserId);
                model.UserId = CurrentUser.UserId;
                model.To = owner.Name + " [" + owner.Mobile + "]";
                return View(model);
            }
            else
            {
                ViewBag.Error = "Sorry , no such ad";
                return View("Invalid");
            }
        }
        [HttpPost]
        public ActionResult contact(ContactModel model)
        {
            var msg = new Message();
            msg.AdId = model.AdId;
            msg.MessageText = model.Message;
            msg.FromUserId = model.UserId;
            int msgId = _msgRepo.AddMessage(msg);
            if (msgId > 0)
            {
                return View("msgsent");
            }
            else
            {
                ViewBag.Message = "Sorry there was error in sending your message";
                return View(model);
            }
        }
        private List<byte[]> GetFiles()
        {
           List<byte[]> filesBinaray = new List<byte[]>();
           for (int i = 0; i < Request.Files.Count; i++)
			{
                if (Request.Files[i].ContentLength > 0)
                {
                    BinaryReader br = new BinaryReader(Request.Files[i].InputStream);
                    var bytes = br.ReadBytes(Request.Files[i].ContentLength);
                    filesBinaray.Add(bytes);
                }
			}
           return filesBinaray;
        }
    }
}
