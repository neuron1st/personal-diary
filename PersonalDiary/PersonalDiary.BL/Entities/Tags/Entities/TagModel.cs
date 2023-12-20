using PersonalDiary.BL.Entities.DiaryEntries.Entities;

namespace PersonalDiary.BL.Entities.Tags.Entities;

public class TagModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual List<DiaryEntryModel> DiaryEntries { get; set; }
}
