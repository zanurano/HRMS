using CoreApp.Lib.Extension;
using CoreApp.Lib;
using CoreApp.Model.Administration;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Net;
using CoreApp.Model.Employee;
using Newtonsoft.Json;
using CoreApp.Lib.Helper;

namespace AdminHR.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdministratorController : Controller
    {
        private IConfiguration Configuration;
        public AdministratorController(IConfiguration config)
        {
            Configuration = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }

        [Route("Admin/Administration/User/Insert")]
        public async Task<IActionResult> CreateUser(Users param)
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/Administrator/User/Insert", RestSharp.Method.POST);
                request.AddJsonParameter(param);
                var response = client.Execute(request);
                return ApiResult<object?>.Ok(response.Content);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        public IActionResult AssessmentIndicator()
        {
            return View();
        }

        [Route("/Admin/Administrator/AssessmentIndicator/Gets")]
        public async Task<IActionResult> AssessmentIndicatorGets()
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/Administration/AssessmentIndicator/Gets", RestSharp.Method.GET);
                var response = client.Execute(request);

                var result = JsonConvert.DeserializeObject<List<AssessmentIndicator>>(response.Content);
                return ApiResult<List<AssessmentIndicator>?>.Ok(result, result!.Count);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        [Route("/Admin/Administrator/AssessmentIndicator/Insert")]
        public async Task<IActionResult> AssessmentIndicatorInsert(AssessmentIndicator param)
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/Administration/AssessmentIndicator/Insert", RestSharp.Method.POST);

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

        [Route("/Admin/Administrator/AssessmentIndicator/Update")]
        public async Task<IActionResult> AssessmentIndicatorUpdate(AssessmentIndicator param)
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/Administration/AssessmentIndicator/Update", RestSharp.Method.POST);
                request.AddJsonParameter(param);
                var response = client.Execute(request);
                return ApiResult<object?>.Ok(response.Content);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        [Route("/Admin/Administrator/AssessmentIndicator/Update")]
        public async Task<IActionResult> AssessmentIndicatorDelete(AssessmentIndicator param)
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/Administration/AssessmentIndicator/Delete", RestSharp.Method.POST);
                request.AddJsonParameter(param);
                var response = client.Execute(request);
                return ApiResult<object?>.Ok(response.Content);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        public IActionResult Holiday()
        {
            ViewData["Title"] = "Holiday";
            return View();
        }
    }
}
