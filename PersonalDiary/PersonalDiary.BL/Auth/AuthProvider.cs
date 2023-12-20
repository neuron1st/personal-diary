using Duende.IdentityServer.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using PersonalDiary.BL.Auth.Entites;
using PersonalDiary.DataAccess.Entities;

namespace PersonalDiary.BL.Auth;

public class AuthProvider(SignInManager<UserEntity> signInManager,
    UserManager<UserEntity> userManager,
    IHttpClientFactory httpClientFactory,
    string identityServerUri,
    string clientId,
    string clientSecret) : IAuthProvider
{
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly string _identityServerUri = identityServerUri;
    private readonly string _cliendId = clientId;
    private readonly string _clientSecret = clientSecret;

    public async Task<TokensResponse> AuthorizeUser(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            throw new UserNotFoundException("The user was not found.");
        }
        var verificationPasswordResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);

        if (!verificationPasswordResult.Succeeded)
        {
            throw new LoginOrPasswordInIncorrectException("The username or password is incorrect.");
        }

        var client = _httpClientFactory.CreateClient();
        var discoveryDoc = await client.GetDiscoveryDocumentAsync(_identityServerUri);

        if (discoveryDoc.IsError)
        {
            throw new DiscoveryDocumentException("An error occurred while receiving the Discovery Document.");
        }

        var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest()
        {
            Address = discoveryDoc.TokenEndpoint,
            GrantType = GrantType.ResourceOwnerPassword,
            ClientId = _cliendId,
            ClientSecret = _clientSecret,
            UserName = user.UserName,
            Password = password,
            Scope = "api offline_access"
        });

        if (tokenResponse.IsError)
        {
            throw new TokenRequestException("An error occurred when requesting the token.");
        }

        return new TokensResponse()
        {
            AccessToken = tokenResponse.AccessToken,
            RefreshToken = tokenResponse.RefreshToken
        };
    }

    public async Task RegisterUser(string userName, string email, string password)
    {
        var existingUser = await _userManager.FindByEmailAsync(email);

        if (existingUser != null)
        {
            throw new UserCreationException("User is already exist");
        }

        UserEntity user = new()
        {
            UserName = userName,
            Email = email
        };

        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);

        var createUserResult = await _userManager.CreateAsync(user);

        if (!createUserResult.Succeeded)
        {
            throw new UserCreationException("Error while creating user");
        }

        return;
    }
}
