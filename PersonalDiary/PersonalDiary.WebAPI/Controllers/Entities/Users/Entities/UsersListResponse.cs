using PersonalDiary.BL.Entities.Users.Entities;

namespace PersonalDiary.WebAPI.Controllers.Entities.Users.Entities;

public class UsersListResponse
{
    public List<UserModel> Users { get; set; }
}
