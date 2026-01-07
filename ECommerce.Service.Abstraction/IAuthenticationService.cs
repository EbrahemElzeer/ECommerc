using ECommerce.Shared.CommonRespones;
using ECommerce.Shared.Dtos.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Abstraction
{
    public interface IAuthenticationService
    {
        Task<Result<UserDto>>LoginAsync(LoginDto loginDto);
        Task<Result<UserDto>>RegisterAsync(RegisterDto registerDto);
        Task<Result<bool>>EmailExists(string email);
        Task<Result<UserDto>>GetUserByEmailAsync(string email);
    }
}
