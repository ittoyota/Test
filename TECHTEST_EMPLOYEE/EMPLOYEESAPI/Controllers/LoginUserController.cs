using EMPLOYEESAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace EMPLOYEESAPI.Controllers
{
    public class LoginUserController : ApiController
    {
        private EMPLOYEEDBEntities1 db = new EMPLOYEEDBEntities1();
         
        [ResponseType(typeof(EMPDETAIL))]
        public IHttpActionResult PostUserLogin(EMPDETAIL emp)
        { 
            EMPDETAIL eMPDETAIL = db.EMPDETAILS.Where(x => x.USERNAME == emp.USERNAME && x.USERPASSWORD == emp.USERPASSWORD).FirstOrDefault();
            if (eMPDETAIL == null)
            {
                return NotFound();
            }
            return CreatedAtRoute("DefaultApi", new { id = eMPDETAIL.USERNAME }, eMPDETAIL);
        }
    }
}
