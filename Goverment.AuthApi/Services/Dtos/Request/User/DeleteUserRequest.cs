using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Goverment.AuthApi.Services.Dtos.Request.User
{
    public class DeleteUserRequest
    {
        public string Password { get; set; }
    }
}
