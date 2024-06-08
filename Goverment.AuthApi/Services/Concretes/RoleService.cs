using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Role;
using Goverment.AuthApi.Business.Dtos.Response.Role;
using Goverment.AuthApi.Repositories.Abstracts;

namespace Goverment.AuthApi.Business.Concretes
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepositroy;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepositroy, IMapper mapper)
        {
            _roleRepositroy = roleRepositroy;
            _mapper = mapper;

        }

        public async Task<CreateRoleResponse> Create(CreateRoleRequest createRoleRequest)
        {
             var createdRole = await _roleRepositroy.AddAsync(_mapper.Map<Role>(createRoleRequest));
            return _mapper.Map<CreateRoleResponse>(createdRole);
            
        }

        public async Task Delete(DeleteRoleRequest deleteRoleRequest)
        {
            var role = await RoleExists(deleteRoleRequest.Name);
            await _roleRepositroy.DeleteAsync(role);

        }

        public async Task<GetByNameRoleResponse> GetByName(string name )
        {
            var role = await RoleExists(name);
            return _mapper.Map<GetByNameRoleResponse>(role);
        }

        private async Task<Role> RoleExists(string name)
        {
            var role = await _roleRepositroy.GetAsync(u =>u.Name==name.ToUpper());
            if (role is  null) throw new BusinessException("Role movcud deyil");
            return role;
        }

        public async Task<IList<ListRoleResponse>> GetList()
        {
            var datas = await _roleRepositroy.GetListAsync();
            return _mapper.Map<IList<ListRoleResponse>>(datas.Items);
        }

        public async Task<UpdateRoleResponse> Update(UpdateRoleRequest updateRoleRequest)
        {
            var role  = await RoleExists(updateRoleRequest.Name);
            role.Name = updateRoleRequest.NewName.ToLower();
            await _roleRepositroy.UpdateAsync(role);
            return _mapper.Map<UpdateRoleResponse>(role);
        }
    }
}
