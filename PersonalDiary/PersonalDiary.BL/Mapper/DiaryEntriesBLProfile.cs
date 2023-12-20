using AutoMapper;
using PersonalDiary.BL.Entities.DiaryEntries.Entities;
using PersonalDiary.DataAccess.Entities;

namespace PersonalDiary.BL.Mapper;

public class DiaryEntriesBLProfile : Profile
{
    public DiaryEntriesBLProfile()
    {
        CreateMap<DiaryEntryEntity, DiaryEntryModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExternalId));

        CreateMap<CreateDiaryEntryModel, DiaryEntryEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ExternalId, opt => opt.Ignore())
            .ForMember(dest => dest.ModificationTime, opt => opt.Ignore())
            .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
            .ForMember(dest => dest.Tags, opt => opt.Ignore())
            .ForMember(dest => dest.Folders, opt => opt.Ignore());
    }
}
