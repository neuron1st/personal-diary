using AutoMapper;
using PersonalDiary.BL.Entities.Users.Entities;
using PersonalDiary.DataAccess.Entities;

namespace PersonalDiary.BL.Mapper;

public class UsersBLProfile : Profile
{
    public UsersBLProfile()
    {
        CreateMap<UserEntity, UserModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExternalId));

        CreateMap<CreateUserModel, UserEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ExternalId, opt => opt.Ignore())
            .ForMember(dest => dest.ModificationTime, opt => opt.Ignore())
            .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
            .ForMember(dest => dest.DiaryEntries, opt => opt.Ignore());
    }
}
