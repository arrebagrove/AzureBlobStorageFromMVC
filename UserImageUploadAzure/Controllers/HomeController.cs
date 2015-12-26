using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserImageUploadAzure.Utilities;
using Microsoft.AspNet.Identity;
using UserImageUploadAzure.Models;

namespace UserImageUploadAzure.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        BlobUtility utility;
        ApplicationDbContext db; 
        string accountName = "debugmodelab";
        string accountKey = "Lx+CqqV/BSZn49DkaBTbW5mARekRCN+64QFDSoJaTiwpt8UZDa650VGnFgcQizD2ihdRT7Xwb1mI5zjo0n1i2A==";
        public HomeController()
        {
            utility = new BlobUtility(accountName, accountKey);
            db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            string loggedInUserId = User.Identity.GetUserId();
            List<UserImage> userImages = (from r in db.UserImages where r.UserId == loggedInUserId select r).ToList();
            ViewBag.PhotoCount = userImages.Count;
            return View(userImages);
        }

        public ActionResult UploadImage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string ContainerName = "jsr"; //hardcoded container name. 
                file = file ?? Request.Files["file"];
                string fileName = Path.GetFileName(file.FileName);
                Stream imageStream = file.InputStream;
                var result = utility.UploadBlob(fileName, ContainerName, imageStream);
                if (result != null)
                {
                    string loggedInUserId = User.Identity.GetUserId();
                    UserImage userimage = new UserImage();
                    userimage.Id = new Random().Next().ToString();
                    userimage.UserId = loggedInUserId;
                    userimage.ImageUrl = result.Uri.ToString();
                    db.UserImages.Add(userimage);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

            
        }

        public ActionResult DeleteImage(string  id)
        {
            UserImage userImage = db.UserImages.Find(id);
            db.UserImages.Remove(userImage);
            db.SaveChanges();
            string  BlobNameToDelete = userImage.ImageUrl.Split('/').Last();
            utility.DeleteBlob(BlobNameToDelete, "jsr");
            return RedirectToAction("Index");
        }
    }
}