using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PR.API.Controllers
{

    // hhtps://localhost:portnumber/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentsNames = new string[] { "Jhon", "Jane", "Mark" };
            return Ok(studentsNames);
        }
    }
}
