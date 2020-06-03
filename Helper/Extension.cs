using Microsoft.AspNetCore.Http;

namespace DatingApplication.Helper
{
    public static class Extension
    {
        public static void AddApplciationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}