using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using SpendWise.Authorization;
using SpendWise.Expenses.Dto;
using System.Linq;

namespace SpendWise.Expenses
{
    [AbpAuthorize(PermissionNames.Pages_Expenses)]
    public class ExpenseAppService : AsyncCrudAppService<Expense, ExpenseDto, int, PagedExpenseResultRequestDto, CreateUpdateExpenseDto, CreateUpdateExpenseDto>, IExpenseAppService
    {
        public ExpenseAppService(IRepository<Expense, int> repository) : base(repository)
        {
            CreatePermissionName = PermissionNames.Pages_Expenses_Create;
            UpdatePermissionName = PermissionNames.Pages_Expenses_Edit;
            DeletePermissionName = PermissionNames.Pages_Expenses_Delete;
        }

        protected override IQueryable<Expense> CreateFilteredQuery(PagedExpenseResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!string.IsNullOrEmpty(input.keyword),
                    p => p.Description.Contains(input.keyword));
        }

        protected override IQueryable<Expense> ApplySorting(IQueryable<Expense> query, PagedExpenseResultRequestDto input)
        {
            return query.OrderByDescending(p => p.CreationTime);
        }
    }
}
