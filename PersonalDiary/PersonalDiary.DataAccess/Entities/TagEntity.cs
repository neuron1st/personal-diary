using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalDiary.DataAccess.Entities;

[Table("tags")]
public class TagEntity : BaseEntity
{
    public string Name { get; set; }

    public virtual ICollection<DiaryEntryEntity> DiaryEntries { get; set; }
}
