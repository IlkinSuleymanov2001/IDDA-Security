using Microsoft.AspNetCore.Http;


namespace Goverment.Core.CrossCuttingConcers.Results
{
    public class SuccessDataResult : DataResult
    {
        public SuccessDataResult( int? status, string? message, object? data) : base(true, status, message, data)
        {
        }

        public SuccessDataResult(int? status, object? data) : base(true, status,"successfully operations", data)
        {

        }

        public SuccessDataResult(object? data) : base(true, StatusCodes.Status200OK, "successfully operations", data)
        {

        }

        public SuccessDataResult(object? data,string? message) : base(true, StatusCodes.Status200OK, message, data)
        {

        }
    }
}
