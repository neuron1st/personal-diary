using AutoMapper;
using PersonalDiary.BL.Entities.Users.Entities;
using PersonalDiary.DataAccess;
using PersonalDiary.DataAccess.Entities;

namespace PersonalDiary.BL.Entities.Users;

public class UsersProvider : IUsersProvider
{
    private readonly IRepository<UserEntity> _repository;
    private readonly IMapper _mapper;

    public UsersProvider(IRepository<UserEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public UserModel GetUserInfo(Guid id)
    {
        var admin = _repository.GetById(id);
        if (admin == null)
        {
            throw new ArgumentException("not found");
        }
        return _mapper.Map<UserModel>(admin);
    }

    public IEnumerable<UserModel> GetUsers(UserModelFilter? filter = null)
    {
        var login = filter?.Login;
        var email = filter?.Email;
        var admins = _repository.GetAll(x =>
            (login == null || x.Login == login) && (email == null || x.Email == email));
        return _mapper.Map<IEnumerable<UserModel>>(admins);
    }
}
