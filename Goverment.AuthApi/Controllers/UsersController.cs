using Core.Application.Requests;
using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.AuthApi.Business.Dtos.Request.UserRole;
using Goverment.AuthApi.Conifgs;
using Microsoft.AspNetCore.Mvc;

namespace Goverment.AuthApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IUserRoleService _userRoleService;


		public UsersController(IUserService userService, IUserRoleService userRoleService)
		{
			_userService = userService;
			_userRoleService = userRoleService;
		}

		[HttpGet("get")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> getById(int id)
		{
			var data = await _userService.GetById(id);
			return Ok(data);
		}


		[HttpDelete("delete")]
        [AuthorizeRoles(Roles.ADMIN,Roles.USER)]
        public async Task<IActionResult> Delete(DeleteUserRequest deleteUserRequest)
		{
			await _userService.Delete(deleteUserRequest);
			return Ok();
		}
		[HttpPut("updateuserpassword")]
        [AuthorizeRoles(Roles.ADMIN, Roles.USER)]
        public async Task<IActionResult> UpdateUserPassword(UpdateUserPasswordRequest updateUserPasswordRequest)
		{
			await _userService.UpdateUserPassword(updateUserPasswordRequest);
			return Ok("success opeartion ,password changed...");
		}

		[HttpPut("updateuseremail")]
        [AuthorizeRoles(Roles.ADMIN, Roles.USER)]
        public async Task<IActionResult> UpdateUserEmail(UpdateUserEmailRequest updateUserEmailRequest)
		{
			var data = await _userService.UpdateUserEmail(updateUserEmailRequest);
			return Ok(data);
		}


		[HttpGet("getlist")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> GetListUser([FromQuery]PageRequest? pageRequest)
		{
			var data = await _userService.GetList(pageRequest);
			return Ok(data);
		}

		[HttpGet("getrolelist")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> GetRoleListByUserId(int userId)
		{
			var data = await _userRoleService.GetRoleListByUserId(userId);
			return Ok(data);
		}

		[HttpDelete("deleteroles")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> DeleteRolesFromUser(int userId)
		{
			await _userRoleService.DeleteRolesFromUser(userId);
			return Ok("success");
		}

		[HttpDelete("deleterole")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> DeleteRoleFromUser([FromBody]DeleteUserRoleRequest deleteUserRoleRequest)
		{
			await _userRoleService.Delete(deleteUserRoleRequest);
			return Ok("success");
		}

		[HttpPost("addroles")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> AddRolesToUser(AddRolesToUserRequest addRolesToUserRequest)
		{
			await _userRoleService.AddRolesToUser(addRolesToUserRequest);
			return Ok("success");
		}

		[HttpPost("addrole")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> AddRoleToUser([FromBody]AddUserRoleRequest addUserRoleRequest)
		{
			var data = await _userRoleService.Add(addUserRoleRequest);
			return Ok(data);
		}

        [HttpPut("updatenameandsurname")]
        [AuthorizeRoles(Roles.USER,Roles.ADMIN)]
		public async Task<IActionResult> UpadetUserNameAndSurname(UpdateNameAndSurnameRequest updateNameAndSurname) 
		{
			await _userService.UpadetUserNameAndSurname(updateNameAndSurname);
			return Ok();
		}

    }
}
