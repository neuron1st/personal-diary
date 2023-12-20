using AutoMapper;
using PersonalDiary.BL.Entities.DiaryEntries.Entities;
using PersonalDiary.WebAPI.Controllers.Entities.DiaryEntries.Entities;

namespace PersonalDiary.WebAPI.Controllers.Mapper;

public class DiaryEntriesServiceProfile : Profile
{
    public DiaryEntriesServiceProfile()
    {
        CreateMap<DiaryEntriesFilter, DiaryEntryModelFilter>();
        CreateMap<CreateDiaryEntryRequest, CreateDiaryEntryModel>();
        CreateMap<UpdateDiaryEntryRequest, UpdateDiaryEntryModel>();
    }
}
