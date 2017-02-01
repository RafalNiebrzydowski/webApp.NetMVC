using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Geodeta.Data;
using Geodeta.Services;

namespace Inzynierka.Controllers
{
    public class AreaSyncApiController : ApiController
    {
        /// <summary>
        /// Obiekt DTO obszaru
        /// </summary>
        private AreaDTO areaDto = new AreaDTO();



        /// <summary>
        /// Edycja obszaru o danym id przy synchronizacji
        /// </summary>
        /// <param name="id">Id area</param>
        /// <param name="a">Informacje do edycji obszaru z pkt i liniami</param>
        /// <returns>Response</returns>
        public HttpResponseMessage PutAreaSync(int id)
        {
            if (ModelState.IsValid)
            {
                Geodeta.Services.Area area = this.areaDto.PutAreaSync(id);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, area);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = area.ID }));
                return response;
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
        
    }
}