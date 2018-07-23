using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Solution.Services.WebServices.Ext.Controllers
{
    public class HelloController : ApiController
    {
        // GET: Hello
        [HttpGet]
        public HttpResponseMessage Index()
        {
            return Request.CreateResponse(HttpStatusCode.OK,new { Message = "Hello" });
        }

    }   
}
