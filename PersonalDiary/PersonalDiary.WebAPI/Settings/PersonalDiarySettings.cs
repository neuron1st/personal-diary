namespace PersonalDiary.WebAPI.Settings
{
    public class PersonalDiarySettings
    {
        public Uri ServiceUri { get; set; }
        public string PersonalDiaryDbContextConnectionString { get; set; }
        public string IdentityServerUri { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
