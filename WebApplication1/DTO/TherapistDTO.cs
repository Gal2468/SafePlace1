﻿using System;
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
        public Nullable<System.DateTime> Treatment_Date;
        public Nullable<System.DateTime> StartTime;
        public Nullable<System.DateTime> EndTime;
        public string WasDone;
        public int Room_Num;
        public string PatientFirstName;
        public string PatientLastName;
        public int Treatment_Id;
    }
}