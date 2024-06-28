using Newtonsoft.Json.Serialization;

namespace Goverment.Core.CrossCuttingConcers.Resposne
{
    public class LowercaseNamingStrategy : NamingStrategy
    {
        protected override string ResolvePropertyName(string name)
        {
            return name.ToLower();
        }
    }
}
