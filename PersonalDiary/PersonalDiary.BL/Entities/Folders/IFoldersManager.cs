using PersonalDiary.BL.Entities.Folders.Entities;

namespace PersonalDiary.BL.Entities.Folders;

public interface IFoldersManager
{
    FolderModel CreateFolder(CreateFolderModel model);
    void DeleteFolder(Guid id);
    FolderModel UpdateFolder(Guid id, UpdateFolderModel model);
}
