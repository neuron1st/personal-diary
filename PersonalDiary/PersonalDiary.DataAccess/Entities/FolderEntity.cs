using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalDiary.DataAccess.Entities;

[Table("folders")]
public class FolderEntity : BaseEntity
{
    public string Name { get; set; }

    public int ParentFolderId { get; set; }
    public FolderEntity ParentFolder { get; set; }

    public virtual ICollection<FolderEntity> Folders { get; set; }

    public virtual ICollection<FolderOfEntryEntity> FoldersOfEntry { get; set; }
}
