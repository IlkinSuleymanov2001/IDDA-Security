
namespace Goverment.Core.CrossCuttingConcers.Resposne.Success
{
    public interface IResponse:IMessage
    {
        public bool Success { get; }
    }

    public interface IMessage
    {
        public string? Message { get; set; }
    }


}
