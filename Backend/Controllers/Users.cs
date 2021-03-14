using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Users.Controllers
{
    public class Users : Controller
    {
        public Users(){}
        /// <summary>
        /// WELCOME TO LAB RESERVATION  (AKA SOFTWARE STUDIO PROJECT)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/welcome")]

        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(new string[] {"X"});
            // return NotFound();
            // return new string[] {"N O T H I N G!"};
        }
    }
}