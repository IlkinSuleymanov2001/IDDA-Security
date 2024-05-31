using Goverment.AuthApi.Business.Abstracts;

namespace Goverment.AuthApi.Business.Concretes
{
    public class HttpClientManager : IHttpClientService
    {
        private readonly HttpClient _httpClient;

        public HttpClientManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async  Task<object> Get(string url)
        {
           /* var msg = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url)
            };

            var httpResponse = await _httpClient.SendAsync(msg);
            httpResponse.EnsureSuccessStatusCode();
            var resultContent = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<Citizen>>(resultContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false
            });
            _httpClient.Dispose();*/
            return default;



        }
    }
}
