namespace PersonalDiary.BL.Entities.Folders.Entities;

public class CreateFolderModel
{
    public string Name { get; set; }
    public int? ParentFolderId { get; set; }

    public List<int> FolderIds { get; set; }
    public List<int> DiaryEntryIds { get; set; }
}
