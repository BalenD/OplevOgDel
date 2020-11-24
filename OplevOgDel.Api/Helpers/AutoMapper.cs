using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Models.Dto.ExperienceDto;
using OplevOgDel.Api.Models.Dto.PictureDto;
using OplevOgDel.Api.Models.Dto.ReviewDto;
using OplevOgDel.Api.Models.Dto.UserDto;
using Profile = AutoMapper.Profile;

namespace OplevOgDel.Api.Helpers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Experience, ViewExperienceDto>().ForMember(x => x.Pictures, opt => opt.MapFrom(y => y.Pictures));
            CreateMap<Experience, ViewOneExperienceDto>().ForMember(x => x.Pictures, opt => opt.MapFrom(y => y.Pictures));
            CreateMap<EditExperienceDto, Experience>().ForMember(x => x.Category, opt => opt.Ignore()).ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<NewExperienceDto, Experience>().ForMember(x => x.Category, opt => opt.Ignore());

            CreateMap<Review, ViewReviewDto>();
            CreateMap<Review, ViewOneReviewDto>();
            CreateMap<EditReviewDto, Review>();
            CreateMap<NewReviewDto, Review>();

            CreateMap<Picture, ViewPictureDto>();

            
            CreateMap<User, ViewUserDto>();
            CreateMap<UpdateUserDto, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CreateUserDto, User>();
            CreateMap<UserRegisterDto, User>();
        }
    }
}
