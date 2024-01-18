namespace API.Options
{
    public class SessionCookieOptions : CookieOptions
    {
        public const string COOKIE_NAME = "Session";
        public SessionCookieOptions()
        {
            IsEssential = true;
            Secure = true;
            HttpOnly = true;;
            SameSite = SameSiteMode.Strict;
        }
    }
}
