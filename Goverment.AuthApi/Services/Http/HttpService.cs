using Core.Security.JWT;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using Core.CrossCuttingConcerns.Exceptions;
using Goverment.Core.CrossCuttingConcers.Resposne.Success;
using Azure;
using Goverment.Core.CrossCuttingConcers.Exceptions;

namespace Goverment.AuthApi.Services.Http
{
    public class HttpService(HttpClient httpClient, ITokenHelper tokenHelper) : IHttpService
    {
        private void SetAuthorizationHeader()
        {
            var jwtToken = tokenHelper.GetToken();
            if (!jwtToken.IsNullOrEmpty())
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        }

        public async Task DeleteAsync(string url, PathParam? pathParam = null, QueryParam? queryParam = null, bool token = false)
        {
            if (token) SetAuthorizationHeader();
            url=  BuildUrl(url, pathParam, queryParam);
            using var response = await httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }
        public async Task DeleteWithDataAsync(string url, object bodyData, bool token = false)
        {
            if (token) SetAuthorizationHeader();
            var content = new StringContent(JsonConvert.SerializeObject(bodyData), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(url),
                Content = content
            };
            using var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
        public async Task<T?> GetAsync<T,TErrorModel>(string url, PathParam? pathParam = null,
                                         QueryParam? queryParam = null,
                                        string? token = null, bool autoToken = false) where TErrorModel:IMessage
        {
            if (autoToken) SetAuthorizationHeader();
            else if (token != null) httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            url = BuildUrl(url, pathParam, queryParam);
            using var response = await httpClient.GetAsync(url);
            if(!response.IsSuccessStatusCode) await CatchException<TErrorModel>(response);
            return await ReadAsStream<T>(response);

        }

        public async Task<T?> GetAsync<T>(string url, PathParam? pathParam = null,
            QueryParam? queryParam = null,
            string? token = null, bool autoToken = false) 
        {
            if (autoToken) SetAuthorizationHeader();
            else if (token != null) httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            url = BuildUrl(url, pathParam, queryParam);
            using var response = await httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode) response.EnsureSuccessStatusCode();
            return await ReadAsStream<T>(response);

        }

        public async Task PostAsync<TErrorModel>(string url, object bodyData, bool token = false) where TErrorModel: IMessage
        {
            if (token) SetAuthorizationHeader();
            var content = new StringContent(JsonConvert.SerializeObject(bodyData), Encoding.UTF8, "application/json");
            using var response = await httpClient.PostAsync(url, content);
            await CatchException<TErrorModel>(response);



        }

        public async Task PutAsync(string url, object bodyData, bool token = false)
        {
            if (token) SetAuthorizationHeader();
            var content = new StringContent(JsonConvert.SerializeObject(bodyData), Encoding.UTF8, "application/json");
            using var response = await httpClient.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
        }

        private  T? GetJsonConvert<T>(JsonTextReader json)
        {

            var jsonSerializer = new JsonSerializer
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new DefaultNamingStrategy()
                },
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate
            };
            return jsonSerializer.Deserialize<T>(json);
        }

        private async Task<T?> ReadAsStream<T>(HttpResponseMessage response) 
        {
            await using var stream = await response.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(stream);
            await using var jsonReader = new JsonTextReader(streamReader);
            return GetJsonConvert<T>(jsonReader);
        }

        private async Task<T?> ReadAsString<T>(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            response.Dispose();
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy
                    {
                        ProcessDictionaryKeys = true,
                        OverrideSpecifiedNames = false
                    }
                }
            };
            return JsonConvert.DeserializeObject<T>(json, settings);
        }
        private string BuildUrl(string url, PathParam? pathParam, QueryParam? queryParam)
        {
            if (pathParam != null)
                url += $"/{pathParam.Data}";
            else if (queryParam != null)
                url += $"?{queryParam.QueryParamName}={JsonConvert.SerializeObject(queryParam.Data)}";
            return url;
        }

        private async Task CatchException<TErrorModel>(HttpResponseMessage response)where TErrorModel: IMessage
        {
            if (!response.IsSuccessStatusCode)
            {
                
                switch ((int)response.StatusCode)
                {
                    case 400:
                        var errorModel = await ReadAsString<TErrorModel>(response);
                        throw new BusinessException(errorModel?.Message ?? "Bad Request");
                    case 404:
                         errorModel = await ReadAsString<TErrorModel>(response);
                        throw new BusinessException(errorModel?.Message ?? "Not Found");
                    case 409:
                        errorModel = await ReadAsString<TErrorModel>(response);
                        throw new UnVerifyOrDuplicatedException(errorModel?.Message ?? "Duplicated data");
                    case 401:
                        throw new AuthorizationException();
                    case 403:
                        throw new ForbiddenException();
                    default:
                        response.EnsureSuccessStatusCode();
                        break;
                }
            }
        }
    }
}
