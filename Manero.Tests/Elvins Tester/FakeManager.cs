using Manero.Models.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Manero.Tests;

public class FakeUserManager : UserManager<AppUser>
{
    public FakeUserManager()
        : base(new Mock<IUserStore<AppUser>>().Object,
               new Mock<IOptions<IdentityOptions>>().Object,
               new Mock<IPasswordHasher<AppUser>>().Object,
               new IUserValidator<AppUser>[0],
               new IPasswordValidator<AppUser>[0],
               new Mock<ILookupNormalizer>().Object,
               new Mock<IdentityErrorDescriber>().Object,
               new Mock<IServiceProvider>().Object,
               new Mock<ILogger<UserManager<AppUser>>>().Object)
    { }
}

public class FakeSignInManager : SignInManager<AppUser>
{
    public FakeSignInManager()
        : base(new FakeUserManager(),
               new Mock<IHttpContextAccessor>().Object,
               new Mock<IUserClaimsPrincipalFactory<AppUser>>().Object,
               new Mock<IOptions<IdentityOptions>>().Object,
               new Mock<ILogger<SignInManager<AppUser>>>().Object,
               new Mock<IAuthenticationSchemeProvider>().Object,
               new Mock<IUserConfirmation<AppUser>>().Object)
    { }
}