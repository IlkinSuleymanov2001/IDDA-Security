using Core.Application.Requests;
using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Auth;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.AuthApi.Business.Dtos.Request.UserRole;
using Goverment.AuthApi.Conifgs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

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

        [HttpPost("create")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> Create(CreateUserRequest createuser)
        {
            var user = await _userService.CreateUser(createuser);
            return Created("",user);
        }


        [HttpGet("getbyid")]
        [AuthorizeRoles(Roles.ADMIN)]
        public async Task<IActionResult> GetById(int id)
		{
			var data = await _userService.GetById(id);
			return Ok(data);
		}

        [HttpGet("get")]
        [AuthorizeRoles(Roles.USER)]
        public async Task<IActionResult> Get()
        {
			var data = await _userService.Get();
            return Ok(data);
        }


        [HttpDelete("delete")]
        [AuthorizeRoles(Roles.USER)]
        public async Task<IActionResult> Delete()
		{
			await _userService.Delete();
			return Ok();
		}
		[HttpPut("updateuserpassword")]
        [AuthorizeRoles( Roles.USER)]
        public async Task<IActionResult> UpdateUserPassword(UpdateUserPasswordRequest updateUserPasswordRequest)
		{
			await _userService.UpdateUserPassword(updateUserPasswordRequest);
			return Ok("success opeartion ,password changed...");
		}

		[HttpPut("updateuseremail")]
        [AuthorizeRoles( Roles.USER)]
        public async Task<IActionResult> UpdateUserEmail(UpdateUserEmailRequest updateUserEmailRequest)
		{
            
            await _userService.UpdateUserEmail(updateUserEmailRequest);
			return Ok();
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
        [AuthorizeRoles(Roles.USER)]
		public async Task<IActionResult> UpadetUserNameAndSurname(UpdateNameAndSurnameRequest updateNameAndSurname) 
		{
			await _userService.UpadetUserNameAndSurname(updateNameAndSurname);
			return Ok();
		}

       

    }
}
