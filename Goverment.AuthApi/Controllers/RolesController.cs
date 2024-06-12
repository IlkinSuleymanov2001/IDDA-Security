using Core.Application.Requests;
using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Role;
using Microsoft.AspNetCore.Mvc;
using Goverment.AuthApi.Controllers.Attributes;
using Goverment.AuthApi.Services.Dtos.Request.Role;

namespace Goverment.AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeRoles(Roles.ADMIN)]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery]RoleRequest createRoleRequest)
        {
            var data = await _roleService.GetByName(createRoleRequest);
            return Ok(data);

        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRole(RoleRequest createRoleRequest)
        {
            var data = await _roleService.Create(createRoleRequest);
            return Ok(data);
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteRole(RoleRequest deleteRoleRequest)
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

        [HttpGet("userlistbyrole")]
        public async Task<IActionResult> GetUserListByRole([FromQuery]UserListByRoleRequest userListByRole)
        {
           var data =  await _roleService.GetUserListByRole(userListByRole);
            return Ok(data);
        }

 


    }
}

