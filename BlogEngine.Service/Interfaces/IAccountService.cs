using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using BlogEngine.Data.Entities;
using BlogEngine.Service.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace BlogEngine.Service.Interfaces
{
    public interface IAccountService
    {
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<Tuple<bool, string[]>> CreateRoleAsync(Role role, IEnumerable<string> claims);
        Task<Tuple<bool, string[]>> CreateUserAsync(User user, IEnumerable<string> roles, string password);
        Task<Tuple<bool, string[]>> DeleteRoleAsync(Role role);
        Task<Tuple<bool, string[]>> DeleteRoleAsync(string roleName);
        Task<Tuple<bool, string[]>> DeleteUserAsync(User user);
        Task<Tuple<bool, string[]>> DeleteUserAsync(string userId);
        Task<Role> GetRoleByIdAsync(string roleId);
        Task<Role> GetRoleByNameAsync(string roleName);
        Task<Role> GetRoleLoadRelatedAsync(string roleName);
        Task<List<Role>> GetRolesLoadRelatedAsync(int page, int pageSize);
        Task<Tuple<User, string[]>> GetUserAndRolesAsync(string userId);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(string userId);
        Task<User> GetUserByUserNameAsync(string userName);
        Task<IList<string>> GetUserRolesAsync(User user);
        Task<User> GetUserAsync(ClaimsPrincipal claims);
        Task<IList<UserDTO>> GetUsersAndRolesAsync(int page, int pageSize);
        Task<Tuple<bool, string[]>> ResetPasswordAsync(User user, string newPassword);
        Task<bool> TestCanDeleteRoleAsync(string roleId);
        Task<bool> TestCanDeleteUserAsync(string userId);
        Task<Tuple<bool, string[]>> UpdatePasswordAsync(User user, string currentPassword, string newPassword);
        Task<Tuple<bool, string[]>> UpdateRoleAsync(Role role, IEnumerable<string> claims);
        Task<Tuple<bool, string[]>> UpdateUserAsync(User user);
        Task<Tuple<bool, string[]>> UpdateUserAsync(User user, IEnumerable<string> roles);
        Task<SignInResult> CheckPasswordSignInAsync(User user, string password, bool locked);
        Task<bool> CanSignInAsync(User user);
        Task<AuthenticationTicket> CreateTicketAsync(OpenIdConnectRequest request, User user);
    }
}
