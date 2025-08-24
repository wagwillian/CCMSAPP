namespace Portal.Authentication.Extensions
{
    public static class HttpClientExtensions 
    {
        public static void AddBearerToken(this HttpClient httpClient, string token)
        {
            if (httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                httpClient.DefaultRequestHeaders.Remove("Authorization");
            }
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }
}
