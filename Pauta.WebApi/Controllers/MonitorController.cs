using log4net;
using MobileItFramework.ComponentModel;
using MobileItFramework.WebApi.Monitoring;
using Ninject;
using Pauta.Component.Framework;
using System.Configuration;
using System.Web.Http;

namespace Pauta.WebApi.Controllers
{
    public class MonitorController : ApiController, IMonitorController
    {
        private readonly ICommonAdapter _commonAdapter;
        private readonly IIntegrationAdapter _integrationAdapter;

        [Inject]
        public ILog Log { get; set; }

        public MonitorController(ICommonAdapter commonAdapter, IIntegrationAdapter integrationAdapter)
        {
            this._commonAdapter = commonAdapter;
            this._integrationAdapter = integrationAdapter;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            Log.Info("Teste");

            var customer = ConfigurationManager.AppSettings["Customer"];
            var frameworkVersion = System.Reflection.Assembly.GetAssembly(typeof(MonitorInfo)).GetName().Version.ToString();
            var applicationVersion = System.Reflection.Assembly.GetAssembly(typeof(MonitorController)).GetName().Version.ToString();

            return Ok(new MonitorInfo(customer, "Pauta", applicationVersion, frameworkVersion, _integrationAdapter, true, null));
        }
    }
}
