using Abp.Application.Services;
using SpendWise.MultiTenancy.Dto;

namespace SpendWise.MultiTenancy;

public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
{
}

