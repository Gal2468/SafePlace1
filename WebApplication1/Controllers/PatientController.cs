using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClassLibrary2;
using WebApplication1.DTO;

namespace WebApplication1.Controllers
{
    public class PatientController : ApiController
    {
        // GET: api/Patient
        [HttpGet]
        [Route("api/patient")]
        public List<PatientDTO> Get()
        {
            safePlaceDbContext db = new safePlaceDbContext();
            List<PatientDTO> patients = db.TblPatients.Select(p => new PatientDTO()
            {
                patientId = p.Patient_Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.TblUser.Email,
                Age = DateTime.Now.Year - p.BirthDate.Year,
                NumTreatments = p.TblUser.TblTreats.Count(),
                phoneNumber = p.PhoneNumber
            }).ToList();

            return patients;
        }

        [HttpGet]
        [Route("api/patient/{therapistId}")]
        public IHttpActionResult GetPatientsByTherapistId(string therapistId)
        {
            try
            {
                safePlaceDbContext db = new safePlaceDbContext();
                List<PatientDTO> patients = db.TblPatients
                    .Where(p => p.TblUser.TblTreats.Any(t => t.Therapist_Id == therapistId))
                    .Select(p => new PatientDTO()
                    {
                        patientId = p.Patient_Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        TherapistFirstName = p.TblUser.TblTreats.FirstOrDefault(t => t.Therapist_Id == therapistId).TblTherapist.FirstName,
                        TherapistLastName = p.TblUser.TblTreats.FirstOrDefault(t => t.Therapist_Id == therapistId).TblTherapist.LastName
                    })
                    .ToList();

                return Ok(patients);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
            
        }

        [HttpGet]
        [Route("api/patientCard/{patientId}")]
        public IHttpActionResult GetPatientByPatienttId(string patientId)
        {
            try
            {
                safePlaceDbContext db = new safePlaceDbContext();
                PatientDTO patient = db.TblPatients
                    .Where(p => p.Patient_Id == patientId)
                    .Select(p => new PatientDTO()
                    {
                        patientId = p.Patient_Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Email = p.TblUser.Email,
                        Age = DateTime.Now.Year - p.BirthDate.Year,
                        NumTreatments = p.TblUser.TblTreats.Count(),
                        phoneNumber = p.PhoneNumber
                    })
                    .SingleOrDefault();

                return Ok (patient);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("api/patientstreatment/{id}")]
        public List<TreatmentDto> GetAllPatientTreatments(string id)
        {
            safePlaceDbContext db = new safePlaceDbContext();

            List<TreatmentDto> treatment = db.TblTreatments.Where(o => o.TblTreats.Any(y => y.Patient_Id == id)).Where(c => c.TreatmentDate > DateTime.Today).
                Select(p => new TreatmentDto()
                {
                    Treatment_Id = p.Treatment_Id,
                    WasDone = p.WasDone,
                    TType_Id = p.TType_Id,
                    Room_Num = p.Room_Num,
                    TreatmentDate = (DateTime)p.TreatmentDate,
                    StartTime = (DateTime)p.StartTime,
                    EndTime = (DateTime)p.EndTime
                }).ToList();

            return treatment;
        }

        // POST: api/Patient
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Patient/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Patient/5
        public void Delete(int id)
        {
        }
    }
}
