using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Goverment.Core.CrossCuttingConcers.Resposne.Success
{
    public class DataResponse<Type> : Response, IDataResponse<Type>
    {

        public Type? Data { get; set; }

        public DataResponse(Type? data)
        {
            Data = data;
        }

        public DataResponse(Type? data,string? message):base(message) 
        {
            Data = data;
        }

        public DataResponse()
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
