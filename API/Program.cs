using Microsoft.AspNetCore.Authentication;

namespace API
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var config = builder.Configuration;
            var env = builder.Environment;

            services.AddControllers(o => o.SuppressAsyncSuffixInActionNames = false);
            services.AddStackExchangeRedisCache(o =>
            {
                o.Configuration = "";
            });
            services.AddAuthentication(UserAuthenticationHandler.SCHEME_NAME)
                            .AddScheme<AuthenticationSchemeOptions, UserAuthenticationHandler>(UserAuthenticationHandler.SCHEME_NAME, null);

            var app = builder.Build();

            if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();

            await app.RunAsync();

            return 0;
        }
    }
}
