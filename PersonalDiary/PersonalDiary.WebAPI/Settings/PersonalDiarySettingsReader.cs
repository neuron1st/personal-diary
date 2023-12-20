namespace PersonalDiary.WebAPI.Settings
{
    public class PersonalDiarySettingsReader
    {
        public static PersonalDiarySettings Read(IConfiguration configuration)
        {
            return new PersonalDiarySettings()
            {
                PersonalDiaryDbContextConnectionString = configuration.GetValue<string>("PersonalDiaryDbContext")
            };
        }
    }
}
