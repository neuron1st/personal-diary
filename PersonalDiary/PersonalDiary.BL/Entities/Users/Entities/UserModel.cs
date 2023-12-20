using PersonalDiary.BL.Entities.DiaryEntries.Entities;

namespace PersonalDiary.BL.Entities.Users.Entities;

public class UserModel
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public List<DiaryEntryModel> DiaryEntries { get; set; }
}
