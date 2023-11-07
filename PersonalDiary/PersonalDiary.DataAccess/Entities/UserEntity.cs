using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalDiary.DataAccess.Entities;

[Table("users")]
public class UserEntity : BaseEntity
{
    public string Login { get; set; }
    public string PasswordHush { get; set; }
    public string Email { get; set; }

    public virtual ICollection<DiaryEntryEntity> DiaryEntries { get; set; }
}
