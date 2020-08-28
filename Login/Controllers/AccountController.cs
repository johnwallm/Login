using Login.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Login.Controllers
{
  
    public class AccountController : Controller
    {
        // GET: Account
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(UserInformation user)
        {
            using (TeraEntities1 db = new TeraEntities1())
            {
                var userDetail = db.UserInformations.Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();




                if (userDetail == null)
                {
                    //user.LoginErrorMessage = "Invalid Username or Password";
                    return View("Login", user);
                }

                else

                {
                    var UserInformationID = userDetail.UserInformationID;
                    var UserTypeID = userDetail.UserTypeID;
                    var Password = userDetail.Password;
                    var Username = userDetail.Username;

                    

                    Session["UserInformationID"] = UserTypeID;
                    Session["UserName"] = Username;
                    Session["UserTypeID"] = UserTypeID;
                    Session["Password"] = Password;

                    if (UserTypeID == 1)
                    {

                        return RedirectToAction("AdminH", "Account");
                    }

                    else if (UserTypeID == 2)
                    {

                        return RedirectToAction("UserPage", "Account");
                    }

                    else
                    {
                        return RedirectToAction("Login", "Account");
                    }
                }

            }


        }

        public ActionResult LogOut()
        {
            int UserInformationID = (int)Session["UserInformationID"];
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Administrator()
        {
            return View();
        }

        public ActionResult UserPage()
        {
            return View();
        }

        //for image saving
        public ActionResult AddImage()
        {
            Brand b1 = new Brand();
            return View(b1);
        }
        //for image saving
        [HttpPost]
        public ActionResult AddImage(Brand model, HttpPostedFileBase image1)
        {
            var db = new CapstoneDemoEntities();
            if (image1!=null)
            {
                model.BranImage = new byte[image1.ContentLength];
                image1.InputStream.Read(model.BranImage, 0, image1.ContentLength);
            }

            db.Brands.Add(model);
            db.SaveChanges();
            return View(model);
        }

        //for image retrieving
        public ActionResult GetImage()
        {
            CapstoneDemoEntities db = new CapstoneDemoEntities();
            var item = (from d in db.Brands
                        select d).ToList();
            return View(item);
        }




    }

}