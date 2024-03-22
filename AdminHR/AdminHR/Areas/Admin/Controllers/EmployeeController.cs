using CoreApp.Lib;
using CoreApp.Model.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Newtonsoft.Json;
using System.Configuration;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using CoreApp.Lib.Extension;
using Microsoft.Data.SqlClient.Server;
using System.Net;
using CoreApp.Lib.Helper;

namespace AdminHR.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private IConfiguration Configuration;
        public EmployeeController(IConfiguration config)
        {
            Configuration = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/Admin/Employee/Detail"), Route("/Admin/Employee/Detail/{id}")]
        public IActionResult EmployeeDetail(string id = "")
        {
            var listReligion = Enum.GetNames(typeof(Religion)).ToList<string>();
            ViewData["Title"] = string.IsNullOrEmpty(id) ? "Create New" : "Update Data";
            ViewData["Religions"] = listReligion;
            ViewData["EmployeeID"] = id;
            ViewData["IsEdit"] = string.IsNullOrEmpty(id) ?  false : true;
            return View();
        }

        public async Task<IActionResult> Gets()
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/Employee/Gets", RestSharp.Method.GET);
                var response = client.Execute(request);

                var result = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
                return ApiResult<List<Employee>?>.Ok(result, result!.Count);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }            
        }

        [Route("/Admin/Employee/Get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var result = getEmployee(id);
                return ApiResult<Employee?>.Ok(result);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        private Employee getEmployee(string employeeID)
        {
            var client = new Client(Configuration);
            var request = new Request($"api/Employee/Get/{employeeID}", RestSharp.Method.GET);
            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<Employee>(response.Content);
            return result!;
        }

        public async Task<IActionResult> Insert(Employee param)
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/Employee/Insert", RestSharp.Method.POST);
                request.AddJsonParameter(param);
                var response = client.Execute(request);
                return ApiResult<object?>.Ok(response.Content);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        public async Task<IActionResult> Update(Employee param)
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/Employee/Update", RestSharp.Method.POST);
                request.AddJsonParameter(param);
                var response = client.Execute(request);
                return ApiResult<object?>.Ok(response.Content);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        public async Task<IActionResult> Delete(Employee param)
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/Employee/Delete", RestSharp.Method.POST);
                request.AddJsonParameter(param);
                var response = client.Execute(request);
                return ApiResult<object?>.Ok(response.Content);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }

        }

        [Route("/Admin/Employee/Family/Get/{id}")]
        public async Task<IActionResult> GetFamily(string id)
        {
            try
            {
                var result = getFamilyMember(id);
                return ApiResult<List<FamilyMember>>.Ok(result);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        private List<FamilyMember> getFamilyMember(string employeeID)
        {
            var client = new Client(Configuration);
            var request = new Request($"api/Employee/Family/Gets/{employeeID}", RestSharp.Method.GET);
            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<List<FamilyMember>>(response.Content);
            return result!;
        }

        [Route("/Admin/Employee/Family/insert")]
        public async Task<IActionResult> FamilyInsert(FamilyMember param)
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/Employee/Family/Insert", RestSharp.Method.POST);

                param.Id = GenerateAny.GenerateIdByDateTime(param.Id);
                param.UpdateDate = DateTime.Now;
                request.AddJsonParameter(param);
                var response = client.Execute(request);
                return ApiResult<object?>.Ok(response.Content);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        [Route("/Admin/Employee/Family/Update")]
        public async Task<IActionResult> FamiilyUpdate(FamilyMember param)
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/Employee/Family/Update", RestSharp.Method.POST);
                request.AddJsonParameter(param);
                var response = client.Execute(request);
                return ApiResult<object?>.Ok(response.Content);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        public async Task<IActionResult> GetEducation(string id)
        {
            try
            {
                var result = getEducation(id);
                return ApiResult<List<Education>?>.Ok(result);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        private List<Education> getEducation(string employeeID)
        {
            var client = new Client(Configuration);
            var request = new Request($"api/Employee/Education/Gets/{employeeID}", RestSharp.Method.GET);
            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<List<Education>>(response.Content);
            return result!;
        }

        [Route("/Admin/Employee/Education/Insert")]
        public async Task<IActionResult> EducationInsert(Education param)
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/Employee/Education/Insert", RestSharp.Method.POST);

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

        [Route("/Admin/Employee/Education/Update")]
        public async Task<IActionResult> EducationUpdate(Education param)
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/Employee/Education/Update", RestSharp.Method.POST);
                request.AddJsonParameter(param);
                var response = client.Execute(request);
                return ApiResult<object?>.Ok(response.Content);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        [Route("/Admin/Employee/Education/Delete")]
        public async Task<IActionResult> EducationDelete(Education param)
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/Employee/Education/Delete", RestSharp.Method.POST);
                request.AddJsonParameter(param);
                var response = client.Execute(request);
                return ApiResult<object?>.Ok(response.Content);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        public async Task<IActionResult> GetWorkExperience(string id)
        {
            try
            {
                var result = getWorkExperience(id);
                return ApiResult<List<WorkExperience>?>.Ok(result);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        private List<WorkExperience> getWorkExperience(string employeeID)
        {
            var client = new Client(Configuration);
            var request = new Request($"api/Employee/WorkExperence/Gets/{employeeID}", RestSharp.Method.GET);
            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<List<WorkExperience>>(response.Content);
            return result!;
        }

        [Route("/Admin/Employee/WorkExperience/insert")]
        public async Task<IActionResult> WorkExperenceInsert(WorkExperience param)
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/Employee/WorkExperience/Insert", RestSharp.Method.POST);

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

        [Route("/Admin/Employee/WorkExperience/Update")]
        public async Task<IActionResult> WorkExperienceUpdate(WorkExperience param)
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/Employee/WorkExperience/Update", RestSharp.Method.POST);
                request.AddJsonParameter(param);
                var response = client.Execute(request);
                return ApiResult<object?>.Ok(response.Content);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        public IActionResult Attendance()
        {
            return View();
        }
    }
}
