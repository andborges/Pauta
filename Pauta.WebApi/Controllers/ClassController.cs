using MobileItFramework.Encryption;
using MobileItFramework.Security;
using MobileItFramework.WebApi.Security;
using Pauta.Component.Framework;
using System;
using System.Linq;
using System.Web.Http;

namespace Pauta.WebApi.Controllers
{
    public class ClassController : ApiController
    {
        private readonly IIntegrationAdapter _integrationAdapter;

        public ClassController(IIntegrationAdapter integrationAdapter)
        {
            _integrationAdapter = integrationAdapter;
        }

        // GET api/Class?{date}
        [RequireToken]
        public IHttpActionResult Get([FromUri]Token token, DateTime date)
        {
            return Ok(_integrationAdapter.GetAllClasses(token, date));
        }

        // GET api/Class/{id}
        [RequireToken]
        public IHttpActionResult Get([FromUri]Token token, string id)
        {
            return this.Ok(_integrationAdapter.GetClass(token, id));
        }

        // GET api/Class/Save/{id}?{attendances}
        [HttpGet]
        [RequireToken]
        public IHttpActionResult Save([FromUri]Token token, string id, string attendances)
        {
            var myclass = this._integrationAdapter.GetClass(token, id);

            var attendancePairs = attendances.AesDecrypt(new AppAesEncryptionInfo()).Split(';');

            foreach (var attendancePair in attendancePairs)
            {
                if (!string.IsNullOrEmpty(attendancePair))
                {
                    var attendance = attendancePair.Split(':');

                    if (myclass.Attendances.Any(a => a.StudentNumber == attendance[0]))
                    {
                        myclass.Attendances.First(a => a.StudentNumber == attendance[0]).Attendant = string.IsNullOrEmpty(attendance[1]) ? (bool?)null : attendance[1] == "1";
                    }
                }
            }

            return Ok(_integrationAdapter.SaveClass(token, myclass));
        }
    }
}