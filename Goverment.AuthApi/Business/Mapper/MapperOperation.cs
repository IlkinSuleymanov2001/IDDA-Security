using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Role;
using Goverment.AuthApi.Business.Dtos.Request.UserRole;
using Goverment.AuthApi.Business.Dtos.Response.Role;
using Goverment.AuthApi.Business.Dtos.Response.User;
using Goverment.AuthApi.Business.Dtos.Response.UserRole;

namespace Goverment.AuthApi.Business.Mapper;

public class MapperOperation:Profile
{
        public MapperOperation()
        {

		#region User
		CreateMap<User,CreateUserResponse>().ReverseMap();
            CreateMap<User,GetUserResponse>().ReverseMap();
            CreateMap<PaginingGetListUserResponse,IPaginate<User>>().ReverseMap();
            CreateMap<ListUserResponse, User>().ReverseMap();
            CreateMap<UpdateUserResponse,User>().ReverseMap();
		#endregion
		#region Role
		CreateMap<Role , GetByIdRoleResponse>().ReverseMap();
		CreateMap<Role, UpdateRoleResponse>().ReverseMap();
		CreateMap<Role, ListRoleResponse>().ReverseMap();
		CreateMap<Role, CreateRoleResponse>().ReverseMap();
		CreateMap<Role, CreateRoleRequest>().ReverseMap();
		CreateMap<Role, UpdateRoleRequest>().ReverseMap();
		#endregion
		#region	UserRole
		CreateMap<UserRole, AddUserRoleRequest>().ReverseMap();
		CreateMap<UserRole, CreateUserRoleResponse>().ReverseMap();
		CreateMap<UserRole, DeleteUserRoleRequest>().ReverseMap();
		CreateMap<UserRole, ListRoleResponse>().
			ForMember(c => c.Id, opt => opt.MapFrom(c => c.Role.Id)).
			ForMember(c => c.Name, opt => opt.MapFrom(c => c.Role.Name)).
			ReverseMap();


		CreateMap<IPaginate<UserRole>, PaginingGetListUserResponse>().ReverseMap();

		CreateMap<UserRole, ListUserResponse>().
		ForMember(c => c.Id,
		opt => opt.MapFrom(c => c.User.Id)).
		ForMember(c=>c.Email,opt=>opt.MapFrom(c=>c.User.Email)).
        ForMember(c => c.FirstName, opt => opt.MapFrom(c => c.User.FirstName)).
        ForMember(c => c.LastName, opt => opt.MapFrom(c => c.User.LastName))
        .ReverseMap();
		#endregion




	}
}
