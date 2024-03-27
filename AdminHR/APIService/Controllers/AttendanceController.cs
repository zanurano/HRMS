using CoreApp.DAL;
using CoreApp.Lib;
using CoreApp.Lib.Helper;
using CoreApp.Model.Attendance;
using CoreApp.Model.Employee;
using CoreApp.Model.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly DBContext _dbContext;
        public AttendanceController(DBContext dbContext)
        {
            _dbContext = dbContext;

        }

        [HttpGet("Gets")]
        public List<Attendance> Gets()
        {
            List<Attendance> resList = _dbContext.Attendances.ToList();
            return resList;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert(Attendance param)
        {
            try
            {
                param.Id = GenerateAny.GenerateIdByDateTime(param.Id);
                param.UpdatedBy = "SYSTEM";
                param.UpdatedDate = DateTime.Now;
                _dbContext.Attendances.Add(param);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                //throw new Exception(err.Message);
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error :\n{err.Message}");
            }

            return ApiResult<object>.Ok(param, "Data has been saved");
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(Attendance param)
        {
            try
            {
                _dbContext.Entry(param).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                //throw new Exception(err.Message);
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error :\n{err.Message}");
            }

            return ApiResult<object>.Ok(param, "Data has been updated");
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(Attendance param)
        {
            try
            {
                _dbContext.Attendances.Remove(param);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                //throw new Exception(err.Message);
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error :\n{err.Message}");
            }

            return ApiResult<Object>.Ok(param, "Data has been deleted");
        }

        [HttpGet("EmployeeAttendance/Gets/{period}")]
        public List<EmployeeAttendanceViewModel> EmployeeAttendanceGets(string period)
        {
            //var result = from person in _dbContext.Employees
            //             join detail in _dbContext.Attendances on person.EmployeeID equals detail.EmployeeID
            //             into Details
            //             from defaultVal in Details.DefaultIfEmpty().GroupBy(x=>x.EmployeeID).ToList()
            //             select new
            //             {
            //                 EmployeeID = person.EmployeeID,
            //                 EmployeeName = person.FullName,
            //             };

            //var group = _dbContext.Attendances.ToList()
            //    .AsEnumerable()
            //    .GroupBy(t => new { t.EmployeeID }).ToList();

            var res = AttendanceCombines(period);
            return res;
            //return ApiResult<List<EmployeeAttendanceViewModel>>.Ok(res, res.Count);

        }

        private List<EmployeeAttendanceViewModel> AttendanceCombines(string period)
        {
            var firstDayOfMonth = new DateTime(2024, 3, 1);

            var year = firstDayOfMonth.Year;
            var month = firstDayOfMonth.Month;
            List<DateTime> daysOfMonth = Enumerable.Range(1, DateTime.DaysInMonth(year, month)).Select(day => new DateTime(year, month, day)).ToList();

            var queryresult = (from emp in _dbContext.Employees.ToList()
                from t in daysOfMonth
                select new
                {
                    EmpID = emp.EmployeeID,
                    Name = emp.FullName,
                    Date = t.Date,
                    Status = (
                        _dbContext.Holidays.Any(h => h.OnDate.Date == t.Date) ? "H" : // holiday  
                        //_dbContext.EmployeeLeaves.Any(el => el.Empid == emp.aid && t.Date >= el.StartDate && t.Date <= el.EndDate) ? "L" : //leave  
                        _dbContext.Attendances.Any(ea => ea.EmployeeID == emp.EmployeeID && ea.ClockIn.Date == t.Date) ? "P" : // persent  
                        "A" //absent  
                    )
                }).ToList();

            //Then, we could use the following code convert result to your required  
            var result = queryresult.GroupBy(c => c.EmpID).Select(g =>
            {
                var empvm = new EmployeeAttendanceViewModel();
                empvm.EmployeeID = g.Key;
                empvm.FullName = g?.FirstOrDefault()?.Name;
                empvm.Attendances = new Dictionary<DateTime, string>();
                foreach (var i in g)
                    empvm.Attendances.Add(i.Date, i.Status);
                return empvm;
            }).ToList();

            return result;
        }

        private List<EmployeeAttendanceViewModel> AttendanceCombines2()
        {
            var firstDayOfMonth = new DateTime(2024, 3, 1);

            var year = firstDayOfMonth.Year;
            var month = firstDayOfMonth.Month;
            List<DateTime> daysOfMonth = Enumerable.Range(1, DateTime.DaysInMonth(year, month)).Select(day => new DateTime(year, month, day)).ToList();

            var queryresult = (from emp in _dbContext.Employees.ToList()
                from t in daysOfMonth
                select new 
                {
                    EmployeeID = emp.EmployeeID,
                    EmployeeName = emp.FullName,
                    Date = t.Date,
                    Status = (
                        _dbContext.Holidays.Any(h => h.OnDate == t.Date) ? "H" : // holiday  
                        //_dbcontext.EmployeeLeaves.Any(el => el.Empid == emp.aid && t.Date >= el.StartDate && t.Date <= el.EndDate) ? "L" : //leave  
                        _dbContext.Attendances.Any(ea => ea.EmployeeID == emp.EmployeeID && ea.ClockIn.Date == t.Date) ? "P" : // persent  
                        "A" //absent  
                    )
                }).ToList();

            //Then, we could use the following code convert result to your required  
            var result = queryresult.GroupBy(c => c.EmployeeID).Select(g =>
            {
                var empvm = new EmployeeAttendanceViewModel();
                empvm.EmployeeID = g.Key;
                empvm.FullName = g?.FirstOrDefault()?.EmployeeName;
                empvm.Attendances = new Dictionary<DateTime, string>();
                foreach (var i in g)
                    empvm.Attendances.Add(i.Date, i.Status);
                return empvm;
            }).ToList();

            return result;
        }
    }
}
