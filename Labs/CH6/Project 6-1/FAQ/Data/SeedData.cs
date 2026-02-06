using Microsoft.EntityFrameworkCore;

namespace FAQ.Data;

public static class SeedData
{
    public static void Initialize(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<FaqContext>();
        context.Database.Migrate();
    }
}
