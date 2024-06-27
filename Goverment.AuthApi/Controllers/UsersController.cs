using Core.Application.Requests;
using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Microsoft.AspNetCore.Mvc;
using Goverment.AuthApi.Controllers.Attributes;
using Goverment.AuthApi.Services.Dtos.Request.User;
using Goverment.AuthApi.Services.Dtos.Request.Role;

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
        public async Task<IActionResult> Create(CreateUserRequest createuser)
        {
            var user = await _userService.CreateUser(createuser);
            return Created("",user);
        }


        [HttpGet("getuserbyemail")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> Get([FromHeader]string email)
		{
			var data = await _userService.GetByEmail(email);
			return Ok(data);
		}

        [HttpGet("getme")]
        [AuthorizeRoles(Roles.USER)]
        public async Task<IActionResult> Get()
        {
			var data = await _userService.Get();
            return Ok(data);
        }


        [HttpDelete("delete")]
        [AuthorizeRoles(Roles.USER)]
        public async Task<IActionResult> Delete([FromBody]DeleteUserRequest deleteUserRequest)
		{
			await _userService.Delete(deleteUserRequest);
			return Ok();
		}


		[HttpPut("updatepassword")]
        [AuthorizeRoles( Roles.USER)]
        public async Task<IActionResult> UpdateUserPassword(UpdateUserPasswordRequest updateUserPasswordRequest)
		{
			await _userService.UpdateUserPassword(updateUserPasswordRequest);
			return Ok("success opeartion ,password changed...");
		}

     


        [HttpGet("getlist")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> GetListUser([FromQuery]PageRequest? pageRequest)
		{
			var data = await _userService.GetList(pageRequest);
			return Ok(data);
		}

        [HttpPut("updatenameandsurname")]
        [AuthorizeRoles(Roles.USER)]
        public async Task<IActionResult> UpadetUserNameAndSurname(UpdateNameAndSurnameRequest updateNameAndSurname)
        {
            await _userService.UpadetUserNameAndSurname(updateNameAndSurname);
            return Ok();
        }


        [HttpPost("getrolelist")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> GetRoleList([FromBody]UserEmailRequest userEmailRequest)
		{
			var data = await _userService.GetRoleList(userEmailRequest);
			return Ok(data);
		}

		[HttpDelete("deleteroles")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> DeleteRoles([FromBody]UserRoleRequest userRoleRequest)
		{
			await _userService.DeleteRole(userRoleRequest);
			return Ok();
		}

		[HttpPost("addroles")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> AddRoleRange(AddRolesToUserRequest addRolesToUserRequest)
		{
			await _userService.AddRoleRange(addRolesToUserRequest);
			return Ok();
		}

       
        [HttpPost("addrole")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> AddRole([FromBody]UserRoleRequest userRoleRequest)
		{
			 await _userService.AddRole(userRoleRequest);
			return Ok();
		}
    }
}
