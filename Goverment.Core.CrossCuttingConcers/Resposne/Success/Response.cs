using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Goverment.Core.CrossCuttingConcers.Resposne.Success
{
    public class Response : IResponse
    {
        public string? Message { get; set; } = "Successfully Operation";
        public bool Success { get; set; } = true;

        public   Response(string? message)
        {
            Message = message;
        }

        public Response()
        {

        }

        public  static  IResponse Ok()
        {
            return new Response();  
        }
        public  static IResponse Ok(string message) 
        {
                return new Response(message);
        }

        public override string ToString()
        {

            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new LowercaseNamingStrategy()
                },
                Formatting = Formatting.Indented // Optional: makes the JSON output readable
            });

        }
    }
}
