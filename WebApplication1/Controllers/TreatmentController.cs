﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
//using ClassLibrary2;

//namespace WebApplication1.Controllers
//{
//    public class TreatmentController : ApiController
//    {

//        [HttpGet]
//        [Route("api/amen/{id}")]
//        public IHttpActionResult Getfreetreat(string id)
//        {
//            string[] hours = { "10:00", "14:00", "16:00", "20:00" }; ///when therapist is avialble 
//            DateTime daytemp = DateTime.Today; //the day of the treatment 
//            //DateTime start = daytemp.Date.AddHours(8); //Our earliest appoinment (8:00)
//            //DateTime end = daytemp.Date.AddHours(-1); //Our latest appointment (23:00)
//            SafePlaceDbContext db = new SafePlaceDbContext();

//            List<TblTreatment> treatsbyday = db.TblTreatments.Where(o => o.TreatmentDate == daytemp).ToList();
//            List<TblTreatment> treatsbydayandther = treatsbyday.Where(y => y.TblTreats.Any(c => c.Therapist_Id == id)).ToList();
//            List<TblTreatment> room1 = treatsbyday.Where(u => u.Room_Num == 1).ToList(); //all treatments happenning TODAY in room 1
//            List<TblTreatment> room2 = treatsbyday.Where(u => u.Room_Num == 2).ToList(); //all treatments happenning TODAY in room 2

//            var lis = new Dictionary<int, string>();

//            foreach (var treatment in treatsbydayandther)
//            {
//                DateTime temp = (DateTime)treatment.StartTime;
//                string dem = temp.ToShortTimeString();

//                lis[treatment.Treatment_Id] = dem; //Dictionary ordered by Treatment number- the value is the string of the hour
//            } //Insets hours to dictionary. (Therapist+Day) Based on Treatment Id
//            foreach (var tre in lis)
//            {
//                for (int i = 0; i < hours.Length; i++)
//                {
//                    if (tre.Value == hours[i])
//                    {
//                        hours[i] = "Taken";
//                    }
//                }
//            } //Checks if any of the hours are taken based on theapist+day
//            foreach (var r1 in room1)
//            {
//                DateTime t = (DateTime)r1.StartTime;
//                string r1time = t.ToShortTimeString(); //a string of all hours hapenning in room 1 for given day

//                for (int i = 0; i < hours.Length; i++)
//                {
//                    if (hours[i] == "Taken")
//                    {
//                        break;
//                    }

//                    if (hours[i] == r1time)
//                    {
//                        hours[i] = "Taken";
//                    }
//                }

//            } //Checks if any of the hours are taken based on Room1 and day
//            foreach (var r2 in room2)
//            {
//                DateTime t = (DateTime)r2.StartTime;
//                string r2time = t.ToShortTimeString(); //a string of all hours hapenning in room 1 for given day

//                for (int i = 0; i < hours.Length; i++)
//                {
//                    if (hours[i] == "Taken")
//                    {
//                        break;
//                    }

//                    if (hours[i] == r2time)
//                    {
//                        hours[i] = "Taken";
//                    }
//                }

//            } //Checks if any of the hours are taken based on Room2 and day

//            return Ok(hours);


//            /////***NEED TO ADD: End times
//        }

//        // GET: api/Treatment/5
//        public string Get(int id)
//        {
//            return "value";
//        }

//        // POST: api/Treatment
//        public void Post([FromBody] TblTreatment value)
//        {
//            SafePlaceDbContext db = new SafePlaceDbContext();
//            int temp = db.TblTreatments.Max(o => o.Treatment_Id) + 1;

//            try
//            {
//                TblTreatment trea = new TblTreatment();

//                trea.Treatment_Id = temp;
//                trea.TreatmentDate = value.TreatmentDate;
//                trea.WasDone = value.WasDone;
//                trea.StartTime = value.StartTime;
//                trea.EndTime = value.EndTime;
//                trea.Room_Num = value.Room_Num;
//                trea.TType_Id = value.TType_Id;

//                db.TblTreatments.Add(trea);
//                db.SaveChanges();
//                Console.WriteLine("YAY");
//            }
//            catch (Exception e)
//            {
//                throw (e);
//            }

//        }

//        // PUT: api/Treatment/5
//        public void Put(int id, [FromBody] string value)
//        {
//        }

//        [HttpPut]
//        [Route("api/temp")]
//        // PUT: api/Treatment/5
//        public void Put()
//        {
//            SafePlaceDbContext db = new SafePlaceDbContext();

//            try
//            {
//                TblTreatment trea = new TblTreatment();

//                trea.Treatment_Id = 7;
//                trea.Treatment_Date = DateTime.Today;
//                trea.WasDone = "n";
//                trea.StartTime = DateTime.Now.AddHours(2);
//                trea.EndTime = DateTime.Now.AddHours(3);
//                trea.Room_Num = 2;
//                trea.Type_Id = 2;

//                db.TblTreatments.Add(trea);
//                db.SaveChanges();
//            }
//            catch (Exception e)
//            {
//                throw e;
//            }

//        }
//        // DELETE: api/Treatment/5
//        public void Delete(int id)
//        {
//        }

//    }
//}
