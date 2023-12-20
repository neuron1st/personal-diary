using PersonalDiary.BL.Auth.Entites;

namespace PersonalDiary.BL.Auth;

public interface IAuthProvider
{
    Task<TokensResponse> AuthorizeUser(string email, string password);
    Task RegisterUser(string name, string email, string password);
}
