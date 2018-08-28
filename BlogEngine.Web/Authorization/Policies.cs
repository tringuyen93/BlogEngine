using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Utility.Authorization
{
    public class Policies
    {
        public const string ViewAllUsersPolicy = "View All Users";
        public const string ManageAllUsersPolicy = "Manage All Users";
        public const string ViewAllRolesPolicy = "View All Roles";
        public const string ViewRoleByRoleNamePolicy = "View Role by RoleName";
        public const string ManageAllRolesPolicy = "Manage All Roles";
        public const string AssignAllowedRolesPolicy = "Assign Allowed Roles";
    }
    public static class AccountManagementOperations
    {
        public const string CreateOperationName = "Create";
        public const string ReadOperationName = "Read";
        public const string UpdateOperationName = "Update";
        public const string DeleteOperationName = "Delete";

        public static UserAccountAuthorizationRequirement Create = new UserAccountAuthorizationRequirement(CreateOperationName);
        public static UserAccountAuthorizationRequirement Read = new UserAccountAuthorizationRequirement(ReadOperationName);
        public static UserAccountAuthorizationRequirement Update = new UserAccountAuthorizationRequirement(UpdateOperationName);
        public static UserAccountAuthorizationRequirement Delete = new UserAccountAuthorizationRequirement(DeleteOperationName);
    }
}
