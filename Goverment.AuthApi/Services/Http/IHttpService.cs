namespace Goverment.AuthApi.Services.Http
{
    public interface IHttpService
    {
        Task<T?> GetAsync<T>(string url, PathParam? pathParam=null, QueryParam? queryParam=null, string? token=null,bool Autotoken=false);
        Task PostAsync(string url, object data, bool token = false);
        Task PutAsync(string url, object data, bool token = false);
        Task DeleteAsync(string url, PathParam? pathParam = null, QueryParam? queryParam = null, bool token = false);
        Task DeleteWithDataAsync(string url, object data, bool token = false);
    }
}
