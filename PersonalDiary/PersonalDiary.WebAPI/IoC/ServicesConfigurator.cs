using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PersonalDiary.BL.Auth;
using PersonalDiary.BL.Entities.DiaryEntries;
using PersonalDiary.BL.Entities.Folders;
using PersonalDiary.BL.Entities.Tags;
using PersonalDiary.BL.Entities.Users;
using PersonalDiary.BL.Entities.Users.Entities;
using PersonalDiary.DataAccess;
using PersonalDiary.DataAccess.Entities;
using PersonalDiary.WebAPI.Settings;

namespace PersonalDiary.WebAPI.IoC;

public static class ServicesConfigurator
{
    public static void ConfigureService(IServiceCollection services, PersonalDiarySettings settings)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUsersProvider>(x =>
            new UsersProvider(x.GetRequiredService<IRepository<UserEntity>>(), x.GetRequiredService<IMapper>()));
        services.AddScoped<IAuthProvider>(x =>
                new AuthProvider(x.GetRequiredService<SignInManager<UserEntity>>(),
                x.GetRequiredService<UserManager<UserEntity>>(),
                x.GetRequiredService<IHttpClientFactory>(),
                settings.IdentityServerUri,
                settings.ClientId,
                settings.ClientSecret));
        services.AddScoped<IUsersManager, UsersManager>();
        services.AddScoped<IDiaryEntriesManager, DiaryEntriesManager>();
        services.AddScoped<IFoldersManager, FoldersManager>();
        services.AddScoped<ITagsManager, TagsManager>();
    }
}
