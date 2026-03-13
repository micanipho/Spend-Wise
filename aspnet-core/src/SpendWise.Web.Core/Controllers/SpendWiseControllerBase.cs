using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace SpendWise.Controllers
{
    public abstract class SpendWiseControllerBase : AbpController
    {
        protected SpendWiseControllerBase()
        {
            LocalizationSourceName = SpendWiseConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
