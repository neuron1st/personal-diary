using AutoMapper;
using PersonalDiary.BL.Entities.Folders.Entities;
using PersonalDiary.DataAccess;
using PersonalDiary.DataAccess.Entities;

namespace PersonalDiary.BL.Entities.Folders;

public class FoldersProvider : IFoldersProvider
{
    private readonly IRepository<FolderEntity> _repository;
    private readonly IMapper _mapper;

    public FoldersProvider(IRepository<FolderEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public IEnumerable<FolderModel> GetFolders(FolderModelFilter? filter = null)
    {
        var name = filter?.Name;
        var parentFolderId = filter?.ParentFolderId;
        var folderIds = filter?.FolderIds;
        var diaryEntryIds = filter?.DiaryEntryIds;

        var folders = _repository.GetAll(x =>
            (name == null || x.Name == name) &&
            (parentFolderId == null || x.ParentFolderId == parentFolderId) &&
            (folderIds == null || folderIds.All(folderId => x.Folders.Any(entryTag => entryTag.Id == folderId))) &&
            (diaryEntryIds == null || diaryEntryIds.All(diaryEntryId => x.DiaryEntries.Any(diaryEntry => diaryEntry.Id == diaryEntryId))));

        return _mapper.Map<IEnumerable<FolderModel>>(folders);
    }

    public FolderModel GetFolderInfo(Guid id)
    {
        var entity = _repository.GetById(id);
        if (entity == null)
        {
            throw new ArgumentException("not found");
        }
        return _mapper.Map<FolderModel>(entity);
    }
}
