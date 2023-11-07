using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalDiary.DataAccess.Entities;

[Table("tags_of_entry")]
public class TagOfEntryEntity
{
    public int TagId { get; set; }
    public TagEntity Tag { get; set; }

    public int DiaryEntryId { get; set; }
    public DiaryEntryEntity DiaryEntry { get; set; }
}
