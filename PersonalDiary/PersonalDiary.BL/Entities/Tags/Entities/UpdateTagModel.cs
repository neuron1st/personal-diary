namespace PersonalDiary.BL.Entities.Tags.Entities;

public class UpdateTagModel
{
    public string Name { get; set; }
    public List<int> DiaryEntryIds { get; set; }
}
