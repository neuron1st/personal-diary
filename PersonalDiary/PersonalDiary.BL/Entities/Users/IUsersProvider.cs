using PersonalDiary.BL.Entities.Users.Entities;

namespace PersonalDiary.BL.Entities.Users;

public interface IUsersProvider
{
    IEnumerable<UserModel> GetUsers(UserModelFilter? filter = null);
    UserModel GetUserInfo(Guid id);
}
