using OplevOgDel.Api.Data.Models;
using OplevOgDel.Api.Models;
using Profile = AutoMapper.Profile;

namespace OplevOgDel.Api.Helpers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Experience, ViewExperienceDto>();
            CreateMap<EditExperienceDto, Experience>().ForMember(x => x.Category, opt => opt.Ignore());
            CreateMap<NewExperienceDto, Experience>().ForMember(x => x.Category, opt => opt.Ignore());
        }
    }
}
