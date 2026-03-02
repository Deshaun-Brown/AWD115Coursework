using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Fitness_Tracker.Controllers;

[Route("/diagnostics/identity")]
[ApiController]
public class IdentityDiagnosticsController : ControllerBase
{
    private readonly IdentityOptions _options;

    public IdentityDiagnosticsController(IOptions<IdentityOptions> options)
    {
        _options = options.Value;
    }

    // GET /diagnostics/identity
    [HttpGet]
    public IActionResult Get()
    {
        // Return a compact view of the important Identity options so you can inspect them at runtime.
        var payload = new
        {
            User = new
            {
                RequireUniqueEmail = _options.User.RequireUniqueEmail,
                AllowedUserNameCharacters = _options.User.AllowedUserNameCharacters
            },
            Password = new
            {
                RequireDigit = _options.Password.RequireDigit,
                RequiredLength = _options.Password.RequiredLength,
                RequireNonAlphanumeric = _options.Password.RequireNonAlphanumeric,
                RequireUppercase = _options.Password.RequireUppercase,
                RequireLowercase = _options.Password.RequireLowercase
            },
            Lockout = new
            {
                MaxFailedAccessAttempts = _options.Lockout.MaxFailedAccessAttempts,
                DefaultLockoutTimeSpan = _options.Lockout.DefaultLockoutTimeSpan
            },
            SignIn = new
            {
                RequireConfirmedEmail = _options.SignIn.RequireConfirmedEmail,
                RequireConfirmedPhoneNumber = _options.SignIn.RequireConfirmedPhoneNumber
            }
        };

        return Ok(payload);
    }
}
