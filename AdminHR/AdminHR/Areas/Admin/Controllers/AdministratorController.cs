using CoreApp.Lib.Extension;
using CoreApp.Lib;
using CoreApp.Model.Administration;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Net;
using CoreApp.Model.Employee;
using Newtonsoft.Json;
using CoreApp.Lib.Helper;
using CoreApp.Model.Attendance;

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

        [Route("/Admin/Administrator/AssessmentIndicator/Delete")]
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

        [Route("/Admin/Administrator/Holiday/Gets")]
        public async Task<IActionResult> HolidayGets()
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/Administration/Holiday/Gets", RestSharp.Method.GET);
                var response = client.Execute(request);

                var result = JsonConvert.DeserializeObject<List<Holiday>>(response.Content);
                return ApiResult<List<Holiday>?>.Ok(result, result!.Count);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        [Route("/Admin/Administrator/Holiday/Insert")]
        public async Task<IActionResult> HolidayInsert(Holiday param)
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/Administration/Holiday/Insert", RestSharp.Method.POST);

                param.Id = GenerateAny.GenerateIdByDateTime(param.Id);
                request.AddJsonParameter(param);
                var response = client.Execute(request);
                var result = JsonConvert.DeserializeObject<ApiResult<Holiday>.Result>(response.Content);
                return new ApiResult<Holiday?>(result!);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        [Route("/Admin/Administrator/Holiday/Update")]
        public async Task<IActionResult> HolidayUpdate(Holiday param)
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/Administration/Holiday/Update", RestSharp.Method.POST);
                request.AddJsonParameter(param);
                var response = client.Execute(request);
                var result = JsonConvert.DeserializeObject<ApiResult<Holiday>.Result>(response.Content);
                return new ApiResult<Holiday?>(result!);
                //return ApiResult<object?>.Ok(response.Content);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        [Route("/Admin/Administrator/Holiday/Delete")]
        public async Task<IActionResult> HolidayDelete(Holiday param)
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/Administration/Holiday/Delete", RestSharp.Method.POST);
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
