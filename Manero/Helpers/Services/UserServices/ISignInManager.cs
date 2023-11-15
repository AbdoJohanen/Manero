using Microsoft.AspNetCore.Identity;

namespace Manero.Helpers.Services.UserServices;

public interface ISignInManager
{
    Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);
}
public class SignInManager : ISignInManager
{
    public Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
    {
        return Task.FromResult(SignInResult.Success);
    }
}
