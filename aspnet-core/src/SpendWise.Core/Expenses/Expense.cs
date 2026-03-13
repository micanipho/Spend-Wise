using Abp.Domain.Entities.Auditing;
using System;

namespace SpendWise.Expenses
{
    public class Expense : FullAuditedEntity<int>
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
