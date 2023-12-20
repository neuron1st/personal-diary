using PersonalDiary.BL.Entities.Users.Entities;

namespace PersonalDiary.BL.Entities.Users;

internal interface IUsersManager
{
    UserModel CreateUser(CreateUserModel model);
    void DeleteUser(Guid id);
    UserModel UpdateUser(Guid id, UpdateUserModel model);
}
