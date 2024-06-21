using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


namespace Goverment.Core.CrossCuttingConcers.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(int? status, string? message) : base(true, status, message)
        {

        }
        public SuccessResult(int? status) : base(true, status,"successfully operations")
        {

        }

    
        public SuccessResult( string? message) : base(true, StatusCodes.Status200OK, message)
        {

        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
