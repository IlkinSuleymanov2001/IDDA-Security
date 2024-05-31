using Core.Application.Requests;
using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Role;
using Goverment.AuthApi.Business.Dtos.Request.UserRole;
using Goverment.AuthApi.Conifgs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Goverment.AuthApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[AuthorizeRoles(Roles.ADMIN)]
	public class RolesController : ControllerBase
	{
		private readonly IRoleService _roleService;
		private readonly IUserRoleService _userRoleService;

		public RolesController(IRoleService roleService, IUserRoleService userRoleService)
		{
			_roleService = roleService;
			_userRoleService = userRoleService;
		}

		[HttpGet("get")]
		public async Task<IActionResult> GetRoleById(int id)
		{
			var data = await _roleService.GetById(id);
			return Ok(data);

		}

		[HttpPost("create")]
		public async Task<IActionResult> CreateRole(CreateRoleRequest createRoleRequest)
		{
			var data = await _roleService.Create(createRoleRequest);
			return Ok(data);
		}


		[HttpDelete("delete")]
		public async Task<IActionResult> DeleteRole(DeleteRoleRequest deleteRoleRequest)
		{
			await _roleService.Delete(deleteRoleRequest);
			return Ok("success");
		}

		[HttpPut("update")]
		public async Task<IActionResult> UpdateRole(UpdateRoleRequest updateRoleRequest)
		{
			var data = await _roleService.Update(updateRoleRequest);
			return Ok(data);
		}

		[HttpGet("getlist")]
		public async Task<IActionResult> GetListRole()
		{
			var data = await _roleService.GetList();
			return Ok(data);
		}

		[HttpGet("getuserlist")]
		public async Task<IActionResult> GetUserListByRoleId(int roleId, [FromQuery] PageRequest pageRequest)
		{
			var data = await _userRoleService.GetUserListByRoleId(new GetUserListByRoleIdRequest
			{
				RoleId = roleId,
				PageRequest = pageRequest
			});

			return Ok(data);
		}

		[HttpDelete("deleteusers")]
		public async Task<IActionResult> DeleteUsersFromRole(int roleId)
		{
			await _userRoleService.DeleteUsersFromRole(roleId);
			return Ok("success");
		}


		[HttpPost("addusers")]
		public async Task<IActionResult> AddUsersToRole(AddUsersToRoleRequest addUsersToRoleRequest)
		{
			await _userRoleService.AddUsersToRole(addUsersToRoleRequest);
			return Ok("success");
		}

	}


}

