using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Umbraco.Core.Models;

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
            var allowedRootTypes = contentTypeService.GetAllContentTypes().Where(x => x.AllowedAsRoot).OrderBy(x=>x.Id).ToList();

            var possibleTypes = "";
            for (int i = 0; i < allowedRootTypes.Count; i++)
            {
                possibleTypes += (i+1)+" "+ allowedRootTypes[i].Alias;
            }

            return "mögliche Typen: " + possibleTypes;
        }

        [HttpGet]
        public string CreateRootNode(int index)
        {
            var contentTypeService = Services.ContentTypeService;
            var contentService = Services.ContentService;

            var allowedRootTypes = contentTypeService.GetAllContentTypes().Where(x => x.AllowedAsRoot).OrderBy(x => x.Id).ToList();
            var rootType = allowedRootTypes[index-1];

            var newRootNode = contentService.CreateContent("New Node", -1, rootType.Alias);
            contentService.Save(newRootNode);
            return "Der Knoten wurde erfolgreich angelegt";

        }

    }
}