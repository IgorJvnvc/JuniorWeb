using JuniorWeb.Api.DTO;
using JuniorWeb.Api.DTO.Searches;
using JuniorWeb.Api.Errors;
using JuniorWeb.DetaAccess;
using JuniorWeb.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JuniorWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        public JuniorWebDbContext _context { get; set; }
        public IExceptionLogger _logger { get; set; }

        public PatientController(JuniorWebDbContext ctx, IExceptionLogger log)
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
        
        public IActionResult Get([FromQuery] SearchPatientDto search)
        {
            try
            {
                var query = _context.Patients.Include(x => x.Hospital)
                        .ThenInclude(x => x.Rooms)
                        .Include(x => x.Diagnosis)
                        .ThenInclude(x => x.Doctors).ToList();

                var keySearch = search.Keyword;
                var diagnosis = search.Diagnosis;

                if (!string.IsNullOrEmpty(keySearch) && !string.IsNullOrEmpty(diagnosis))
                {
                    var key = keySearch.ToLower();
                    var dia = diagnosis.ToLower();

                    query = query.Where(x =>
                            x.Diagnosis != null &&
                            x.Diagnosis.Any(d => d.Name != null && d.Name.ToLower().Contains(diagnosis)) &&
                            (
                            (x.FirstName != null && x.FirstName.ToLower().Contains(key)) ||
                            (x.LastName != null && x.LastName.ToLower().Contains(key)) ||
                            (x.Hospital != null && x.Hospital.Name != null && x.Hospital.Name.ToLower().Contains(key)) ||
                            (x.Room != null && x.Room.ForWhatTypeOfPatient != null && x.Room.ForWhatTypeOfPatient.ToLower().Contains(key)) ||
                            (x.Doctor != null && x.Doctor.FristName != null && x.Doctor.FristName.ToLower().Contains(key)) ||
                            (x.Doctor != null && x.Doctor.LastName != null && x.Doctor.LastName.ToLower().Contains(key))
                            )).ToList();

                    //pokriva search svih mogucih kombinacija + dijagnoza
                }
                var resault = query.Select(x => new PatientDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Id = x.Id,
                    Diagnoses = x.Diagnosis.Select(x => new DiagnosesDto
                    {
                        Name = x.Name
                    })

                }).ToList();

                return Ok(resault);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PatientController>
        [HttpPost]
        public IActionResult Post([FromBody] CreatePatientDto dto)
        {
            try
            {
                var errors = new List<string>();



                if (dto.Dignosis == "COVID19")
                {
                    var hospitalIds = _context.Hospitals.Where(h => h.Covid19 == true)
                                                        .Select(h => h.Id)
                                                        .ToList();

                    if (!hospitalIds.Contains(dto.HospitalId))
                    {
                        errors.Add("This hospital is not for covid patients");
                        return BadRequest(errors);
                    }
                    else
                    {
                        var roomNumbers = _context.Rooms
                                       .Where(r => r.HospitalId == dto.HospitalId && r.ForWhatTypeOfPatient == "COVID19")
                                       .Select(r => r.Number)
                                       .ToList();

                        if (!roomNumbers.Contains(dto.RoomNumber))
                        {
                            errors.Add("This room is not for covid patients");
                            return BadRequest(errors);
                        }
                    }
                    var count = _context.Doctors.Where(x => x.Diagnosiss.Count() < 5).Select(x => x.Id).ToList();
                    if (!count.Contains(dto.DoctorId))
                    {
                        errors.Add("This doctor has more than 5 patients");
                        return BadRequest(errors);
                    }
                    return Ok();
                }


                if (string.IsNullOrWhiteSpace(dto.FirstName))
                {
                    errors.Add("First Name is required");
                }
                if (string.IsNullOrWhiteSpace(dto.LastName))
                {
                    errors.Add("Last Name is required");
                }
                if (_context.Patients.Any(x => x.JMBG == dto.JMBG))
                {
                    errors.Add("Patient with this JMBG already exists");
                }
                if (dto.HospitalId <= 0)
                {
                    errors.Add("HospitalId can't be 0 or less than 0");
                }

                if (errors.Any())
                {
                    return UnprocessableEntity(errors);
                }

                //insert
                var patient = new Patient
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    JMBG = dto.JMBG,
                    HospitalId = dto.HospitalId,
                    RoomId = dto.RoomNumber
                };

                _context.Patients.Add(patient);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);

            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdatePatientDto update)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.Id == id);
            var diagnosis = _context.Diagnoses.FirstOrDefault(x => x.PatientId == id);

            var error = new List<string>();

            if (patient == null)
            {
                return NotFound("Patient not found");
            }

            if (update.RoomId != 0 && update.DoctorId != 0)
            {
                patient.FirstName = update.FirstName;
                patient.LastName = update.LastName;
                patient.RoomId = update.RoomId;
                patient.DoctorId = update.DoctorId;
            }
            else
            {
                error.Add("HospitalId,RoomId,DoctorId cant be 0");
                return BadRequest(error);
            }
            if (update.Diagnoses == null)
            {
                var number = _context.Diagnoses.Count(x => x.PatientId == id);
                error.Add("Please update patients diagnoses.Patient has " + number + " diagnoses and theri descriptions");
                return BadRequest(error);
            }
            else
            {
                foreach (var item in update.Diagnoses)
                {
                    diagnosis.Name = item.Name;
                    diagnosis.Description = item.Description;

                }
            }
            _context.SaveChanges();
            return Ok("Patient's information updated successfully");
        }


        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var patient = _context.Patients.Find(id);


                if (patient == null)
                {
                    return NotFound();
                }
                _context.Patients.Remove(patient);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }
    }
}
