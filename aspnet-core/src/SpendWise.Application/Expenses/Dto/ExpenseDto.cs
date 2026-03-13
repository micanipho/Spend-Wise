

using Abp.Application.Services.Dto;
using System;

namespace SpendWise.Expenses.Dto
{
    public class ExpenseDto : EntityDto<int>
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
