using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Moq;
using NUnit.Framework;
using PersonalDiary.DataAccess;
using PersonalDiary.DataAccess.Entities;
using PersonalDiary.UnitTests.WebAPI.Helpers;

namespace PersonalDiary.UnitTests.WebAPI;

public class PersonalDiaryWebAPIBaseClass
{
    public PersonalDiaryWebAPIBaseClass()
    {
        var settings = TestSettingsHelper.GetSettings();

        _testServer = new TestWebApplicationFactory(services =>
        {
            services.Replace(ServiceDescriptor.Scoped(_ =>
            {
                var httpClientFactoryMock = new Mock<IHttpClientFactory>();
                httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(TestHttpClient);
                return httpClientFactoryMock.Object;
            }));
            services.PostConfigureAll<JwtBearerOptions>(options =>
            {
                options.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                    $"{settings.IdentityServerUri}/.well-known/openid-configuration",
                    new OpenIdConnectConfigurationRetriever(),
                    new HttpDocumentRetriever(TestHttpClient)
                    {
                        RequireHttps = false,
                        SendAdditionalHeaderData = true
                    });
            });
        });
    }

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        //using var scope = GetService<IServiceScopeFactory>().CreateScope();
        //var diaryEntryRepository = scope.ServiceProvider.GetRequiredService<IRepository<DiaryEntryEntity>>();
        //var diaryEntry = diaryEntryRepository.Save(new DiaryEntryEntity()
        //{
        //    Heading = "test",
        //    Text = "test",
        //    IsPublic = true
        //});;
        //TestDiaryEntryId = diaryEntry.Id;
    }

    public T? GetService<T>()
    {
        return _testServer.Services.GetRequiredService<T>();
    }

    private readonly WebApplicationFactory<Program> _testServer;
    protected int TestDiaryEntryId;
    protected HttpClient TestHttpClient => _testServer.CreateClient();
}
