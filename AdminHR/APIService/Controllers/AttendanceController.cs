using CoreApp.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
