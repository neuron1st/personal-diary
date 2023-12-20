using PersonalDiary.BL.Entities.Folders.Entities;

namespace PersonalDiary.BL.Entities.Folders;

public interface IFoldersProvider
{
    IEnumerable<FolderModel> GetFolders(FolderModelFilter? filter = null);
    FolderModel GetFolderInfo(Guid id);
}
