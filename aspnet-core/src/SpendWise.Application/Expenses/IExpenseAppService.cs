using Abp.Application.Services;
using SpendWise.Expenses.Dto;

namespace SpendWise.Expenses
{
    public interface IExpenseAppService : IAsyncCrudAppService<ExpenseDto, int, PagedExpenseResultRequestDto, CreateUpdateExpenseDto, CreateUpdateExpenseDto>
    {
        // Additional methods specific to Expense can be added here
    }
}
