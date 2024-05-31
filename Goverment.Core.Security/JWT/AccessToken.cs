
namespace Goverment.Core.Security.JWT
{
	public  class AccessToken
	{
		public AccessToken(string token, DateTime expires)
		{
			Token = token;
			Expires = expires;
		}

		public string Token { get; set; }
		public DateTime Expires { get; set; }
	}
}
