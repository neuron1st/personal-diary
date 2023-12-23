using FluentAssertions;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using PersonalDiary.BL.Auth.Entites;
using PersonalDiary.DataAccess;
using PersonalDiary.DataAccess.Entities;
using System.Net;

namespace PersonalDiary.UnitTests.WebAPI.Users;

public class LoginUserTests : PersonalDiaryWebAPIBaseClass
{
    [Test]
    public async Task SuccessFullResult()
    {
        var user = new UserEntity()
        {
            Email = "test@test",
            UserName = "test",
        };
        var password = "Password1@";

        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
        var result = await userManager.CreateAsync(user, password);

        var query = $"?email={user.UserName}&password={password}";
        var requestedUri = PersonalDiaryApiEndpoints.AuthorizeUserEndpoint + query;
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestedUri);
        var client = TestHttpClient;
        var response = await client.SendAsync(request);

        var responseContentJson = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<TokensResponse>(responseContentJson);

        content.Should().NotBeNull();
        content.AccessToken.Should().NotBeNull();
        content.RefreshToken.Should().NotBeNull();

        var requestToGetAllDiaryEntries =
            new HttpRequestMessage(HttpMethod.Get, PersonalDiaryApiEndpoints.GetAllDiaryEntriesEndpoint);

        var clientWithToken = TestHttpClient;
        client.SetBearerToken(content.AccessToken);
        var getAllUsersResponse = await client.SendAsync(requestToGetAllDiaryEntries);

        getAllUsersResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Test]
    public async Task BadRequestUserNotFoudResultTest()
    {
        var login = "not_existing@mail.ru";
        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IRepository<UserEntity>>();
        var user = userRepository.GetAll().FirstOrDefault(x => x.UserName.ToLower() == login.ToLower());
        if (user != null)
        {
            userRepository.Delete(user);
        }

        var password = "password";

        var query = $"?email={login}&password={password}";
        var requestUri = PersonalDiaryApiEndpoints.AuthorizeUserEndpoint + query;
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var response = await TestHttpClient.SendAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task PasswordIsIncorrectResultTest()
    {
        var user = new UserEntity()
        {
            Email = "test@test",
            UserName = "test@test",
        };
        var password = "password";

        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        var userManager = scope.ServiceProvider.GetService<UserManager<UserEntity>>();
        await userManager.CreateAsync(user, password);

        var incorrect_password = "kjdfdfsdf";

        var query = $"?email={user.UserName}&password={incorrect_password}";
        var requestUri = PersonalDiaryApiEndpoints.AuthorizeUserEndpoint + query;
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var client = TestHttpClient;
        var response = await client.SendAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Test]
    [TestCase("", "")]
    [TestCase("qwe", "")]
    [TestCase("test@test", "")]
    [TestCase("", "password")]
    public async Task LoginOrPasswordAreInvalidResultTest(string login, string password)
    {
        var query = $"?login={login}&password={password}";
        var requestUri = PersonalDiaryApiEndpoints.AuthorizeUserEndpoint + query;
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var client = TestHttpClient;
        var response = await client.SendAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
