using CoreApp.Lib.Extension;
using CoreApp.Lib;
using CoreApp.Model.Administration;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Configuration;
using System.Net;
using CoreApp.Model.Attendance;
using CoreApp.Lib.Helper;
using System.Data;
using CoreApp.Model.Employee;
using CoreApp.Model.ViewModel;

namespace AdminHR.Areas.Admin.Controllers
{ 
    [Area("Admin")]
    public class AttendanceController : Controller
    {
        private IConfiguration Configuration;
        public AttendanceController(IConfiguration config)
        {
            Configuration = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/Admin/Attendance/Gets/Combine/{period}")]
        public async Task<IActionResult> AttendanceCombineGets(string period)
        {
            try
            {
                List<EmployeeAttendanceViewModel> employeeAttendance = EmployeeAttendance(period);
                List<Attendance> attendances = AttendanceGets();
                List<Holiday> holidays = HolidayGets();

                //var grouping1 = from p in employeeAttendance
                //    .Where(a => a.EmployeeID != null)
                //    .SelectMany(l => l.EmployeeID, (x, t) => new Attendance
                //    {
                //        EmployeeID = x.EmployeeID,
                //    }) select p;

                return ApiResult<object>.Ok(new { Attendances = employeeAttendance, Holiday = holidays });
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        private List<EmployeeAttendanceViewModel> EmployeeAttendance(string period) {
            var client = new Client(Configuration);
            var request = new Request($"api/Attendance/EmployeeAttendance/Gets/{period}", RestSharp.Method.GET);
            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<List<EmployeeAttendanceViewModel>>(response.Content);
            return result!;
        }

        private List<Attendance> AttendanceGets()
        {
            var client = new Client(Configuration);
            var request = new Request($"api/Attendance/Gets", RestSharp.Method.GET);
            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<List<Attendance>>(response.Content);
            return result!;
        }

        private List<Holiday> HolidayGets()
        {
            var client = new Client(Configuration);
            var request = new Request($"api/Administration/Holiday/Gets", RestSharp.Method.GET);
            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<List<Holiday>>(response.Content);
            return result!;
        }

        [Route("/Admin/Attendance/Insert")]
        public async Task<IActionResult> AttendanceInsert(Attendance param)
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/Attendance/Insert", RestSharp.Method.POST);

                param.Id = GenerateAny.GenerateIdByDateTime(param.Id);
                param.UpdatedBy = "SYSTEM";
                param.UpdatedDate = DateTime.Now;
                request.AddJsonParameter(param);
                var response = client.Execute(request);
                var result = JsonConvert.DeserializeObject<ApiResult<object>.Result>(response.Content);
                return new ApiResult<object>(result);
                //return ApiResult<object?>.Ok(response.Content);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        [Route("/Admin/Attendance/Update")]
        public async Task<IActionResult> AttendanceUpdate(Attendance param)
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/Attendance/Update", RestSharp.Method.POST);

                param.Id = GenerateAny.GenerateIdByDateTime(param.Id);
                param.UpdatedBy = "SYSTEM";
                param.UpdatedDate = DateTime.Now;
                request.AddJsonParameter(param);
                var response = client.Execute(request);
                var result = JsonConvert.DeserializeObject<ApiResult<object>.Result>(response.Content);
                return new ApiResult<object>(result);
                //return ApiResult<object?>.Ok(response.Content);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

        [Route("/Admin/Attendance/Delete")]
        public async Task<IActionResult> AttendanceDelete(Attendance param)
        {
            try
            {
                var client = new Client(Configuration);
                var request = new Request($"api/Attendance/Delete", RestSharp.Method.POST);

                param.Id = GenerateAny.GenerateIdByDateTime(param.Id);
                param.UpdatedBy = "SYSTEM";
                param.UpdatedDate = DateTime.Now;
                request.AddJsonParameter(param);
                var response = client.Execute(request);
                var result = JsonConvert.DeserializeObject<ApiResult<object>.Result>(response.Content);


                return new ApiResult<object>(result);
                //return ApiResult<object?>.Ok(response.Content);
            }
            catch (Exception e)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error service :\n{e.Message}");
            }
        }

    }
}
