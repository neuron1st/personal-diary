using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalDiary.DataAccess.Entities;

[Table("admins")]
public class AdminEntity : BaseEntity
{
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
}
