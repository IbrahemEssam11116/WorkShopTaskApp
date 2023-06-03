using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WorkshopTaskApp.Bussniss.DTOS;
using WorkshopTaskApp.Bussniss.Intrfaces;
using WorkshopTaskApp.Entity.Enums;
using WorkshopTaskApp.Entity.Models;
using WorkshopTaskApp.Repository.Interfaces;

namespace WorkshopTaskApp.Bussniss.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IGenericRepository<User> genericRepository,
                              IUnitOfWork unitOfWork,
                              IHttpContextAccessor httpContextAccessor)
        {
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }



        public async Task<User> RegisterUser(User user, bool isAdmin)
        {
            if (isAdmin)
                user.Role = RoleEnum.Admin;
            else
                user.Role = RoleEnum.User;
            var savedUser = await _genericRepository.Add(user);
            var isSaved = await _unitOfWork.Save();
            if (isSaved)
                return savedUser;
            else
                return null;
        }


        public ClaimsPrincipal CreateIdentity(int id, string name, RoleEnum role)
        {
            try
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(id)),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role.ToString()),

            };
                //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
                return new ClaimsPrincipal(identity);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int? FetchUserID()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims.ToList();
            if (claims.Count != 0)
                return int.Parse(claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            else
                return null;
            //return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }

        public async Task<User> GetUserById(int UserId)
        {
            return await _genericRepository.GetById(UserId);
        }

        public async Task<User> ValidateUser(UserToLoginDto loginDto)
        {
            var users = await _genericRepository.FindByCondition(w => w.UserName == loginDto.UserName && w.Password == loginDto.Password);
            return users.FirstOrDefault();
        }
    }
}
