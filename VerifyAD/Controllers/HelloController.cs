using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.DirectoryServices.AccountManagement;

namespace VerifyAD.Controllers
{
    // CORS - Enable HTTP calls from any source URL
	//      - To allow specific caller DNS domains only use this syntax:
	//        (origins: "http://domain1, http://domain1",
    [EnableCors(origins: "*",
        headers: "*",
        methods: "*",
        SupportsCredentials = true)]
    [Authorize]
    public class HelloController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            DateTime now = DateTime.Now;
            return new string[] { "Hello", now.ToLongDateString() + " " + now.ToLongTimeString() };
        }

        // GET api/values/5
        public string Get(int id)
        {
            DateTime now = DateTime.Now;
            return id.ToString() + " " + now.ToLongDateString() + " " + now.ToLongTimeString();
        }

        // POST api/values
        public bool Post([FromBody]verifySettings settings)
        {
            bool isValid = false;
            // create a "principal context" - e.g. your domain (could be machine, too)
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, settings.Domain))
            {
                // validate the credentials
                isValid = pc.ValidateCredentials(settings.User, settings.Password);
            }
            return isValid;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
