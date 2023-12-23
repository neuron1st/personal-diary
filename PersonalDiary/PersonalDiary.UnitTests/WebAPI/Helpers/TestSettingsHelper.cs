using PersonalDiary.WebAPI.Settings;

namespace PersonalDiary.UnitTests.WebAPI.Helpers;

public class TestSettingsHelper
{
    public static PersonalDiarySettings GetSettings()
    {
        return PersonalDiarySettingsReader.Read(ConfigurationHelper.GetConfiguration());
    }
}
