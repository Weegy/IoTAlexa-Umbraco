using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IoTAlex.Core.API
{
    using Umbraco.Web.WebApi;
    public class AlexaController : UmbracoApiController
    {
        [HttpGet]
        public string GetActualUmbracoVersion()
        {
            return "Dies ist die Umbraco Instanz von Byte5";
        }
    }
}