using AutoMapper;
using PersonalDiary.BL.Entities.Tags.Entities;
using PersonalDiary.DataAccess;
using PersonalDiary.DataAccess.Entities;

namespace PersonalDiary.BL.Entities.Tags;

public class TagsProvider : ITagsProvider
{
    private readonly IRepository<TagEntity> _repository;
    private readonly IMapper _mapper;

    public TagsProvider(IRepository<TagEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public IEnumerable<TagModel> GetTags(TagModelFilter? filter = null)
    {
        var name = filter?.Name;
        var diaryEntryIds = filter?.DiaryEntryIds;

        var tags = _repository.GetAll(x =>
            (name == null || x.Name == name) &&
            (diaryEntryIds == null || diaryEntryIds.All(diaryEntryId => x.DiaryEntries.Any(diaryEntry => diaryEntry.Id == diaryEntryId))));

        return _mapper.Map<IEnumerable<TagModel>>(tags);
    }

    public TagModel GetTagInfo(Guid id)
    {
        var entity = _repository.GetById(id);
        if (entity == null)
        {
            throw new ArgumentException("not found");
        }
        return _mapper.Map<TagModel>(entity);
    }
}
