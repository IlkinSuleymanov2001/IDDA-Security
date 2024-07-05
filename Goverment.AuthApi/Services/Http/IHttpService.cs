namespace Goverment.AuthApi.Services.Http
{
    public interface IHttpService
    {
        Task<T> GetAsync<T>(string url, PathParam? pathParam=null, QueryParam? queryParam=null, string? token=null,bool Autotoken=false);
        Task<T> PostAsync<T>(string url, object data, bool token = false);
        Task<T> PutAsync<T>(string url, object data, bool token = false);
        Task<T> DeleteAsync<T>(string url, PathParam? pathParam = null, QueryParam? queryParam = null, bool token = false);
        Task<T> DeleteWithDataAsync<T>(string url, object data, bool token = false);
    }
}
