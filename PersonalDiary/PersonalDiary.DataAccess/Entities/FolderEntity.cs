using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalDiary.DataAccess.Entities;

[Table("folders")]
public class FolderEntity : BaseEntity
{
    public string Name { get; set; }

    public virtual ICollection<FolderOfEntryEntity> Folders { get; set; }
}
