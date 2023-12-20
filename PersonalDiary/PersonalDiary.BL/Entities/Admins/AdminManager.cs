using AutoMapper;
using PersonalDiary.BL.Entities.Admins.Entities;
using PersonalDiary.DataAccess;
using PersonalDiary.DataAccess.Entities;

namespace PersonalDiary.BL.Entities.Admins;

public class AdminsManager : IAdminsManager
{
    private readonly IRepository<AdminEntity> _repository;
    private readonly IMapper _mapper;

    public AdminsManager(IRepository<AdminEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public AdminModel CreateAdmin(CreateAdminModel model)
    {
        var entity = _mapper.Map<AdminEntity>(model);
        _repository.Save(entity);
        return _mapper.Map<AdminModel>(entity);
    }

    public void DeleteAdmin(Guid id)
    {
        var entity = _repository.GetById(id);
        if (entity == null)
        {
            throw new ArgumentException("not found");
        }
        _repository.Delete(entity);
    }

    public AdminModel UpdateAdmin(Guid id, UpdateAdminModel model)
    {
        var entity = _repository.GetById(id);
        if (entity == null)
        {
            throw new ArgumentException("not found");
        }
        entity.Login = model.Login;
        entity.Email = model.Email;
        _repository.Save(entity);
        return _mapper.Map<AdminModel>(entity);
    }
}
