using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradexa.Application.DTOs;

namespace Tradexa.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(string id);
        Task<UserDto> UpdateAsync(string id, UserUpdateDto dto);
        Task<bool> DeleteAsync(string id);
        Task<bool> ResetPasswordAsync(string id, string newPassword);
        Task<bool> AssignRolesAsync(string id, List<string> roles);
    }

}
