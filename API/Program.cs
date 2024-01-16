using DataLayer;

using Domain.DataAccess;
using Domain.Services;
using Domain.Services.DefaultImplementations;

using Microsoft.AspNetCore.Authentication;

using MongoDB.Driver;

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

            var mongoDbConnString = config["dbConnString"];     // For local development only
            var redisConnString = config["redisConnString"];

            ArgumentNullException.ThrowIfNull(nameof(mongoDbConnString));
            ArgumentNullException.ThrowIfNull(nameof(redisConnString));

            services.AddControllers(o => o.SuppressAsyncSuffixInActionNames = false);
            services.AddStackExchangeRedisCache(o => o.Configuration = redisConnString);        // redis connection string
            services.AddSingleton<IMongoClient>(new MongoClient(mongoDbConnString));            // mongodb connection string
            services.AddAuthentication(UserAuthenticationHandler.SCHEME_NAME)
                            .AddScheme<AuthenticationSchemeOptions, UserAuthenticationHandler>(UserAuthenticationHandler.SCHEME_NAME, null);

            services.AddScoped<IUserDataAccess, UserDataAccess>();
            services.AddScoped<IBookDataAccess, BookDataAccess>();
            services.AddScoped<IUserAccountService, DefaultUserAccountService>();
            services.AddScoped<IBookService, DefaultBookService>();
            services.AddScoped<IBorrowingService, DefaultBorrowingService>();

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
