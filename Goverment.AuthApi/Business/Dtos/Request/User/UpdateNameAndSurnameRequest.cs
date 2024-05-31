using System.Net.Sockets;

namespace Goverment.AuthApi.Business.Dtos.Request.User
{
    //todo Validation 
    public class UpdateNameAndSurnameRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
