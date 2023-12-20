namespace PersonalDiary.WebAPI.Settings
{
    public class PersonalDiarySettingsReader
    {
        public static PersonalDiarySettings Read(IConfiguration configuration)
        {
            return new PersonalDiarySettings()
            {
                ServiceUri = configuration.GetValue<Uri>("Uri"),
                PersonalDiaryDbContextConnectionString = configuration.GetValue<string>("PersonalDiaryDbContext"),
                IdentityServerUri = configuration.GetValue<string>("IdentityServerSettings:Uri"),
                ClientId = configuration.GetValue<string>("IdentityServerSettings:ClientId"),
                ClientSecret = configuration.GetValue<string>("IdentityServerSettings:ClientSecret"),
            };
        }
    }
}
