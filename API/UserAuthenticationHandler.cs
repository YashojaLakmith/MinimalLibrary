using System.Security.Claims;
using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace API
{
    public class UserAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IDistributedCache _cache;
        public const string SCHEME_NAME = "USER AUTHENTICATION SCHEME";

        public UserAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, IDistributedCache cache, UrlEncoder encoder) : base(options, logger, encoder)
        {
            _cache = cache;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                if (!Request.Cookies.TryGetValue("session", out string? sessionToken))
                {
                    return AuthenticateResult.Fail("No session value");
                }

                var userId = await _cache.GetStringAsync(sessionToken);
                if (userId is null)
                {
                    return AuthenticateResult.Fail("Invalid session");
                }

                await _cache.RefreshAsync(sessionToken);

                var claim = new Claim(ClaimTypes.NameIdentifier, userId);
                var identity = new ClaimsIdentity(new[] { claim }, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                AuthenticationTicket ticket = new(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            catch (Exception)
            {
                return AuthenticateResult.Fail("An error occured");
            }
        }
    }
}
