using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalDiary.DataAccess.Entities;

[Table("users")]
public class UserEntity : IdentityUser<int>, IBaseEntity
{
    public Guid ExternalId { get; set; }
    public DateTime ModificationTime { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime Birthday { get; set; }

    public virtual ICollection<DiaryEntryEntity> DiaryEntries { get; set; }
}

public class UserRoleEntity : IdentityRole<int>
{
}
