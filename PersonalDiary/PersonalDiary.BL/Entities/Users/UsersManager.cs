using AutoMapper;
using PersonalDiary.BL.Entities.Users.Entities;
using PersonalDiary.DataAccess;
using PersonalDiary.DataAccess.Entities;

namespace PersonalDiary.BL.Entities.Users;

public class UsersManager : IUsersManager
{
    private readonly IRepository<UserEntity> _repository;
    private readonly IMapper _mapper;

    public UsersManager(IRepository<UserEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public UserModel CreateUser(CreateUserModel model)
    {
        var entity = _mapper.Map<UserEntity>(model);
        _repository.Save(entity);
        return _mapper.Map<UserModel>(entity);
    }

    public void DeleteUser(Guid id)
    {
        var entity = _repository.GetById(id);
        if (entity == null)
        {
            throw new ArgumentException("not found");
        }
        _repository.Delete(entity);
    }

    public UserModel UpdateUser(Guid id, UpdateUserModel model)
    {
        var entity = _repository.GetById(id);
        if (entity == null)
        {
            throw new ArgumentException("not found");
        }
        entity.Login = model.Login;
        entity.Email = model.Email;
        _repository.Save(entity);
        return _mapper.Map<UserModel>(entity);
    }
}
