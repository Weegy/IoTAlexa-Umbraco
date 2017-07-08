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

        [HttpGet]
        public string GetPossibleRootNodes()
        {
            var contentTypeService = Services.ContentTypeService;
            var possibleRootTypes = contentTypeService.GetAllContentTypes().Where(x => x.AllowedAsRoot);

            var possibleTypes = "";
            foreach (var type in possibleRootTypes)
            {
                possibleTypes += type.Name + ",";
            }
            possibleTypes.TrimEnd(',');

            return "mögliche Typen: "+possibleTypes;
        }
        
    }
}