using Abp.Application.Services;
using SpendWise.Categories.Dto;

namespace SpendWise.Categories
{
    public interface ICategoryAppService
        : IAsyncCrudAppService<CategoryDto, int, PagedCategoryResultRequestDto, CreateUpdateCategoryDto, CreateUpdateCategoryDto>
    {
    }
}
