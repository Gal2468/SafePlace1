using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.DTO
{
    public class TreatssDto
    {
        public string Patient_Id { get; set; }
        public int Treatment_Id { get; set; }
        public string Therapist_Id { get; set; }

        public List<TreatmentDto> TreatmentsList;
    }
}