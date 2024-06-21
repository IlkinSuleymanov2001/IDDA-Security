
using Newtonsoft.Json;

namespace Goverment.Core.CrossCuttingConcers.Results
{
    public class DataResult:Result
    {
        public DataResult(bool success, int? status, object? message,object? data) : base(success, status, message)
        {
            Data =data;
        }

        public object? Data { get; set; }

    }

}
