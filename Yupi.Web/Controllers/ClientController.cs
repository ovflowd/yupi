using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Yupi.Web.Controllers
{
    [RoutePrefix ("client")]
    public class ClientController : Controller
    {
        private static readonly string LoadingText = String.Join("/", new String[] {
            "For science, you monster",
            "Loading funny message\\u2026please wait.",
            "Would you like fries with that?",
            "Follow the yellow duck.",
            "Time is just an illusion.",
            "Are we there yet?!",
            "I like your t-shirt.",
            "Look left. Look right. Blink twice. Ta da!",
            "It's not you, it's me.",
            "Shhh! I'm trying to think here.",
            "Loading pixel universe."
        });

        [Route ("")]
        public ActionResult Index ()
        {
            ViewBag.LoadingText = LoadingText;
            return View ();
        }

        [Route ("external_variables")]
        public ActionResult ExternalVariables ()
        {
            // TODO Implement
            byte [] data = Encoding.UTF8.GetBytes ("hi=there");
            return File (data, "text/plain");
        }

        [Route ("external_override_variables")]
        public ActionResult ExternalOverrideVariables ()
        {
            // TODO Implement
            byte [] data = Encoding.UTF8.GetBytes ("");
            return File (data, "text/plain");
        }

        [Route ("external_texts")]
        public ActionResult ExternalTexts ()
        {
            // TODO Implement
            byte [] data = Encoding.UTF8.GetBytes ("hi=there");
            return File (data, "text/plain");
        }

        [Route ("external_override_texts")]
        public ActionResult ExternalOverrideTexts ()
        {
            // TODO Implement
            byte [] data = Encoding.UTF8.GetBytes ("");
            return File (data, "text/plain");
        }

        [Route ("furnidata")]
        public ActionResult Furnidata ()
        {
            // TODO Implement
            return Json (null);
        }

        [Route ("productdata")]
        public ActionResult Productdata ()
        {
            // TODO Implement
            return Json (null);
        }
    }
}