using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.DTO
{
    public class TherapistDTO
    {
        public string Therapist_Id;
        public string FirstName;
        public string LastName;
        public DateTime Treatment_Date;
        public int StartTime;
        public int EndTime;
        public string WasDone;
        public int Room_Num;
        public string PatientFirstName;
        public string PatientLastName;
        public int Treatment_Id;
    }
}