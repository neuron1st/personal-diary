namespace PersonalDiary.BL.Entities.DiaryEntries.Entities;

public class CreateDiaryEntryModel
{
    public string Heading { get; set; }
    public string? Text { get; set; }
    public bool IsPublic { get; set; }
    public int UserId { get; set; }

    public List<int> TagIds { get; set; }
    public List<int> FolderIds { get; set; }
}
