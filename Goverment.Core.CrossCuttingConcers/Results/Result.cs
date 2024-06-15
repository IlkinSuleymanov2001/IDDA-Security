
namespace Goverment.Core.CrossCuttingConcers.Results
{
    public class Result
    {


        public bool Success { get; set; }
        public int? Status { get; set; }
        public object? Message { get; set; }

        public Result(bool success, int? status, object? message)
        {
            Success = success;
            Status = status;
            Message = message;
        }



    }
}
