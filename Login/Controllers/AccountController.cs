using Login.Models;
using System;
using System.Collections.Generic;
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

                        return RedirectToAction("UserPage", "Entities");
                    }

                    else
                    {
                        return RedirectToAction("Login", "Entities");
                    }
                }

            }


        }

        public ActionResult LogOut()
        {
            int UserInformationID = (int)Session["UserID"];
            Session.Abandon();
            return RedirectToAction("Login", "Entities");
        }
    }
}