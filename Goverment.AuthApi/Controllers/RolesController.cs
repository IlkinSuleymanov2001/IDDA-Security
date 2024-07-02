using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Role;
using Microsoft.AspNetCore.Mvc;
using Goverment.AuthApi.Services.Dtos.Request.Role;
using Goverment.AuthApi.Commans.Attributes;

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
        public async Task<IActionResult> Get([FromQuery]RoleRequest createRoleRequest)=>
             Ok(await _roleService.GetByName(createRoleRequest));


        [HttpPost("create")]
        public async Task<IActionResult> CreateRole(RoleRequest createRoleRequest) =>
             Ok(await _roleService.Create(createRoleRequest));


        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteRole(RoleRequest deleteRoleRequest)=>
             Ok(await _roleService.Delete(deleteRoleRequest));



        [HttpPut("update")]
        public async Task<IActionResult> UpdateRole(UpdateRoleRequest updateRoleRequest)=>
             Ok(await _roleService.Update(updateRoleRequest));


        [HttpGet("getlist")]
        public async Task<IActionResult> GetListRole()=>
             Ok(await _roleService.GetList());


        [HttpGet("userlistbyrole")]
        public async Task<IActionResult> GetUserListByRole([FromQuery]UserListByRoleRequest userListByRole)=>
             Ok(await _roleService.GetUserListByRole(userListByRole));

    }
}

