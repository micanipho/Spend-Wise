using Abp.Application.Services;
using SpendWise.Authorization.Accounts.Dto;
using System.Threading.Tasks;

namespace SpendWise.Authorization.Accounts;

public interface IAccountAppService : IApplicationService
{
    Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

    Task<RegisterOutput> Register(RegisterInput input);
}
