using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace SpendWise.Expenses.Dto
{
    public class CreateUpdateExpenseDto : EntityDto<int>
    {
        [Required]
        [MaxLength(256)]
        public string Description { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
