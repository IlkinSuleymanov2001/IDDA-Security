using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Role;
using Goverment.AuthApi.Business.Dtos.Response.Role;
using Goverment.AuthApi.DataAccess.Repositories.Abstracts;

namespace Goverment.AuthApi.Business.Concretes
{
    public class RoleManager : IRoleService
    {
        private readonly IRoleRepository _roleRepositroy;
        private readonly IMapper _mapper;

        public RoleManager(IRoleRepository roleRepositroy, IMapper mapper)
        {
            _roleRepositroy = roleRepositroy;
            _mapper = mapper;

        }

        public async Task<CreateRoleResponse> Create(CreateRoleRequest createRoleRequest)
        {
            await RoleIsUnique(createRoleRequest.Name);
            var createdRole = await _roleRepositroy.AddAsync(_mapper.Map<Role>(createRoleRequest));
            return _mapper.Map<CreateRoleResponse>(createdRole);
        }

        public async Task RoleIsUnique(string roleName)
        {
            var role = await _roleRepositroy.GetAsync(c => c.Name.ToUpper() == roleName.ToUpper());
            if (role != null) throw new BusinessException("Role artiq var");

        }

        public async Task Delete(DeleteRoleRequest deleteRoleRequest)
        {
            var role = await RoleExists(deleteRoleRequest.Id);
            await _roleRepositroy.DeleteAsync(role);

        }

        public async Task<GetByIdRoleResponse> GetById(int roleId)
        {
            var role = await RoleExists(roleId);
            return _mapper.Map<GetByIdRoleResponse>(role);
        }

        private async Task<Role> RoleExists(int id)
        {
            var role = await _roleRepositroy.GetAsync(u => u.Id == id);
            if (role == null) throw new BusinessException("Role movcud deyil");
            return role;
        }

        public async Task<IList<ListRoleResponse>> GetList()
        {
            var datas = await _roleRepositroy.GetListAsync();
            return _mapper.Map<IList<ListRoleResponse>>(datas.Items);
        }

        public async Task<UpdateRoleResponse> Update(UpdateRoleRequest updateRoleRequest)
        {
            //todo bax buna niye iwdemir
            //var role  = await RoleExists(updateRoleRequest.Id);
            var role = await _roleRepositroy.UpdateAsync(_mapper.Map<Role>(updateRoleRequest));
            return _mapper.Map<UpdateRoleResponse>(role);
        }
    }
}
