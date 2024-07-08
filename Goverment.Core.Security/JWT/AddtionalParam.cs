
namespace Goverment.Core.Security.JWT
{
    public  class AddtionalParam
    {
        public AddtionalParam(string? value, string key)
        {
            Value = value;
            Key = key;
        }

        public AddtionalParam()
        {
            
        }

        public required string?  Value { get; set; }
        public string Key { get; set; } = "organizationName";
    }
}
