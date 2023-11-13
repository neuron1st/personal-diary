using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalDiary.DataAccess.Entities;

[Table("diary_entries")]
public class DiaryEntryEntity : BaseEntity
{
    public string Heading { get; set; }
    public string Text { get; set; }
    public bool IsPublic { get; set; }

    public int UserId { get; set; }
    public UserEntity User { get; set; }

    public virtual ICollection<TagEntity> Tags { get; set; }

    public virtual ICollection<FolderEntity> Folders { get; set; }
}
