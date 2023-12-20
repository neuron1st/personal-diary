using AutoMapper;
using PersonalDiary.BL.Entities.Folders.Entities;
using PersonalDiary.BL.Exceptions;
using PersonalDiary.DataAccess;
using PersonalDiary.DataAccess.Entities;

namespace PersonalDiary.BL.Entities.Folders;

public class FoldersManager : IFoldersManager
{
    private readonly IRepository<FolderEntity> _repository;
    private readonly IRepository<DiaryEntryEntity> _diaryEntryRepository;
    private readonly IMapper _mapper;

    public FoldersManager(
        IRepository<FolderEntity> repository,
        IRepository<DiaryEntryEntity> diaryEntryRepository,
        IMapper mapper)
    {
        _repository = repository;
        _diaryEntryRepository = diaryEntryRepository;
        _mapper = mapper;
    }

    public FolderModel CreateFolder(CreateFolderModel model)
    {
        var entity = _mapper.Map<FolderEntity>(model);
        if (model.DiaryEntryIds != null && model.DiaryEntryIds.Any())
        {
            var diaryEntries = _diaryEntryRepository.GetAll(d => model.DiaryEntryIds.Contains(d.Id)).ToList();
            foreach (var diaryEntry in diaryEntries)
            {
                entity.DiaryEntries.Add(diaryEntry);
            }
        }

        if (model.FolderIds != null && model.FolderIds.Any())
        {
            var folders = _repository.GetAll(d => model.FolderIds.Contains(d.Id)).ToList();
            foreach (var folder in folders)
            {
                entity.Folders.Add(folder);
            }
        }

        _repository.Save(entity);

        return _mapper.Map<FolderModel>(entity);
    }

    public void DeleteFolder(Guid id)
    {
        var entity = _repository.GetById(id);
        if (entity == null)
        {
            throw new NotFoundException();
        }
        _repository.Delete(entity);
    }

    public FolderModel UpdateFolder(Guid id, UpdateFolderModel model)
    {
        var entity = _repository.GetById(id);
        if (entity == null)
        {
            throw new NotFoundException();
        }

        entity.Name = model.Name;
        entity.ParentFolderId = model.ParentFolderId;

        entity.DiaryEntries.Clear();
        if (model.DiaryEntryIds != null && model.DiaryEntryIds.Any())
        {
            var diaryEntries = _diaryEntryRepository.GetAll(d => model.DiaryEntryIds.Contains(d.Id)).ToList();
            foreach (var diaryEntry in diaryEntries)
            {
                entity.DiaryEntries.Add(diaryEntry);
            }
        }

        entity.Folders.Clear();
        if (model.FolderIds != null && model.FolderIds.Any())
        {
            var folders = _repository.GetAll(f => model.FolderIds.Contains(f.Id)).ToList();
            foreach (var folder in folders)
            {
                entity.Folders.Add(folder);
            }
        }

        _repository.Save(entity);

        return _mapper.Map<FolderModel>(entity);
    }
}
