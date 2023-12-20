using AutoMapper;
using PersonalDiary.BL.Entities.Tags.Entities;
using PersonalDiary.DataAccess;
using PersonalDiary.DataAccess.Entities;

namespace PersonalDiary.BL.Entities.Tags;

public class TagsManager : ITagsManager
{
    private readonly IRepository<TagEntity> _repository;
    private readonly IRepository<DiaryEntryEntity> _diaryEntryRepository;
    private readonly IMapper _mapper;

    public TagsManager(
        IRepository<TagEntity> repository,
        IRepository<DiaryEntryEntity> diaryEntryRepository,
        IMapper mapper)
    {
        _repository = repository;
        _diaryEntryRepository = diaryEntryRepository;
        _mapper = mapper;
    }

    public TagModel CreateTag(CreateTagModel model)
    {
        var entity = _mapper.Map<TagEntity>(model);
        if (model.DiaryEntryIds != null && model.DiaryEntryIds.Any())
        {
            var diaryEntries = _diaryEntryRepository.GetAll(d => model.DiaryEntryIds.Contains(d.Id)).ToList();
            foreach (var diaryEntry in diaryEntries)
            {
                entity.DiaryEntries.Add(diaryEntry);
            }
        }

        _repository.Save(entity);

        return _mapper.Map<TagModel>(entity);
    }

    public void DeleteTag(Guid id)
    {
        var entity = _repository.GetById(id);
        if (entity == null)
        {
            throw new ArgumentException("not found");
        }
        _repository.Delete(entity);
    }

    public TagModel UpdateTag(Guid id, UpdateTagModel model)
    {
        var entity = _repository.GetById(id);
        if (entity == null)
        {
            throw new ArgumentException("not found");
        }

        entity.Name = model.Name;

        entity.DiaryEntries.Clear();
        if (model.DiaryEntryIds != null && model.DiaryEntryIds.Any())
        {
            var diaryEntries = _diaryEntryRepository.GetAll(d => model.DiaryEntryIds.Contains(d.Id)).ToList();
            foreach (var diaryEntry in diaryEntries)
            {
                entity.DiaryEntries.Add(diaryEntry);
            }
        }

        _repository.Save(entity);

        return _mapper.Map<TagModel>(entity);
    }
}
