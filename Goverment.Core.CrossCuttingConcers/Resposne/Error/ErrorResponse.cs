using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Goverment.Core.CrossCuttingConcers.Resposne.Success;

namespace Goverment.Core.CrossCuttingConcers.Resposne.Error
{
    public class ErrorResponse : IResponse
    {
        public string? Message { get; set; }
        public bool Success { get { return false; } }

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
