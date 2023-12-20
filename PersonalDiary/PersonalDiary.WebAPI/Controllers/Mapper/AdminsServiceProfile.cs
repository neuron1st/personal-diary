using AutoMapper;
using PersonalDiary.BL.Entities.Admins.Entities;
using PersonalDiary.WebAPI.Controllers.Entities.Admins.Entities;

namespace PersonalDiary.WebAPI.Controllers.Mapper;

public class AdminsServiceProfile : Profile
{
    public AdminsServiceProfile()
    {
        CreateMap<AdminsFilter, AdminModelFilter>();
        CreateMap<CreateAdminRequest, CreateAdminModel>();
        CreateMap<UpdateAdminRequest, UpdateAdminModel>();
    }
}
