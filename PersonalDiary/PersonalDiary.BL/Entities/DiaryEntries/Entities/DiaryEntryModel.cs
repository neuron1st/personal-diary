using PersonalDiary.BL.Entities.Folders.Entities;
using PersonalDiary.BL.Entities.Tags.Entities;
using PersonalDiary.BL.Entities.Users.Entities;

namespace PersonalDiary.BL.Entities.DiaryEntries.Entities;

public class DiaryEntryModel
{
    public Guid Id { get; set; }
    public string Heading { get; set; }
    public string? Text { get; set; }
    public bool IsPublic { get; set; }
    public int UserId { get; set; }
    public UserModel User { get; set; }
    public List<TagModel> Tags { get; set; }
    public List<FolderModel> Folders { get; set; }
}
