using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalDiary.DataAccess.Entities;

[Table("admins")]
public class AdminEntity : BaseEntity
{
    public string Login { get; set; }
    public string PasswordHush { get; set; }
    public string Email { get; set; }
}
