using Microsoft.AspNetCore.Mvc;

namespace AdminHR.Areas.Admin.Controllers
{ 
    [Area("Admin")]
    public class AttendanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
