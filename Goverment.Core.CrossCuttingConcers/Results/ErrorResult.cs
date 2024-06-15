using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goverment.Core.CrossCuttingConcers.Results
{
    public class ErrorResult : Result
    {
        private string _url = "https://govermentauthapi20240610022027.azurewebsites.net";
        public DateTime TimeSpam { get ;}= DateTime.UtcNow;
        public string? Url { get { return _url; } set { _url += value; } }
        public string? Title { get; set; }

        public ErrorResult(int? status, object? message, string? url, string? title = "Business exception") : base(false, status, message)
        {
            Url = url;
            Title = title;
        }

        public ErrorResult(object? message,string? url,string? title = "Business exception") : this(StatusCodes.Status400BadRequest, message,url,title)
        {

        }

        public override string ToString() => JsonConvert.SerializeObject(this);

    }
}
