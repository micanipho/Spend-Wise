using Abp.Application.Services.Dto;

namespace SpendWise.Categories.Dto
{
    public class CategoryDto : EntityDto<int>
    {
        public long? UserId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
    }
}
