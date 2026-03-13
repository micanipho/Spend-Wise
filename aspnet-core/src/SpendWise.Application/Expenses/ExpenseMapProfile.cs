using AutoMapper;
using SpendWise.Expenses.Dto;

namespace SpendWise.Expenses
{
    public class ExpenseMapProfile : Profile
    {
        public ExpenseMapProfile()
        {
            CreateMap<Expense, ExpenseDto>();
            CreateMap<CreateUpdateExpenseDto, Expense>().ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
