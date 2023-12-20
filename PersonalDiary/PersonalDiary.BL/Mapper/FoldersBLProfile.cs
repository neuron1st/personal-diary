using AutoMapper;
using PersonalDiary.BL.Entities.Folders.Entities;
using PersonalDiary.DataAccess.Entities;

namespace PersonalDiary.BL.Mapper;

public class FoldersBLProfile : Profile
{
    public FoldersBLProfile()
    {
        CreateMap<FolderEntity, FolderModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExternalId));

        CreateMap<CreateFolderModel, FolderEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ExternalId, opt => opt.Ignore())
            .ForMember(dest => dest.ModificationTime, opt => opt.Ignore())
            .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
            .ForMember(dest => dest.ParentFolder, opt => opt.Ignore())
            .ForMember(dest => dest.Folders, opt => opt.Ignore())
            .ForMember(dest => dest.DiaryEntries, opt => opt.Ignore());
    }
}
