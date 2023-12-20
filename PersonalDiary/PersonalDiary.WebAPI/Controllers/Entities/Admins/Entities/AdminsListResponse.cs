using PersonalDiary.BL.Entities.Admins.Entities;

namespace PersonalDiary.WebAPI.Controllers.Entities.Admins.Entities;

public class AdminsListResponse
{
    public List<AdminModel> Admins { get; set; }
}
