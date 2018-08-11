using System;
using System.Linq;
using AutoMapper;
using BlogEngine.Data.Entities;
using BlogEngine.Utility;
using BlogEngine.WebApi.Helper;
using BlogEngine.WebApi.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace BlogEngine.WebApi.AppConfig
{
    public class Mapper : Profile
    {
        private readonly IMapper _mapper;
        public Mapper(IMapper mapper)
        {
            _mapper = mapper;
        }
        public Mapper()
        {
            CreateMap<UserEditViewModel, User>()
                .ForMember(s=>s.Roles, d => d.Ignore());
            CreateMap<User, UserEditViewModel>()
                .ForMember(s => s.Roles, d => d.Ignore());

            CreateMap<RoleViewModel, Role>();
            CreateMap<Role, RoleViewModel>()
                .ForMember(d => d.Permissions, map => map.MapFrom(s => s.Claims))
                .ForMember(d => d.UsersCount, map => map.ResolveUsing(s => s.Users?.Count ?? 0))
                .ReverseMap();

            CreateMap<IdentityRoleClaim<string>, ClaimViewModel>()
                .ForMember(d => d.Type, map => map.MapFrom(s => s.ClaimType))
                .ForMember(d => d.Value, map => map.MapFrom(s => s.ClaimValue))
                .ReverseMap();

            CreateMap<Permission, PermissionViewModel>();
            CreateMap<PermissionViewModel, Permission>();

            CreateMap<IdentityRoleClaim<string>, PermissionViewModel>()
                .ConvertUsing(s => _mapper.Map<PermissionViewModel>(Permissions.GetPermissionByValue(s.ClaimValue)));

            CreateMap<PermissionViewModel, IdentityRoleClaim<string>>()
                .ForMember(x => x.ClaimType, s => s.MapFrom(x => x.GroupName))
                .ForMember(x => x.ClaimValue, s => s.MapFrom(x => x.Value))
                .ForMember(x => x.RoleId, s => s.MapFrom(x => x.RoleId));
        } 
    }
}
