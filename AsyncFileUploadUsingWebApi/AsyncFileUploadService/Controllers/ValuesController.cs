using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace AsyncFileUploadService.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromUri]string filename)
        {
            var task = this.Request.Content.ReadAsStreamAsync();

            task.Wait();

            Stream file = task.Result;

            try
            {
                Stream fileOnSystem = File.Create(HttpContext.Current.Server.MapPath("~/" + filename));

                file.CopyTo(fileOnSystem);
                file.Close();
                fileOnSystem.Close();
            }

            catch(IOException ex) {
                throw new HttpException();
            }
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