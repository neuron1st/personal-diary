using PersonalDiary.BL.Entities.DiaryEntries.Entities;

namespace PersonalDiary.BL.Entities.Folders.Entities;

public class FolderModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public int? ParentFolderId { get; set; }
    public FolderModel? ParentFolder { get; set; }

    public List<FolderModel> Folders { get; set; }

    public List<DiaryEntryModel> DiaryEntries { get; set; }
}