using CoreApp.DAL;
using CoreApp.Lib.Helper;
using CoreApp.Model.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DBContext _dbContext;
        public EmployeeController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<EmployeeController>
        [HttpGet("Gets")]
        public List<Employee> Gets()
        {
            //Employee emp = new Employee(_dbContext);
            List<Employee> empList = _dbContext.Employees.ToList();
            return empList;
        }

        // GET api/<EmployeeController>/5
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<Employee>> Get(string id)
        {
            if (_dbContext.Employees == null)
            {
                return NotFound();
            }
            var emp = _dbContext.Employees.Where(x => x.EmployeeID == id).FirstOrDefault();
            if (emp == null)
            {
                return NotFound();
            }
            return emp;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> EmployeeInsert(Employee employee)
        {
            try {
                employee.UpdateDate = DateTime.Now;
                _dbContext.Employees.Add(employee);
                await _dbContext.SaveChangesAsync();
            } catch (Exception err)
            {
                throw new Exception(err.Message);
            }

            return Ok("Employee has been saved");
        }

        [HttpPost("Update")]
        public async Task<IActionResult> EmployeeUpdate(Employee employee)
        {
            try
            {
                employee.UpdateDate = DateTime.Now;
                _dbContext.Entry(employee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            
            return Ok("Employee has been updated");
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> EmployeeDelete(Employee employee)
        {
            try
            {
                _dbContext.Employees.Remove(employee);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            
            return Ok("Employee has been deleted");
        }

        [HttpGet("Education/Gets/{id}")]
        public List<Education> EducationGets(string id)
        {
            List<Education> educations = _dbContext.Educations.Where(x => x.EmployeeID == id).ToList();
            return educations;
        }

        [HttpGet("Education/Get/{id}")]
        public async Task<ActionResult<Education>> EducationGet(string id)
        {
            if (_dbContext.Educations == null)
            {
                return NotFound();
            }
            var res = _dbContext.Educations.Where(x => x.Id == id).FirstOrDefault();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }
        [HttpPost("Education/Insert")]
        public async Task<IActionResult> EducationInsert(Education education)
        {
            try
            {
                if(string.IsNullOrEmpty(education.Id))
                {
                    education.Id = DateTime.Now.Ticks.ToString();
                }
                education.UpdateDate = DateTime.Now;
                _dbContext.Educations.Add(education);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }

            return Ok("Data has been saved");
        }

        [HttpPost("Education/Update")]
        public async Task<IActionResult> EducationeUpdate(Education education)
        {
            try
            {
                education.UpdateDate = DateTime.Now;
                _dbContext.Entry(education).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }

            return Ok("Data has been updated");
        }

        [HttpPost("Education/Delete")]
        public async Task<IActionResult> EducationDelete(Education education)
        {
            try
            {
                _dbContext.Educations.Remove(education);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }

            return Ok("Data has been deleted");
        }

        [HttpGet("WorkExperence/Gets/{id}")]
        public List<WorkExperience> WorkExperienceGets(string id)
        {
            List<WorkExperience> workExperiences = _dbContext.WorkExperiences.Where(x => x.EmployeeID == id).ToList();
            return workExperiences;
        }

        [HttpGet("WorkExperence/Get/{id}")]
        public async Task<ActionResult<WorkExperience>> WorkExperienceGet(string id)
        {
            if (_dbContext.WorkExperiences == null)
            {
                return NotFound();
            }
            var res = _dbContext.WorkExperiences.Where(x => x.Id == id).FirstOrDefault();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }
        [HttpPost("WorkExperience/Insert")]
        public async Task<IActionResult> WorkExperienceInsert(WorkExperience workExperience)
        {
            try
            {
                if (string.IsNullOrEmpty(workExperience.Id))
                {
                    workExperience.Id = DateTime.Now.Ticks.ToString();
                }
                _dbContext.WorkExperiences.Add(workExperience);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }

            return Ok("Data has been saved");
        }

        [HttpPost("WorkExperience/Update")]
        public async Task<IActionResult> WorkExperienceUpdate(WorkExperience workExperience)
        {
            try
            {
                _dbContext.Entry(workExperience).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }

            return Ok("Data has been updated");
        }

        [HttpPost("WorkExperience/Delete")]
        public async Task<IActionResult> WorkExperienceDelete(WorkExperience workExperience)
        {
            try
            {
                _dbContext.WorkExperiences.Remove(workExperience);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }

            return Ok("Data has been deleted");
        }

        [HttpGet("Family/Gets/{id}")]
        public List<FamilyMember> FamilyMemberGets(string id)
        {
            List<FamilyMember> familyMembers = _dbContext.FamilyMembers.Where(x=> x.EmployeeID == id).ToList();
            return familyMembers;
        }

        [HttpGet("Family/Get/{id}")]
        public async Task<ActionResult<FamilyMember>> FamilyMemberGet(string id)
        {
            if (_dbContext.FamilyMembers == null)
            {
                return NotFound();
            }
            var res = _dbContext.FamilyMembers.Where(x => x.Id == id).FirstOrDefault();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        [HttpPost("Family/Insert")]
        public async Task<IActionResult> FamilyMemberInsert(FamilyMember familyMember)
        {
            try
            {
                familyMember.Id = GenerateAny.GenerateIdByDateTime(familyMember.Id);
                familyMember.UpdateDate = DateTime.Now;
                _dbContext.FamilyMembers.Add(familyMember);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }

            return Ok("Data has been saved");
        }

        [HttpPost("Family/Update")]
        public async Task<IActionResult> FamilyMemberUpdate(FamilyMember familyMember)
        {
            try
            {
                familyMember.UpdateDate = DateTime.Now;
                _dbContext.Entry(familyMember).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }

            return Ok("Data has been updated");
        }

        [HttpPost("Family/Delete")]
        public async Task<IActionResult> FamilyMemberDelete(FamilyMember familyMember)
        {
            try
            {
                _dbContext.FamilyMembers.Remove(familyMember);
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
