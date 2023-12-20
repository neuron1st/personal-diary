using PersonalDiary.DataAccess;
using PersonalDiary.DataAccess.Entities;
using System.Linq.Expressions;

namespace PersonalDiary.UnitTests.BL.Admins;

public interface IAdminRepository : IRepository<AdminEntity>
{
    AdminEntity GetById(Guid id);
    IEnumerable<AdminEntity> GetAll(Expression<Func<AdminEntity, bool>> predicate);
}