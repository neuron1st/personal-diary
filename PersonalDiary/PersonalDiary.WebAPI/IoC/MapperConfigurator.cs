using PersonalDiary.BL.Mapper;
using PersonalDiary.WebAPI.Controllers.Mapper;

namespace PersonalDiary.WebAPI.IoC;

public class MapperConfigurator
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            config.AddProfile<AdminsBLProfile>();
            config.AddProfile<DiaryEntriesBLProfile>();
            config.AddProfile<FoldersBLProfile>();
            config.AddProfile<TagsBLProfile>();
            config.AddProfile<UsersBLProfile>();

            config.AddProfile<AdminsServiceProfile>();
            config.AddProfile<DiaryEntriesServiceProfile>();
            config.AddProfile<UsersServiceProfile>();  
        });
    }
}
