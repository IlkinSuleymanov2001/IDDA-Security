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
    public class UsersController(IUserService userService) : ControllerBase
    {
        [HttpPost("create")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest createUser, [FromQuery] string? organization ,[FromQuery] params string?[]? role) =>
             Created("", await userService.Create(createUser,organization, role));


        [HttpGet("getuserbyemail")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> Get([FromHeader] string email) =>
             Ok(await userService.GetByEmail(email));

        [HttpGet("getme")]
        [AuthorizeRoles(Roles.USER)]
        public async Task<IActionResult> GetMe() =>
             Ok(await userService.Get());


        [HttpGet("getmeweb")]
        [AuthorizeRoles(Roles.STAFF,Roles.ADMIN)]
        public async Task<IActionResult> GetMeForWEb() =>
            Ok(await userService.GetForWeb());


        [HttpDelete("delete")]
        [AuthorizeRoles(Roles.USER)]
        public async Task<IActionResult> Delete([FromBody] DeleteUserRequest deleteUserRequest) =>
             Ok(await userService.Delete(deleteUserRequest));


        [HttpPut("updatepassword")]
        [AuthorizeRoles(Roles.USER)]
        public async Task<IActionResult> UpdateUserPassword(UpdateUserPasswordRequest updateUserPasswordRequest) =>
           Ok(await userService.UpdatePassword(updateUserPasswordRequest));


        [HttpGet("getlist")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> GetListUser([FromQuery] PageRequest pageRequest) =>
             Ok(await userService.GetList(pageRequest));


        [HttpPut("updatefullname")]
        [AuthorizeRoles(Roles.USER)]
        public async Task<IActionResult> UpdateFullName(UpdateUserFullNameRequest updateNameAndSurname) =>
             Ok(await userService.UpdateFullName(updateNameAndSurname));


        [HttpPost("getrolelist")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> GetRoleList([FromBody] UserEmailRequest userEmailRequest) =>
             Ok(await userService.GetRoleList(userEmailRequest));


        [HttpDelete("deleterole")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> DeleteRole([FromBody] UserRoleRequest userRoleRequest) =>
             Ok(await userService.DeleteRole(userRoleRequest));


        [HttpPost("addroles")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> AddRoleRange(AddRolesToUserRequest addRolesToUserRequest) =>
             Ok(await userService.AddRoleRange(addRolesToUserRequest));


        [HttpPost("addrole")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> AddRole([FromBody] UserRoleRequest userRoleRequest) =>
             Ok(await userService.AddRole(userRoleRequest));
    }
}
