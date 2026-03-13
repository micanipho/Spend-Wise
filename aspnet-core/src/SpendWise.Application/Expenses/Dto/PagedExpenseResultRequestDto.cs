using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise.Expenses.Dto
{
    public class PagedExpenseResultRequestDto : PagedResultRequestDto
    {
        public string keyword { get; set; }
    }
}
