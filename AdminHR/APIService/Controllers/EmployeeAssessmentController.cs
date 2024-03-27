using CoreApp.DAL;
using CoreApp.Lib.Helper;
using CoreApp.Model.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAssessmentController : ControllerBase
    {
        private readonly DBContext _dbContext;
        public EmployeeAssessmentController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("Gets")]
        public List<EmployeeAssessment> Gets()
        {
            //Employee emp = new Employee(_dbContext);
            List<EmployeeAssessment> list = _dbContext.EmployeeAssessments.ToList();
            return list;
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<EmployeeAssessment>> Get(string id)
        {
            if (_dbContext.EmployeeAssessments == null)
            {
                return NotFound();
            }
            var emp = _dbContext.EmployeeAssessments.Where(x => x.EmployeeID == id).FirstOrDefault();
            if (emp == null)
            {
                return NotFound();
            }
            return emp;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert(EmployeeAssessment param)
        {
            try
            {
                param.Id = GenerateAny.GenerateIdByDateTime(param.Id);
                _dbContext.EmployeeAssessments.Add(param);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }

            return Ok("Data has been saved");
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(EmployeeAssessment param)
        {
            try
            {
                _dbContext.Entry(param).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }

            return Ok("Data has been updated");
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(EmployeeAssessment param)
        {
            try
            {
                _dbContext.EmployeeAssessments.Remove(param);
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
