using Microsoft.EntityFrameworkCore;
using PersonalDiary.DataAccess;
using PersonalDiary.WebAPI.Settings;

namespace PersonalDiary.WebAPI.IoC;

public static class DbContextConfigurator
{
    public static void ConfigureService(IServiceCollection services, PersonalDiarySettings settings)
    {
        services.AddDbContextFactory<PersonalDiaryDbContext>(
            options => { options.UseSqlServer(settings.PersonalDiaryDbContextConnectionString); },
            ServiceLifetime.Scoped);
    }

    public static void ConfigureApplication(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<PersonalDiaryDbContext>>();
        using var context = contextFactory.CreateDbContext();
        context.Database.Migrate(); //makes last migrations to db and creates database if it doesn't exist
    }
}
