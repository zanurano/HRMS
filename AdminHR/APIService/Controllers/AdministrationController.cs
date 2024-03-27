using CoreApp.DAL;
using CoreApp.Lib.Extension;
using CoreApp.Lib;
using CoreApp.Model.Administration;
using CoreApp.Model.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using CoreApp.Model.Attendance;
using CoreApp.Lib.Helper;

namespace APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        private readonly DBContext _dbContext;
        public AdministrationController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("User/Gets")]
        public List<Users> UserGets()
        {
            List<Users> resList = _dbContext.Users.ToList();
            return resList;
        }

        [HttpPost("User/Insert")]
        public async Task<IActionResult> EmployeeInsert(Users param)
        {
            try
            {
                _dbContext.Users.Add(param);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error :\n{err.Message}");
            }

            return Ok("Data has been saved");
        }

        [HttpPost("User/Update")]
        public async Task<IActionResult> EmployeeUpdate(Users param)
        {
            try
            {
                _dbContext.Entry(param).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error :\n{err.Message}");
            }

            return Ok("Data has been updated");
        }

        [HttpPost("User/Delete")]
        public async Task<IActionResult> EmployeeDelete(Users param)
        {
            try
            {
                _dbContext.Users.Remove(param);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error :\n{err.Message}");
            }

            return Ok("Data has been deleted");
        }

        [HttpGet("AssessmentIndicator/Gets")]
        public List<AssessmentIndicator> AssessmentIndicatorGets()
        {
            List<AssessmentIndicator> resList = _dbContext.AssessmentIndicators.ToList();
            return resList;
        }

        [HttpGet("AssessmentIndicator/Getby/Position/{id}")]
        public async Task<ActionResult<AssessmentIndicator>> Get(string id)
        {
            if (_dbContext.AssessmentIndicators == null)
            {
                return NotFound();
            }
            var emp = _dbContext.AssessmentIndicators.Where(x => x.Position == id).FirstOrDefault();
            if (emp == null)
            {
                return NotFound();
            }
            return emp;
        }

        [HttpPost("AssessmentIndicator/Insert")]
        public async Task<IActionResult> EmployeeInsert(AssessmentIndicator assessmentIndicator)
        {
            try
            {
                assessmentIndicator.Id = GenerateAny.GenerateIdByDateTime(assessmentIndicator.Id);
                _dbContext.AssessmentIndicators.Add(assessmentIndicator);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                //throw new Exception(err.Message);
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error :\n{err.Message}");
            }

            return Ok("Data has been saved");
        }

        [HttpPost("AssessmentIndicator/Update")]
        public async Task<IActionResult> EmployeeUpdate(AssessmentIndicator assessmentIndicator)
        {
            try
            {
                _dbContext.Entry(assessmentIndicator).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                //throw new Exception(err.Message);
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error :\n{err.Message}");
            }

            return Ok("Data has been updated");
        }

        [HttpPost("AssessmentIndicator/Delete")]
        public async Task<IActionResult> EmployeeDelete(AssessmentIndicator assessmentIndicator)
        {
            try
            {
                _dbContext.AssessmentIndicators.Remove(assessmentIndicator);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error :\n{err.Message}");
            }

            return Ok("Data has been deleted");
        }

        [HttpGet("Holiday/Gets")]
        public List<Holiday> HolidayGets()
        {
            List<Holiday> resList = _dbContext.Holidays.ToList();
            return resList;
        }

        [HttpPost("Holiday/Insert")]
        public async Task<IActionResult> HolidayInsert(Holiday param)
        {
            try
            {
                param.Id = GenerateAny.GenerateIdByDateTime(param.Id);
                _dbContext.Holidays.Add(param);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error :\n{err.Message}");
            }

            return ApiResult<object>.Ok(param, "Data has been saved");
        }

        [HttpPost("Holiday/Update")]
        public async Task<IActionResult> HolidayUpdate(Holiday param)
        {
            try
            {
                _dbContext.Entry(param).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error :\n{err.Message}");
            }

            return ApiResult<object>.Ok(param, "Data has been updated");
        }

        [HttpPost("Holiday/Delete")]
        public async Task<IActionResult> HolidayDelete(Holiday param)
        {
            try
            {
                _dbContext.Holidays.Remove(param);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                return ApiResult<object>.Error(HttpStatusCode.BadRequest, $"Error :\n{err.Message}");
            }

            //return Ok("Data has been deleted");
            return ApiResult<object>.Ok(param, "Data has been saved");
        }
    }
}
