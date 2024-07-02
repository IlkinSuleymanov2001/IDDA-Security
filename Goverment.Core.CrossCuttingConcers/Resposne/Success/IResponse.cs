

namespace Goverment.Core.CrossCuttingConcers.Resposne.Success
{
    public interface IResponse
    {
        public string? Message { get; set; }
        public bool Success { get; }
    }
}
