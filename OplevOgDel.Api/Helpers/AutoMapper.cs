using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Models.Dto.ExperienceDto;
using OplevOgDel.Api.Models.Dto.ReviewDto;
using Profile = AutoMapper.Profile;

namespace OplevOgDel.Api.Helpers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Experience, ViewExperienceDto>();
            CreateMap<Experience, ViewOneExperienceDto>();
            CreateMap<EditExperienceDto, Experience>().ForMember(x => x.Category, opt => opt.Ignore()).ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<NewExperienceDto, Experience>().ForMember(x => x.Category, opt => opt.Ignore());

            CreateMap<Review, ViewReviewDto>();
            CreateMap<Review, ViewOneReviewDto>();
            CreateMap<EditReviewDto, Review>();
            CreateMap<NewReviewDto, Review>();
        }
    }
}
