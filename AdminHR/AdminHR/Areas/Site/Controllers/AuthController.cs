using CoreApp.Lib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminHR.Areas.Site.Controllers
{
    [Area("Site")]
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Login", "Auth", new { area = "Site" });
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            ViewBag.Title = "Login";
            return View();
        }

        public async Task<IActionResult> GoIn()
        {
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            //return ApiResult<object>.Ok("success");
        }
    }
}
