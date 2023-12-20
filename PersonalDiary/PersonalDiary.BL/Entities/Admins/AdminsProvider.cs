using AutoMapper;
using PersonalDiary.BL.Entities.Admins.Entities;
using PersonalDiary.BL.Exceptions;
using PersonalDiary.DataAccess;
using PersonalDiary.DataAccess.Entities;

namespace PersonalDiary.BL.Entities.Admins;

public class AdminsProvider : IAdminsProvider
{
    private readonly IRepository<AdminEntity> _repository;
    private readonly IMapper _mapper;

    public AdminsProvider(IRepository<AdminEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public AdminModel GetAdminInfo(Guid id)
    {
        var admin = _repository.GetById(id);
        if (admin == null)
        {
            throw new NotFoundException();
        }
        return _mapper.Map<AdminModel>(admin);
    }

    public IEnumerable<AdminModel> GetAdmins(AdminModelFilter? filter = null)
    {
        var login = filter?.Login;
        var email = filter?.Email;
        var admins = _repository.GetAll(x =>
            (login == null || x.Login == login) && (email == null || x.Email == email));
        return _mapper.Map<IEnumerable<AdminModel>>(admins);
    }
}
