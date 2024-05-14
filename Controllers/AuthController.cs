using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using TecApi.Dtos;
using TecApi.Models;

namespace TecApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly HttpClient _client;

    public AuthController(HttpClient client)
    {
        _client = client;
    }

    [HttpPost("/login")]
    public async Task<AuthResponseDto> Login(AuthRequestDto authRequest)
    {
        Console.WriteLine(authRequest);
        
        try
        {
            var args = new
            {
                authRequest.Email,
                authRequest.Password,
                returnSecureToken = true,
            };

            var response = await _client.PostAsJsonAsync(
                "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=AIzaSyA60ILDVmASWsNK8HNAfGwcVhZiE4UXNxg",
                args
            );
            
            var content = await response.Content.ReadFromJsonAsync<Auth>();
            
            Console.WriteLine(content);

            return new AuthResponseDto
            {
                Success = response.IsSuccessStatusCode,
                Uid = content?.LocalId,
                Token = content?.IdToken,
                Message = response.IsSuccessStatusCode ? null : "Credenciales incorrectas"
            };
        }
        catch (Exception e)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = e.Message
            };
        }
    }

    [HttpPost("/register")]
    public async Task<AuthResponseDto> Register(AuthRequestDto authRequest)
    {
        var args = new UserRecordArgs
        {
            Email = authRequest.Email,
            Password = authRequest.Password,
        };

        try
        {
            var exist = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(authRequest.Email);

            if (exist != null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Correo ya registrado"
                };
            }
        }
        catch (Exception e)
        {
            // ignored
        }

        var user = await FirebaseAuth.DefaultInstance.CreateUserAsync(args);

        return new AuthResponseDto
        {
            Success = true,
            Uid = user.Uid,
        };
    }
}