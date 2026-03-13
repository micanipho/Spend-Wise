using Abp.Application.Services;
using SpendWise.Sessions.Dto;
using System.Threading.Tasks;

namespace SpendWise.Sessions;

public interface ISessionAppService : IApplicationService
{
    Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
}
