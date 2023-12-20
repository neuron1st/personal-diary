using PersonalDiary.BL.Entities.Admins.Entities;

namespace PersonalDiary.BL.Entities.Admins;

public interface IAdminsProvider
{
    IEnumerable<AdminModel> GetAdmins(AdminModelFilter? filter = null);
    AdminModel GetAdminInfo(Guid id);
}
