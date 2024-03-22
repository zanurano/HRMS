﻿using CoreApp.DAL;
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
                throw new Exception(err.Message);
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
                throw new Exception(err.Message);
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
                throw new Exception(err.Message);
            }

            return Ok("Data has been deleted");
        }

        [HttpGet("AssessmentIndicator/Gets")]
        public List<AssessmentIndicator> AssessmentIndicatorGets()
        {
            List<AssessmentIndicator> resList = _dbContext.AssessmentIndicators.ToList();
            return resList;
        }

        [HttpPost("AssessmentIndicator/Insert")]
        public async Task<IActionResult> EmployeeInsert(AssessmentIndicator assessmentIndicator)
        {
            try
            {;
                _dbContext.AssessmentIndicators.Add(assessmentIndicator);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
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
                throw new Exception(err.Message);
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
                throw new Exception(err.Message);
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
                _dbContext.Holidays.Add(param);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }

            return Ok("Data has been saved");
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
                throw new Exception(err.Message);
            }

            return Ok("Data has been updated");
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
                throw new Exception(err.Message);
            }

            return Ok("Data has been deleted");
        }
    }
}