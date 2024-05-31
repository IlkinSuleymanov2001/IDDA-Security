
namespace Goverment.AuthApi.Business.Abstracts
{
    public interface  IHttpClientService
    {
       Task<object> Get(string url);
    }
}
