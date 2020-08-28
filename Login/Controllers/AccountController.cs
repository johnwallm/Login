
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Login.Controllers
{
  
    public class AccountController : Controller
    {
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

               
                db.SaveChanges();
            }
            else
            {
                Response.Write("<script>alert('You did not choose any image(s).'</script>");
            }

            db.Brands.Add(model);
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