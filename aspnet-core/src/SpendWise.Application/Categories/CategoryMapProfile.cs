using AutoMapper;
using SpendWise.Categories.Dto;

namespace SpendWise.Categories
{
    public class CategoryMapProfile : Profile
    {
        public CategoryMapProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateUpdateCategoryDto, Category>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
