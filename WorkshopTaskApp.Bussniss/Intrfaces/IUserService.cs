using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WorkshopTaskApp.Bussniss.DTOS;
using WorkshopTaskApp.Entity.Enums;
using WorkshopTaskApp.Entity.Models;

namespace WorkshopTaskApp.Bussniss.Intrfaces
{
    public interface IUserService
    {
        Task<User> RegisterUser(User user,bool isAdmin);
        ClaimsPrincipal CreateIdentity(int id, string name,RoleEnum role);
        int? FetchUserID();
        Task<User> GetUserById(int UserId);
        Task<User> ValidateUser(UserToLoginDto loginDto);
    }
}
