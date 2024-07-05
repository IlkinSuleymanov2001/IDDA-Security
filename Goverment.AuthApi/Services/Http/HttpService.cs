
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.JWT;
using Goverment.AuthApi.Commans.Constants;
using Goverment.Core.CrossCuttingConcers.Resposne.Error;
using Goverment.Core.CrossCuttingConcers.Resposne.Success;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Net.Http.Headers;
using System.Text;

namespace Goverment.AuthApi.Services.Http
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenHelper _tokenHelper;

        public HttpService(HttpClient httpClient, ITokenHelper tokenHelper)
        {
            _httpClient = httpClient;
            _tokenHelper = tokenHelper;
        }

        private void SetAuthorizationHeader()
        {
            var jwtToken = _tokenHelper.GetToken();
            if (!jwtToken.IsNullOrEmpty())
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        }

        public async Task<T> DeleteAsync<T>(string url, PathParam? pathParam = null, QueryParam? queryParam = null, bool token = false)
        {
            if (token) SetAuthorizationHeader();
            if (pathParam != null)
            {
                url += $"/{pathParam.Data}"; // Assuming data is appended to URL path
            }
            else if (queryParam != null)
            {
                var queryString = JsonConvert.SerializeObject(queryParam.Data);
                url += $"?query={queryString}"; // Example: Sending data as query parameter
            }
            var response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
        public async Task<T> DeleteWithDataAsync<T>(string url, object data, bool token = false)
        {
            if (token) SetAuthorizationHeader();
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(url),
                Content = content
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
        public async Task<T> GetAsync<T>(string url, PathParam? pathParam = null,
                                         QueryParam? queryParam = null,
                                        string? token = null, bool Autotoken = false)
        {
             if (Autotoken)
                SetAuthorizationHeader();
             else if (token!=null) 
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
           
            if (pathParam != null)
            {
                url += $"/{pathParam.Data}"; // Assuming data is appended to URL path
            }
            else if (queryParam != null) 
            {
                var queryString = JsonConvert.SerializeObject(queryParam.Data);
                url += $"?{queryParam.QueryParamName}={queryString}"; // Example: Sending data as query parameter
            }
            string? jsonString = default;
            using (var response = await _httpClient.GetAsync(url)) 
            {
                response.EnsureSuccessStatusCode();
                jsonString = await response.Content.ReadAsStringAsync();
            }
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public async Task<T> PostAsync<T>(string url, object bodyData, bool token = false)
        {
            if(token) SetAuthorizationHeader();
            var content = new StringContent(JsonConvert.SerializeObject(bodyData), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public async Task<T> PutAsync<T>(string url, object bodyData, bool token = false)
        {
            if (token) SetAuthorizationHeader();
            var content = new StringContent(JsonConvert.SerializeObject(bodyData), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
