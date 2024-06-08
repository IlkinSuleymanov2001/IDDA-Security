using System.Net.Sockets;

namespace Goverment.AuthApi.Business.Dtos.Request.User
{

    public class UpdateNameAndSurnameRequest
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
