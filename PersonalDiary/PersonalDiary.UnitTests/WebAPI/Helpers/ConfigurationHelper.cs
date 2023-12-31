﻿using Microsoft.Extensions.Configuration;

namespace PersonalDiary.UnitTests.WebAPI.Helpers;

public class ConfigurationHelper
{
    public static IConfiguration GetConfiguration()
    {
        return new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
    }
}
