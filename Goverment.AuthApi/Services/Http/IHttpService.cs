using Goverment.Core.CrossCuttingConcers.Resposne.Success;

namespace Goverment.AuthApi.Services.Http
{
    public interface IHttpService
    {
        Task<T?> GetAsync<T>(string url, PathParam? pathParam=null, QueryParam? queryParam=null, string? token=null,bool Autotoken=false);
        Task PostAsync<TErrorModel>(string url, object bodyData, bool token = false) where TErrorModel : IMessage;
        Task PutAsync(string url, object data, bool token = false);
        Task DeleteAsync(string url, PathParam? pathParam = null, QueryParam? queryParam = null, bool token = false);
        Task DeleteWithDataAsync(string url, object data, bool token = false);
    }
}
