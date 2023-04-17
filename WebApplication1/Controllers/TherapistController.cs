using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.DTO;
using ClassLibrary2;

namespace WebApplication1.Controllers
{
    public class TherapistController : ApiController
    {
        // GET: api/Therapist
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Therapist/5
        [HttpGet]
        [Route("api/Therapist/{id}")]
        public List<TherapistDTO> Get(string id)
        {
            safePlaceDbContext db = new safePlaceDbContext();
            List <TherapistDTO> listMeeting = db.TblTreats.Where(a => a.Therapist_Id == id && a.TblTreatment.Treatment_Date == DateTime.Today)
            .Select(x => new TherapistDTO
            {
                Therapist_Id = x.Therapist_Id,
                FirstName = x.TblTherapist.FirstName,
                LastName = x.TblTherapist.LastName,
                Treatment_Date = x.TblTreatment.Treatment_Date,
                StartTime = x.TblTreatment.StartTime,
                EndTime = x.TblTreatment.EndTime,
                Room_Num = x.TblTreatment.Room_Num,
                WasDone = x.TblTreatment.WasDone,
                PatientFirstName = x.TblUser.TblPatient.FirstName,
                PatientLastName = x.TblUser.TblPatient.LastName,
                Treatment_Id = x.TblTreatment.Treatment_Id
            }).ToList();

            if (listMeeting.Any())
            {
                return listMeeting;
            }
            else
            {
                return null;
            }
        }

        // POST: api/Therapist
        public void Post([FromBody]string value)
        {
            
        }

        // PUT: api/Therapist/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Therapist/5
        public void Delete(int id)
        {
        }
    }
}
