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

        [HttpGet]
        [Route("api/patient/{therapistId}")]
        public IHttpActionResult GetPatientsByTherapistId(string therapistId)
        {
            try
            {
                SafePlaceDbContext db = new SafePlaceDbContext();
                List<PatientDTO> patients = db.TblPatients
                    .Where(p => p.TblTreats.Any(t => t.Therapist_Id == therapistId))
                    .Select(p => new PatientDTO()
                    {
                        patientId = p.Patient_Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        TherapistFirstName = p.TblTreats.FirstOrDefault(t => t.Therapist_Id == therapistId).TblTherapist.FirstName,
                        TherapistLastName = p.TblTreats.FirstOrDefault(t => t.Therapist_Id == therapistId).TblTherapist.LastName
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
                SafePlaceDbContext db = new SafePlaceDbContext();
                PatientDTO patient = db.TblPatients
                    .Where(p => p.Patient_Id == patientId)
                    .Select(p => new PatientDTO()
                    {
                        patientId = p.Patient_Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Email = p.Email, 
                        Age = DateTime.Now.Year - p.BirthDate.Value.Year,
                        NumTreatments = p.TblTreats.Count(),
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
            SafePlaceDbContext db = new SafePlaceDbContext();

            List<TreatmentDto> treatment = db.TblTreatments.Where(o => o.TblTreats.Any(y => y.Patient_Id == id)).Where(c => c.Treatment_Date > DateTime.Today).
                Select(p => new TreatmentDto()
                {
                    Treatment_Id = p.Treatment_Id,
                    WasDone = p.WasDone,
                    Type_Id = p.Type_Id,
                    Room_Num = p.Room_Num,
                    TreatmentDate = (DateTime)p.Treatment_Date,
                    StartTime = (DateTime)p.StartTime,
                    EndTime = (DateTime)p.EndTime
                }).ToList();

            return treatment;
        }


        [HttpGet]
        [Route("api/GetSummaryByDate/{PatientId}/{Date}")]
        public IHttpActionResult GetSummaryByNum(string PatientId, string date)
        {
            try
            {
                SafePlaceDbContext db = new SafePlaceDbContext();
                SummaryDTO Summary = db.TblSummaries.Where(a => a.Summary_Date.ToString().Substring(0, 10) == date && a.WrittenBy == "t" && a.TblTreatments.FirstOrDefault().TblTreats.FirstOrDefault().Patient_Id == PatientId).Select(x => new SummaryDTO()
                {
                    Summary_Num = x.Summary_Num,
                    WrittenBy = x.WrittenBy,
                    Summary_Date = x.Summary_Date.ToString().Substring(0, 10),
                    ImportanttoNote = x.ImportentToNote,
                    Content = x.Content,
                    StartTime = (DateTime)x.TblTreatments.FirstOrDefault().StartTime,
                    EndTime = (DateTime)x.TblTreatments.FirstOrDefault().EndTime,
                    FirstNameP = x.TblTreatments.FirstOrDefault().TblTreats.FirstOrDefault().TblPatient.FirstName,
                    LastNameP = x.TblTreatments.FirstOrDefault().TblTreats.FirstOrDefault().TblPatient.LastName
                }).FirstOrDefault();


                return Ok(Summary);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("api/GetAllSummary/{PatientId}")]
        public IHttpActionResult GetAllSummary(string PatientId)
        {
            try
            {
                SafePlaceDbContext db = new SafePlaceDbContext();
                List<SummaryDTO> allSummaries = db.TblTreats.Where(x => x.Patient_Id == PatientId)
                    .Select(s => new SummaryDTO()
                    {
                        Summary_Num = 0,
                        Summary_Date = s.TblTreatment.Treatment_Date.ToString().Substring(0, 10),
                        Patient_Id = s.TblPatient.Patient_Id
                    }).ToList();

                return Ok(allSummaries);

            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
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
