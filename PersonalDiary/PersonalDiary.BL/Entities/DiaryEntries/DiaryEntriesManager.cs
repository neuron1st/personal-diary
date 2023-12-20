using AutoMapper;
using PersonalDiary.BL.Entities.DiaryEntries.Entities;
using PersonalDiary.DataAccess;
using PersonalDiary.DataAccess.Entities;

namespace PersonalDiary.BL.Entities.DiaryEntries;

public class DiaryEntriesManager : IDiaryEntriesManager
{
    private readonly IRepository<DiaryEntryEntity> _repository;
    private readonly IRepository<TagEntity> _tagRepository;
    private readonly IRepository<FolderEntity> _folderRepository;
    private readonly IMapper _mapper;

    public DiaryEntriesManager(
            IRepository<DiaryEntryEntity> repository,
            IRepository<TagEntity> tagRepository,
            IRepository<FolderEntity> folderRepository,
            IMapper mapper)
    {
        _repository = repository;
        _tagRepository = tagRepository;
        _folderRepository = folderRepository;
        _mapper = mapper;
    }

    public DiaryEntryModel CreateDiaryEntry(CreateDiaryEntryModel model)
    {
        var entity = _mapper.Map<DiaryEntryEntity>(model);

        if (model.TagIds != null && model.TagIds.Any())
        {
            var tags = _tagRepository.GetAll(t => model.TagIds.Contains(t.Id)).ToList();
            foreach (var tag in tags)
            {
                entity.Tags.Add(tag);
            }
        }

        if (model.FolderIds != null && model.FolderIds.Any())
        {
            var folders = _folderRepository.GetAll(f => model.FolderIds.Contains(f.Id)).ToList();
            foreach (var folder in folders)
            {
                entity.Folders.Add(folder);
            }
        }

        _repository.Save(entity);

        return _mapper.Map<DiaryEntryModel>(entity);
    }

    public void DeleteDiaryEntry(Guid id)
    {
        var entity = _repository.GetById(id);
        if (entity == null)
        {
            throw new ArgumentException("not found");
        }
        _repository.Delete(entity);
    }

    public DiaryEntryModel UpdateDiaryEntry(Guid id, UpdateDiaryEntryModel model)
    {
        var entity = _repository.GetById(id);
        if (entity == null)
        {
            throw new ArgumentException("not found");
        }

        entity.Heading = model.Heading;
        entity.Text = model.Text;
        entity.IsPublic = model.IsPublic;

        entity.Tags.Clear();
        if (model.TagIds != null && model.TagIds.Any())
        {
            var tags = _tagRepository.GetAll(t => model.TagIds.Contains(t.Id)).ToList();
            foreach (var tag in tags)
            {
                entity.Tags.Add(tag);
            }
        }

        entity.Folders.Clear();
        if (model.FolderIds != null && model.FolderIds.Any())
        {
            var folders = _folderRepository.GetAll(f => model.FolderIds.Contains(f.Id)).ToList();
            foreach (var folder in folders)
            {
                entity.Folders.Add(folder);
            }
        }

        _repository.Save(entity);

        return _mapper.Map<DiaryEntryModel>(entity);
    }
}
