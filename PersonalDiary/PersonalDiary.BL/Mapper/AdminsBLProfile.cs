using AutoMapper;
using PersonalDiary.BL.Entities.Admins.Entities;
using PersonalDiary.DataAccess.Entities;

namespace PersonalDiary.BL.Mapper;

public class AdminsBLProfile : Profile
{
    public AdminsBLProfile()
    {
        CreateMap<AdminEntity, AdminModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExternalId));

        CreateMap<CreateAdminModel, AdminEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ExternalId, opt => opt.Ignore())
            .ForMember(dest => dest.ModificationTime, opt => opt.Ignore())
            .ForMember(dest => dest.CreationTime, opt => opt.Ignore());
    }
}
