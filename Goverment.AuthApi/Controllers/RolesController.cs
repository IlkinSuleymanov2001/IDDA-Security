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
    public class RolesController(IRoleService roleService) : ControllerBase
    {
        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery]RoleRequest createRoleRequest)=>
             Ok(await roleService.GetByName(createRoleRequest));


        [HttpPost("create")]
        public async Task<IActionResult> CreateRole(RoleRequest createRoleRequest) =>
             Ok(await roleService.Create(createRoleRequest));


        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteRole(RoleRequest deleteRoleRequest)=>
             Ok(await roleService.Delete(deleteRoleRequest));



        [HttpPut("update")]
        public async Task<IActionResult> UpdateRole(UpdateRoleRequest updateRoleRequest)=>
             Ok(await roleService.Update(updateRoleRequest));


        [HttpGet("getlist")]
        public async Task<IActionResult> GetListRole()=>
             Ok(await roleService.GetList());


        [HttpGet("userlistbyrole")]
        public async Task<IActionResult> GetUserListByRole([FromQuery]UserListByRoleRequest userListByRole)=>
             Ok(await roleService.GetUserListByRole(userListByRole));

    }
}

