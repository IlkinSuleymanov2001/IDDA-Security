using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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

        public override string ToString() 
        {

            return   JsonConvert.SerializeObject(this , new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new LowercaseNamingStrategy()
                },
                Formatting = Formatting.Indented // Optional: makes the JSON output readable
            });
        }

      

    }

    public class LowercaseNamingStrategy : NamingStrategy
    {
        protected override string ResolvePropertyName(string name)
        {
            // Convert property names to lowercase
            return name.ToLower();
        }
    }
}
