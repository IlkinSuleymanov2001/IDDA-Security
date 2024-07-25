using Goverment.Core.CrossCuttingConcers.Resposne.Success;

namespace Goverment.AuthApi.Services.Http
{
    public interface IHttpService
    {
        Task<T?> GetAsync<T,TErrorModel>(string url, PathParam? pathParam=null, QueryParam? queryParam=null, string? token=null,bool autoToken=false) where TErrorModel : IMessage;
        Task<T?> GetAsync<T>(string url, PathParam? pathParam=null, QueryParam? queryParam=null, string? token=null,bool autoToken=false);
        Task PostAsync<TErrorModel>(string url, object bodyData, bool token = false) where TErrorModel : IMessage;
        Task PutAsync(string url, object data, bool token = false);
        Task DeleteAsync<TErrorModel>(string url, PathParam? pathParam = null, QueryParam? queryParam = null, bool autoToken = false) where TErrorModel : IMessage;
        Task DeleteWithDataAsync<TErrorModel>(string url, object data, bool autoToken = false) where TErrorModel : IMessage;
    }
}
