using AutoMapper;
using PersonalDiary.BL.Entities.Users.Entities;
using PersonalDiary.WebAPI.Controllers.Entities.Users.Entities;

namespace PersonalDiary.WebAPI.Controllers.Mapper;

public class UsersServiceProfile : Profile
{
    public UsersServiceProfile()
    {
        CreateMap<UsersFilter, UserModelFilter>();
        CreateMap<CreateUserRequest, CreateUserModel>();
        CreateMap<UpdateUserRequest, UpdateUserModel>();
    }
}
