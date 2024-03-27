using CoreApp.Lib.Extension;
using CoreApp.Lib.Helper;
using CoreApp.Lib;
using CoreApp.Model.Employee;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Net;
using Newtonsoft.Json;

namespace AdminHR.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeAssessmentController : Controller
    {
        private IConfiguration Configuration;
        public EmployeeAssessmentController(IConfiguration config)
        {
            Configuration = config;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Employee Assesment";
            return View();
            //return View("~/Areas/Admin/Views/Employee/EmployeeAssessment.cshtml");
        }

        [Route("/Admin/EmployeeAssessment/Detail/{id}")]
        public IActionResult AssessmentDetail(string id = "")
        {;
            ViewData["EmployeeID"] = id;
            ViewData["Title"] = "Detail Assessment";
            return View();
        }

        [Route("/Admin/EmployeeAssessment/AssessmentIndicator/Gets")]
        public async Task<IActionResult> AssessmentIndicatorGets()
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/AssessmentIndicator/Gets", RestSharp.Method.GET);
                var response = client.Execute(request);

                var result = JsonConvert.DeserializeObject<List<EmployeeAssessment>>(response.Content);
                return ApiResult<List<EmployeeAssessment>?>.Ok(result, result!.Count);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        [Route("/Admin/AssessmentIndicator/Getby/Position/{Id}")]
        public async Task<IActionResult> AssessmentIndicatorGet(string Id)
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/AssessmentIndicator/GetBy/Position/{Id}", RestSharp.Method.GET);
                var response = client.Execute(request);

                var result = JsonConvert.DeserializeObject<EmployeeAssessment>(response.Content);
                return ApiResult<EmployeeAssessment?>.Ok(result);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        [Route("Insert")]
        public async Task<IActionResult> Insert(EmployeeAssessment param)
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/EmployeeAssessment/Insert", RestSharp.Method.POST);

                param.Id = GenerateAny.GenerateIdByDateTime(param.Id);
                request.AddJsonParameter(param);
                var response = client.Execute(request);
                return ApiResult<object?>.Ok(response.Content);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        [Route("Update")]
        public async Task<IActionResult> Update(EmployeeAssessment param)
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/EmployeeAssessment/Update", RestSharp.Method.POST);
                request.AddJsonParameter(param);
                var response = client.Execute(request);
                return ApiResult<object?>.Ok(response.Content);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        [Route("Delete")]
        public async Task<IActionResult> Delete(EmployeeAssessment param)
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/EmployeeAssessment/Delete", RestSharp.Method.POST);
                request.AddJsonParameter(param);
                var response = client.Execute(request);
                return ApiResult<object?>.Ok(response.Content);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }
    }
}
