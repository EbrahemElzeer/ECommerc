using ECommerce.Domin.Model.IdentityModule;
using ECommerce.Service.Abstraction;
using ECommerce.Shared.CommonRespones;
using ECommerce.Shared.Dtos.IdentityDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationService(UserManager<ApplicationUser> userManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<Result<bool>> EmailExists(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }

        public async Task<Result<UserDto>> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
                        if (user == null) return Error.NotFound("User not found.");
            var token= await CreateTokenAsync(user);
            return new UserDto(user.Email!, user.DisplayName!, token);
        }

        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Error.InvalidCredintals("User not found.");
            var IsPassordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!IsPassordValid) return Error.InvalidCredintals("User not found.");
            var token= await CreateTokenAsync(user);
            return new UserDto(user.Email!, user.DisplayName!, token);
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.Phone,
                UserName = registerDto.UserName

            };
            var IdentityResult = await _userManager.CreateAsync(user, registerDto.Password);
            if (!IdentityResult.Succeeded)
            {

                return IdentityResult.Errors.Select(s => Error.Validation(s.Code, s.Description)).ToList();
            }
                var token=await CreateTokenAsync(user);
                return new UserDto(user.Email!, user.DisplayName!, token);
        }

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
           

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email!),
                new Claim(JwtRegisteredClaimNames.Name,user.UserName!)
               
            };
            var roles= await _userManager.GetRolesAsync(user);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var secretKey = _configuration["JwtOption:SecretKey"];
            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
           (
                issuer: _configuration["JwtOption:Issuer"],
                audience: _configuration["JwtOption:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: cred
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
