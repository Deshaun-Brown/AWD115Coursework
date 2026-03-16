Goal: Configure external authentication providers (Google, Microsoft, Instagram) for this Razor/Identity app and verify they work locally.

Prerequisites
- You must register OAuth apps at each provider and obtain ClientId/ClientSecret.
- Know your app's HTTPS port (see Properties/launchSettings.json or the Visual Studio debug dropdown).

Redirect URIs to register at each provider (use your HTTPS port):
- Google:  https://localhost:{HTTPS_PORT}/signin-google
- Microsoft: https://localhost:{HTTPS_PORT}/signin-microsoft
- Instagram (via Facebook/Meta dev portal): https://localhost:{HTTPS_PORT}/signin-instagram

Step-by-step (line-by-line)

1) Install required NuGet packages
- Open PowerShell in the project folder and run:
  dotnet add package Microsoft.AspNetCore.Authentication.Google
  dotnet add package Microsoft.AspNetCore.Authentication.MicrosoftAccount
  dotnet add package AspNet.Security.OAuth.Providers

2) Store secrets (recommended: user-secrets for development)
- Initialize user secrets for the project (run once):
  dotnet user-secrets init
- Add each provider's credentials (replace placeholders):
  dotnet user-secrets set "Authentication:Google:ClientId" "<GOOGLE_CLIENT_ID>"
  dotnet user-secrets set "Authentication:Google:ClientSecret" "<GOOGLE_CLIENT_SECRET>"

  dotnet user-secrets set "Authentication:Microsoft:ClientId" "<MICROSOFT_CLIENT_ID>"
  dotnet user-secrets set "Authentication:Microsoft:ClientSecret" "<MICROSOFT_CLIENT_SECRET>"

  dotnet user-secrets set "Authentication:Instagram:ClientId" "<INSTAGRAM_CLIENT_ID>"
  dotnet user-secrets set "Authentication:Instagram:ClientSecret" "<INSTAGRAM_CLIENT_SECRET>"

3) Update Program.cs to register providers
- Open `Program.cs` and locate where services are configured (before builder.Build()).
- Add the following lines after you configure Identity (example shows exact placement):

  // --- External authentication providers start ---
  builder.Services.AddAuthentication()
      .AddGoogle(googleOptions =>
      {
          googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
          googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
      })
      .AddMicrosoftAccount(msOptions =>
      {
          msOptions.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"];
          msOptions.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"];
      })
      .AddInstagram(inst =>
      {
          inst.ClientId = builder.Configuration["Authentication:Instagram:ClientId"];
          inst.ClientSecret = builder.Configuration["Authentication:Instagram:ClientSecret"];
      });
  // --- External authentication providers end ---

- Note: the `AddInstagram` extension comes from the `AspNet.Security.OAuth.Providers` package.

4) Ensure middleware order (already present in this project)
- `app.UseAuthentication();` must be called before `app.UseAuthorization();` (your Program.cs already does this).

5) Redirect/callback paths
- Default callback paths used by the providers are:
  - Google: /signin-google
  - Microsoft: /signin-microsoft
  - Instagram: /signin-instagram
- Ensure these exact paths were registered at provider dashboards.

6) Trust the development certificate (if not already trusted)
- dotnet dev-certs https --trust

7) Run the app and test
- Start the app with Visual Studio or `dotnet run`.
- Open the login page (/Identity/Account/Login) — the external provider buttons should appear under "Use another service to log in.".
- Click a provider, complete the OAuth consent, and the app will handle the callback.

Troubleshooting
- "There are no external authentication services configured." — Confirm packages installed and the `AddGoogle` / other registration lines are present in Program.cs and the keys exist in configuration.
- "Invalid redirect URI" — Verify redirect URIs registered at the provider match your app URL and callback path (including port and https prefix).
- See console logs: run `dotnet run` to view startup errors and exceptions.

Notes & Next steps
- In production do not use user-secrets; store secrets in a secure store (Azure Key Vault, environment variables, etc.).
- If you want automatic display names or icons, you can add DisplayName configuration in the AddX options.

If you want, I can now:
- a) Add the exact Program.cs snippet into your `Program.cs` (I will not change it until you confirm you want me to); or
- b) Run the package-install and user-secrets commands for you (I can create a PS script to run locally).

End of code-along file.
