using JuniorWeb.Api.DTO;
using JuniorWeb.Api.Errors;
using JuniorWeb.DetaAccess;
using JuniorWeb.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JuniorWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DiagnosesController : ControllerBase
    {
        public JuniorWebDbContext _context { get; set; }
        public IExceptionLogger _logger { get; set; }

        public DiagnosesController(JuniorWebDbContext ctx, IExceptionLogger log)
        {
            _context = ctx;
            _logger = log;
        }
        private IActionResult HandleException(Exception ex)
        {

            var guid = _logger.LogError(ex);

            return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = "There was an error processing your request. Please contact support with this identifier: " + guid });
        }



        // GET: api/<DiagnosesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DiagnosesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DiagnosesController>
        [HttpPost]
        public IActionResult Post([FromBody] AddDiagnosisDto dto)
        {
            try
            {
                var errors = new List<string>();

                if (string.IsNullOrEmpty(dto.Name))
                {
                    errors.Add("Name is required");

                }
                if (dto.DiagnosedAt < DateTime.Now)
                {
                    errors.Add("Diagnosed time cant be before now");
                }
                if (dto.PatientId <= 0)
                {
                    errors.Add("Patient id cant be 0 or less");
                }
                if (dto.DoctorId <= 0)
                {
                    errors.Add("Doctor id cant be 0 or less");
                }

                if (errors.Any())
                {
                    return UnprocessableEntity(errors);
                }

                var diagnosis = new Diagnosis
                {
                    DoctorId = dto.DoctorId,
                    PatientId = dto.PatientId,
                    Name = dto.Name,
                    Description = dto.Description,
                    DiagnosedAt = dto.DiagnosedAt

                };

                _context.Diagnoses.Add(diagnosis);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // PUT api/<DiagnosesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<DiagnosesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
