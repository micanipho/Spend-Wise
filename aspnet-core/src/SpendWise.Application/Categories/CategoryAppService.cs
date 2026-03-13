using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using SpendWise.Authorization;
using SpendWise.Categories.Dto;
using System.Linq;

namespace SpendWise.Categories
{
    [AbpAuthorize(PermissionNames.Pages_Categories)]
    public class CategoryAppService
        : AsyncCrudAppService<Category, CategoryDto, int, PagedCategoryResultRequestDto, CreateUpdateCategoryDto, CreateUpdateCategoryDto>,
          ICategoryAppService
    {
        public CategoryAppService(IRepository<Category, int> repository) : base(repository)
        {
            CreatePermissionName = PermissionNames.Pages_Categories_Create;
            UpdatePermissionName = PermissionNames.Pages_Categories_Edit;
            DeletePermissionName = PermissionNames.Pages_Categories_Delete;
        }

        protected override IQueryable<Category> CreateFilteredQuery(PagedCategoryResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!string.IsNullOrEmpty(input.Keyword),
                    x => x.Name.Contains(input.Keyword));
        }

        protected override IQueryable<Category> ApplySorting(IQueryable<Category> query, PagedCategoryResultRequestDto input)
        {
            return query.OrderByDescending(x => x.CreationTime);
        }
    }
}
