using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Role;
using Goverment.AuthApi.Business.Dtos.Response.Role;
using Goverment.AuthApi.Business.Dtos.Response.User;
using Goverment.AuthApi.Repositories.Abstracts;
using Goverment.AuthApi.Services.Dtos.Request.Role;
using Goverment.Core.CrossCuttingConcers.Resposne.Success;
using Microsoft.EntityFrameworkCore;

namespace Goverment.AuthApi.Services.Concretes
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepositroy;
        private readonly IUserRoleRepository _userRoleRepository;   
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepositroy, IMapper mapper, IUserRoleRepository userRoleRepository)
        {
            _roleRepositroy = roleRepositroy;
            _mapper = mapper;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<IDataResponse<CreateRoleResponse>> Create(RoleRequest createRoleRequest)
        {
            var createdRole = await _roleRepositroy.AddAsync(_mapper.Map<Role>(createRoleRequest));
            return DataResponse<CreateRoleResponse>.Ok(_mapper.Map<CreateRoleResponse>(createdRole));
        }

        public async Task<IResponse> Delete(RoleRequest roleRequest)
        {
            var role = await RoleExists(roleRequest.Name);
            await _roleRepositroy.DeleteAsync(role);
            return Response.Ok();

        }

        public async Task<IDataResponse<GetByNameRoleResponse>> GetByName(RoleRequest roleRequest)
        {
            var role = await RoleExists(roleRequest.Name);
            return DataResponse<GetByNameRoleResponse>.Ok(_mapper.Map<GetByNameRoleResponse>(role));
        }

      

        public async Task<IDataResponse<IList<ListRoleResponse>>> GetList()
        {
            var paginateData = await _roleRepositroy.GetListAsync();
            var roleList = _mapper.Map<IList<ListRoleResponse>>(paginateData.Items);
            return DataResponse<IList<ListRoleResponse>>.Ok(roleList);
        }

        public async Task<IDataResponse<UpdateRoleResponse>> Update(UpdateRoleRequest updateRoleRequest)
        {
            var role  = await RoleExists(updateRoleRequest.Name);
            role.Name = updateRoleRequest.NewName;
            await _roleRepositroy.UpdateAsync(role);
            return DataResponse<UpdateRoleResponse>.Ok(_mapper.Map<UpdateRoleResponse>(role));
        }

        public async Task<IDataResponse<PaginingGetListUserResponse>> GetUserListByRole(UserListByRoleRequest @event)
        {
           var role =  await RoleExists(@event.RoleName);

            IPaginate<UserRole> datas = await _userRoleRepository.GetListAsync(ur => ur.RoleId == role.Id, size: @event.PageRequest.PageSize,
                index: @event.PageRequest.Page, include: ef => ef.Include(ur => ur.User));

            if (datas.Items.Count == 0) throw new BusinessException("Rol-a uygun User yoxdur ");
            return DataResponse<PaginingGetListUserResponse>.Ok(_mapper.Map<PaginingGetListUserResponse>(datas));
        }

        private async Task<Role> RoleExists(string name)
        {
            var role = await _roleRepositroy.GetAsync(u => u.Name == name);
            if (role is null) throw new BusinessException("Role movcud deyil");
            return role;
        }


    }
}
