using AutoMapper;
using PersonalDiary.BL.Entities.Tags.Entities;
using PersonalDiary.DataAccess.Entities;

namespace PersonalDiary.BL.Mapper;

public class TagsBLProfile : Profile
{
    public TagsBLProfile()
    {
        CreateMap<TagEntity, TagModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExternalId));

        CreateMap<CreateTagModel, TagEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ExternalId, opt => opt.Ignore())
            .ForMember(dest => dest.ModificationTime, opt => opt.Ignore())
            .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
            .ForMember(dest => dest.DiaryEntries, opt => opt.Ignore());
    }
}
