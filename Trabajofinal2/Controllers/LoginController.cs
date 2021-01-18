using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trabajofinal2.Models;

namespace Trabajofinal2.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autherize(Trabajofinal2.Models.User userModel)
        {
            using (TrabajoFinalEntities db = new TrabajoFinalEntities())
            {
                var userDetails = db.Users.Where(x => x.name == userModel.name && x.password == userModel.password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Las credenciales son Incorrectas";
                    return View("Index", userModel); // te direcciona al Login
                }
                else
                {
                    Session["userID"] = userDetails.UserID;
                    Session["userName"] = userDetails.name;
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public ActionResult LogOut()
        {
            
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}