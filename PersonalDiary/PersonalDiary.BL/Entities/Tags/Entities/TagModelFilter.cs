namespace PersonalDiary.BL.Entities.Tags.Entities;

public class TagModelFilter
{
    public string Name { get; set; }
    public List<int> DiaryEntryIds { get; set; }
}
