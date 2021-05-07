using Eagency.Dal.Entities;
using Eagency.Web.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(string id);
        Task<User> GetByIdModelAsync(string id);
        Task<UserDto> CreateCustomerAsync(UserDto create);
        Task<UserDto> CreateAdminAsync(UserDto create);
        Task DeleteAsync(string id);
        Task<UserDto> ModifyEmailAsync(string id, string email);
        Task<UserDto> ModifyUserNameAsync(string id, string username);
        Task<UserDto> ModifyNameAsync(string id, string fname, string lname);
        Task<UserDto> ModifyPasswordAsync(string id, string currentPassword, string newpassword);

    }
}
