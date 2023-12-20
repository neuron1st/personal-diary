using PersonalDiary.BL.Entities.DiaryEntries.Entities;

namespace PersonalDiary.BL.Entities.Tags.Entities;

public class CreateTagModel
{
    public string Name { get; set; }
    public List<int> DiaryEntryIds { get; set; }
}
