using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace SpendWise.Categories.Dto
{
    public class CreateUpdateCategoryDto : EntityDto<int>
    {
        public long? UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string Type { get; set; }

        [MaxLength(100)]
        public string Icon { get; set; }

        [MaxLength(7)]
        public string Color { get; set; }
    }
}
