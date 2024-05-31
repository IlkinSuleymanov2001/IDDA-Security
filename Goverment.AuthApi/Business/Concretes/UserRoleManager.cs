using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Dtos.Request.UserRole;
using Goverment.AuthApi.Business.Dtos.Response.Role;
using Goverment.AuthApi.Business.Dtos.Response.User;
using Goverment.AuthApi.Business.Dtos.Response.UserRole;
using Goverment.AuthApi.DataAccess.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Goverment.AuthApi.Business.Concretes;

public class UserRoleManager : IUserRoleService
{

	private readonly IUserRoleRepository _userRoleRepository;
	private readonly IMapper _mapper;

	public UserRoleManager(IUserRoleRepository userRoleRepository, IMapper mapper)
	{
		_userRoleRepository = userRoleRepository;
		_mapper = mapper;
	}

	public async Task<CreateUserRoleResponse> Add(AddUserRoleRequest addUserRoleRequest)
	{
		var userRole = _mapper.Map<UserRole>(addUserRoleRequest);
		var success= await _userRoleRepository.AddAsync(userRole);
		if (success == null) throw new BusinessException("User bu Role artiq elave olunub");

		return _mapper.Map<CreateUserRoleResponse>(success);
	}

	public async  Task AddRolesToUser(AddRolesToUserRequest addRolesToUserRequest)
	{
		foreach (var roleId in addRolesToUserRequest.RolesId)
			await _userRoleRepository.CustomQuery().AddAsync(new UserRole(addRolesToUserRequest.UserId, roleId));
		await _userRoleRepository.SaveChangesAsync();
	}

	public async Task AddUsersToRole(AddUsersToRoleRequest addusersToRoleRequest)
	{
		foreach (var userId in addusersToRoleRequest.UsersId)
            await _userRoleRepository.CustomQuery().AddAsync(new UserRole(userId, addusersToRoleRequest.RoleId));

		await _userRoleRepository.SaveChangesAsync();
	}

	public async Task Delete(DeleteUserRoleRequest deleteUserRoleRequest)
	{
		await _userRoleRepository.DeleteAsync(_mapper.Map<UserRole>(deleteUserRoleRequest));
	}

	public async Task DeleteUsersFromRole(int roleId)
	{
		var datas = await _userRoleRepository.GetListAsync(ur => ur.RoleId == roleId);
		if (datas.Items.Count == 0) return;
		_userRoleRepository.CustomQuery().RemoveRange(datas.Items);
		await _userRoleRepository.SaveChangesAsync();
	}

	public async  Task DeleteRolesFromUser(int userId)
	{
		var datas = await _userRoleRepository.GetListAsync(ur => ur.UserId == userId);
		if (datas.Items.Count == 0) return;
        _userRoleRepository.CustomQuery().RemoveRange(datas.Items);
		await _userRoleRepository.SaveChangesAsync();

	}

	public async Task<IList<ListRoleResponse>> GetRoleListByUserId(int userId)
	{
		IPaginate<UserRole> datas = await _userRoleRepository.GetListAsync(ur => ur.UserId == userId, include: ef => ef.Include(ur => ur.Role));
		if (datas.Items.Count == 0) throw new BusinessException("User in Rollari yoxdur");
		return _mapper.Map<IList<ListRoleResponse>>(datas.Items);

	}

	public async Task<PaginingGetListUserResponse> GetUserListByRoleId(GetUserListByRoleIdRequest getUserListByRoleIdRequest)
	{
		IPaginate<UserRole> datas = await _userRoleRepository.GetListAsync(ur => ur.RoleId == getUserListByRoleIdRequest.RoleId, size: getUserListByRoleIdRequest.PageRequest.PageSize,
			index: getUserListByRoleIdRequest.PageRequest.Page, include: ef => ef.Include(ur => ur.User));

		if (datas.Items.Count == 0) throw new BusinessException("Rol-a uygun User yoxdur ");
		return _mapper.Map<PaginingGetListUserResponse>(datas);
	}

}
