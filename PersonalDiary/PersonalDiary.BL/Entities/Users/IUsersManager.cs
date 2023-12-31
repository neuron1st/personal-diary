﻿using PersonalDiary.BL.Entities.Users.Entities;

namespace PersonalDiary.BL.Entities.Users;

public interface IUsersManager
{
    UserModel CreateUser(CreateUserModel model);
    void DeleteUser(Guid id);
    UserModel UpdateUser(Guid id, UpdateUserModel model);
}
