

namespace Goverment.Core.Security.JWT
{
	public  class RefreshToken
	{
		public string Token { get; set; }
		public DateTime Expires { get; set; }

	}
}
