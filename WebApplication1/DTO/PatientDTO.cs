using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClassLibrary2;

namespace WebApplication1.DTO
{
    public class PatientDTO
    {
        public string patientId;
        public string FirstName;
        public string LastName;
        public string Email;
        public int Age;
        public int NumTreatments;
        public string phoneNumber;
        public string TherapistFirstName;
        public string TherapistLastName;
    }
}