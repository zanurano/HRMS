using Microsoft.AspNetCore.Mvc;

namespace AdminHR.Areas.Admin.Controllers
{
    public class ReportController : Controller
    {
        private IConfiguration Configuration;
        public ReportController(IConfiguration config)
        {
            Configuration = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ReportAttendance()
        {
            return View();
        }

        public IActionResult ReportEmployeeAssessment()
        {
            return View();
        }
    }
}
