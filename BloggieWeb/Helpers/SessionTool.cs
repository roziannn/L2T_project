namespace BloggieWeb.Helpers
{
    public class SessionTool
    {
        private readonly IHttpContextAccessor HttpContextAccessor;

        public SessionTool(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public void SetSession(string key, string value)
        {
            HttpContextAccessor.HttpContext!.Session.SetString(key, value);
        }

        public string GetSession(string key)
        {
            return HttpContextAccessor.HttpContext!.Session.GetString(key)!;
        }

        public void ClearSession()
        {
            HttpContextAccessor.HttpContext!.Session.Clear();
        }

    }
}
