using Core.Application.Requests;
using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Role;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.AuthApi.Commans.Attributes;
using Goverment.AuthApi.Services.Dtos.Request.Role;
using Goverment.AuthApi.Services.Dtos.Request.User;
using Microsoft.AspNetCore.Mvc;

namespace Goverment.AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest createuser, [FromQuery] params string?[] role) =>
             Created("", await _userService.Create(createuser, role));


        [HttpGet("getuserbyemail")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> Get([FromHeader] string email) =>
             Ok(await _userService.GetByEmail(email));

        [HttpGet("getme")]
        [AuthorizeRoles(Roles.USER)]
        public async Task<IActionResult> Get() =>
             Ok(await _userService.Get());



        [HttpDelete("delete")]
        [AuthorizeRoles(Roles.USER)]
        public async Task<IActionResult> Delete([FromBody] DeleteUserRequest deleteUserRequest) =>
             Ok(await _userService.Delete(deleteUserRequest));


        [HttpPut("updatepassword")]
        [AuthorizeRoles(Roles.USER)]
        public async Task<IActionResult> UpdateUserPassword(UpdateUserPasswordRequest updateUserPasswordRequest) =>
           Ok(await _userService.UpdatePassword(updateUserPasswordRequest));




        [HttpGet("getlist")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> GetListUser([FromQuery] PageRequest? pageRequest) =>
             Ok(await _userService.GetList(pageRequest));


        [HttpPut("updatefullname")]
        [AuthorizeRoles(Roles.USER)]
        public async Task<IActionResult> UpdateFullName(UpdateUserFullNameRequest updateNameAndSurname) =>
             Ok(await _userService.UpdateFullName(updateNameAndSurname));


        [HttpPost("getrolelist")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> GetRoleList([FromBody] UserEmailRequest userEmailRequest) =>
             Ok(await _userService.GetRoleList(userEmailRequest));


        [HttpDelete("deleterole")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> DeleteRole([FromBody] UserRoleRequest userRoleRequest) =>
             Ok(await _userService.DeleteRole(userRoleRequest));


        [HttpPost("addroles")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> AddRoleRange(AddRolesToUserRequest addRolesToUserRequest) =>
             Ok(await _userService.AddRoleRange(addRolesToUserRequest));


        [HttpPost("addrole")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> AddRole([FromBody] UserRoleRequest userRoleRequest) =>
             Ok(await _userService.AddRole(userRoleRequest));
    }
}
