using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Models.Dto.ExperienceDto;
using OplevOgDel.Api.Models.Dto.PictureDto;
using OplevOgDel.Api.Models.Dto.ProfileDto;
using OplevOgDel.Api.Models.Dto.ReviewDto;
using OplevOgDel.Api.Models.Dto.UserDto;
using Profile = AutoMapper.Profile;

namespace OplevOgDel.Api.Helpers
{
    /// <summary>
    /// Configuration for automapper  to map DTO's to Models and vice versa
    /// </summary>
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Experience, ViewExperienceDto>().ForMember(x => x.Pictures, opt => opt.MapFrom(y => y.Pictures));
            CreateMap<Experience, ViewOneExperienceDto>().ForMember(x => x.Pictures, opt => opt.MapFrom(y => y.Pictures));
            // map EditExperienceDto to Experience  model 
            // ignore category property
            // but only the properties that are not null
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
            // map NewProfileDto to Profile model but calculate the age from the birthday
            CreateMap<NewProfileDto, Data.Models.Profile>().ForMember(x=> x.Age, opt => opt.MapFrom(y => y.Birthday.GetAge()));
            CreateMap<EditProfileDto, Profile>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
