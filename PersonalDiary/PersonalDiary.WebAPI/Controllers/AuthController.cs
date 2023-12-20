using Microsoft.AspNetCore.Mvc;
using PersonalDiary.BL.Auth;

namespace PersonalDiary.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IAuthProvider authProvider) : ControllerBase
{
    private readonly IAuthProvider _authProvider = authProvider;

    [HttpGet]
    [Route("login")]
    public async Task<IActionResult> LoginUser([FromQuery] string email, [FromQuery] string password)
    {
        try
        {
            var tokens = await _authProvider.AuthorizeUser(email, password);

            return Ok(tokens);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterUser(string name, string email, string password)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _authProvider.RegisterUser(name, email, password);

            return Ok("Пользователь успешно зарегистрирован.");
        }
        catch (UserCreationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest($"Непредвиденная ошибка: {ex.Message}");
        }
    }
}
