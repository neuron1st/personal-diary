using AutoMapper;
using PersonalDiary.BL.Entities.DiaryEntries.Entities;
using PersonalDiary.DataAccess;
using PersonalDiary.DataAccess.Entities;

namespace PersonalDiary.BL.Entities.DiaryEntries;

public class DiaryEntriesProvider : IDiaryEntriesProvider
{
    private readonly IRepository<DiaryEntryEntity> _repository;
    private readonly IMapper _mapper;

    public DiaryEntriesProvider(IRepository<DiaryEntryEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public IEnumerable<DiaryEntryModel> GetDiaryEntries(DiaryEntryModelFilter? filter = null)
    {
        var heading = filter?.Heading;
        var text = filter?.Text;
        var isPublic = filter?.IsPublic;
        var userId = filter?.UserId;
        var tags = filter?.TagIds;
        var folders = filter?.FolderIds;

        var diaryEntries = _repository.GetAll(x =>
            (heading == null || x.Heading == heading) &&
            (text == null || x.Text.Contains(text)) &&
            (isPublic == null || x.IsPublic == isPublic) &&
            (userId == null || x.UserId == userId) &&
            (tags == null || tags.All(tagId => x.Tags.Any(entryTag => entryTag.Id == tagId))) &&
            (folders == null || folders.All(folderId => x.Folders.Any(entryFolder => entryFolder.Id == folderId))));

        return _mapper.Map<IEnumerable<DiaryEntryModel>>(diaryEntries);
    }

    public DiaryEntryModel GetDiaryEntryInfo(Guid id)
    {
        var entity = _repository.GetById(id);
        if (entity == null)
        {
            throw new ArgumentException("not found");
        }
        return _mapper.Map<DiaryEntryModel>(entity);
    }
}
