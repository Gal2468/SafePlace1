using ClinicWebApplication.Models;
using Data;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;

namespace ClinicWebApplication.DTO
{
    [RoutePrefix("api/SignInUser")]
    public class EnycreptedUserController : ApiController
    {
        ClinicEntitiesDB db = new ClinicEntitiesDB();
        public static string EncryptPassword1(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] storePassword = ASCIIEncoding.ASCII.GetBytes(password);
                string encryptedPassword = Convert.ToBase64String(storePassword);
                return encryptedPassword;
            }
        }

        public static string DecryptPassword1(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] encryptedPassword = ASCIIEncoding.ASCII.GetBytes(password);
                string decryptedPassword = Convert.ToBase64String(encryptedPassword);
                return decryptedPassword;
            }
        }
        public static string EncryptPassword(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] encrypted = md5.ComputeHash(bytes);
            return Encoding.UTF8.GetString(encrypted);
        }


        public static bool IsPasswordMatch(string password, string hashedPassword)
        {
            string encryptedPassword = EncryptPassword(password);
            return encryptedPassword.Equals(hashedPassword);
        }
        [HttpPost]
        [Route("SignIn")]
        public IHttpActionResult SignUp([FromBody] EnycreptedUserDTO model)
        {
            try
            {
                using (var db = new ClinicEntitiesDB())
                {
                    string modelGender = "";

                    if (model.gender == "זכר")
                    {
                        modelGender = "M";
                    }
                    else
                    {
                        modelGender = "F";
                    };

                    var newPatient = new TblPatient
                    {
                        FirstName = model.firstname,
                        LastName = model.lastname,
                        BirthDate = model.birthdate,
                        StartDate = model.startdate,
                        Patient_Id = model.patient_Id


                    };
                    db.TblPatient.Add(newPatient);
                    db.SaveChanges();

                }
                using (var db = new ClinicEntitiesDB())
                {
                    var newUser = new TblUsers
                    {
                        Email = model.email,
                        Password = EncryptPassword(model.password),
                        Patient_Id = model.patient_Id,
                        IsSuperUser = model.issuperuser

                    };

                    db.TblUsers.Add(newUser);
                    db.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }




        }


        [HttpPost]
        [Route("login")]
        public HttpContext Login([FromBody] DecryptUserDTO model, HttpContext context)
        {
            using (var db = new ClinicEntitiesDB())
            {
                var hasdedPassword = db.TblUsers.FirstOrDefault(u => u.Email == model.email);
                string hasded = hasdedPassword.Password;
                if (IsPasswordMatch(model.password, hasded))
                {
                    ////Check for user IsSuperUser Level
                    //int userLevel = hasdedPassword.IsSuperUser;

                    //// Generate a Session Authentication token
                    //string sessionToken = Guid.NewGuid().ToString("N");

                    //// Set the token as a session variable
                    //context.Session["SessionToken"] = sessionToken;

                    //// Create an HTTP response
                    //context.Response.StatusCode = (int)HttpStatusCode.OK;
                    //context.Response.ContentType = "application/json";

                    //context.Response.Write("{\"sessionToken\":\"" + sessionToken + "\"}");

                    ////return Content(HttpStatusCode.OK, userLevel);
                    //return context;
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }


            }


        }

    }
}
