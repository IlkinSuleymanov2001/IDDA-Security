using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Goverment.Core.CrossCuttingConcers.Resposne.Success
{
    public class Response : IResponse
    {
        public string? Message { get; set; } = "Successfully Operation";
        public bool Success { get { return true; } }

        public Response(string? message)
        {
            Message = message;
        }

        public Response()
        {

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
