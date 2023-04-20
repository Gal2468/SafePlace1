using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.DTO
{
    public class DecryptUserDTO
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public string Patient_Id { get; set; }
        public int issuperuser { get; set; }
    }
}