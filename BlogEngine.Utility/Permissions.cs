using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System;
using System.Security.Claims;

namespace BlogEngine.Utility
{
    public static class Permissions
    {
        public static ReadOnlyCollection<Permission> AllPermissions;


        public const string UsersPermissionGroupName = "User Permissions";
        public static Permission ViewUsers = new Permission("View Users", "users.view", UsersPermissionGroupName, "Permission to view other users account details");
        public static Permission ManageUsers = new Permission("Manage Users", "users.manage", UsersPermissionGroupName, "Permission to create, delete and modify other users account details");

        public const string RolesPermissionGroupName = "Role Permissions";
        public static Permission ViewRoles = new Permission("View Roles", "roles.view", RolesPermissionGroupName, "Permission to view available roles");
        public static Permission ManageRoles = new Permission("Manage Roles", "roles.manage", RolesPermissionGroupName, "Permission to create, delete and modify roles");
        public static Permission AssignRoles = new Permission("Assign Roles", "roles.assign", RolesPermissionGroupName, "Permission to assign roles to users");


        static Permissions()
        {
            List<Permission> allPermissions = new List<Permission>()
            {
                ViewUsers,
                ManageUsers,

                ViewRoles,
                ManageRoles,
                AssignRoles
            };

            AllPermissions = allPermissions.AsReadOnly();
        }

        public static Permission GetPermissionByName(string permissionName)
        {
            return AllPermissions.Where(p => p.Name == permissionName).FirstOrDefault();
        }

        public static Permission GetPermissionByValue(string permissionValue)
        {
            return AllPermissions.Where(p => p.Value == permissionValue).FirstOrDefault();
        }

        public static string[] GetAllPermissionValues()
        {
            return AllPermissions.Select(p => p.Value).ToArray();
        }

        public static string[] GetAdministrativePermissionValues()
        {
            return new string[] { ManageUsers, ManageRoles, AssignRoles };
        }
    }



    public class Permission
    {
        public Permission()
        { }

        public Permission(string name, string value, string groupName, string description = null)
        {
            Name = name;
            Value = value;
            GroupName = groupName;
            Description = description;
        }



        public string Name { get; set; }
        public string Value { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }


        public override string ToString()
        {
            return Value;
        }


        public static implicit operator string(Permission permission)
        {
            return permission.Value;
        }
    }
}
