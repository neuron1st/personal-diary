using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalDiary.DataAccess.Entities;

[Table("folders_of_entry")]
public class FolderOfEntryEntity : BaseEntity
{
    public int FolderId { get; set; }
    public FolderEntity Folder { get; set; }

    public int DiaryEntryId { get; set; }
    public DiaryEntryEntity DiaryEntry { get; set; }
}
