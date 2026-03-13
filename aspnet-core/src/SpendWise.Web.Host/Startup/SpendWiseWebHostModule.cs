using Abp.Modules;
using Abp.Reflection.Extensions;
using SpendWise.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace SpendWise.Web.Host.Startup
{
    [DependsOn(
       typeof(SpendWiseWebCoreModule))]
    public class SpendWiseWebHostModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public SpendWiseWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SpendWiseWebHostModule).GetAssembly());
        }
    }
}
