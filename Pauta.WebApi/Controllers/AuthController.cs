using MobileItFramework.ComponentModel;
using MobileItFramework.Security;
using System.Web.Http;

namespace Pauta.WebApi.Controllers
{
    public class AuthController : ApiController
    {
        private readonly ICommonAdapter _commonAdapter;

        public AuthController(ICommonAdapter commonAdapter)
        {
            _commonAdapter = commonAdapter;
        }

        // GET api/Auth?{credential}
        // andborges|1q2w3e4r czE+1YayU2c7vdTei9whUtLhfVfRDlWSdk+rRReggoo=
        public IHttpActionResult Get([FromUri]Credential credential)
        {
            return Ok(_commonAdapter.Authenticate(credential));
        }

        //// [HttpGet]
        //// public object InvalidToken()
        //// {
        ////     return new { Valid = false };
        //// }
    }
}