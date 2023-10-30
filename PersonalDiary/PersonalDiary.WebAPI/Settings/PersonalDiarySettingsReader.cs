namespace PersonalDiary.WebAPI.Settings
{
    public class PersonalDiarySettingsReader
    {
        public static PersonalDiarySettings Read(IConfiguration configuration)
        {
            // здесь будет чтение настроек приложения из конфига
            return new PersonalDiarySettings();
        }
    }
}
