using AutoMapper;
using Eagency.BLL.Services.Interfaces;
using Eagency.Dal;
using Eagency.Dal.Entities;
using Eagency.Web.Shared.Contants;
using Eagency.Web.Shared.Dto;
using Eagency.Web.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly EagencyDbContext db;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public UserService(EagencyDbContext _db, IMapper _mapper, UserManager<User> _userManager)
        {
            db = _db;
            mapper = _mapper;
            userManager = _userManager;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await db.Users.ToListAsync();
            var userdto = mapper.Map<List<UserDto>>(users);

            foreach(var user in userdto)
            {
                var usertemp = users.Where(c => c.Id == user.Id).FirstOrDefault();
                if(usertemp!= null)
                {
                    var roles = await userManager.GetRolesAsync(usertemp);
                    user.Role = roles[0];
                }

            }

            return userdto;
        }

        public async Task<UserDto> GetByIdAsync(string id)
        {
            var user = await db.Users.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new DbNullException();
            }
            return mapper.Map<UserDto>(user);
        }

        public async Task<User> GetByIdModelAsync(string id)
        {
            var user = await db.Users.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new DbNullException();
            }
            return user;
        }

        public async Task<UserDto> CreateCustomerAsync(UserDto create)
        {
            if (CheckEntity(create))
            {
                var user = mapper.Map<User>(create);
                user.Id = Guid.NewGuid().ToString();
                var result = await userManager.CreateAsync(user, create.Password);

                if (!result.Succeeded)
                {
                    throw new DbNullException();
                }

                var currentUser = await userManager.FindByNameAsync(user.UserName);
                await userManager.AddToRoleAsync(currentUser, Roles.Customer);
                await db.SaveChangesAsync();
                return mapper.Map<UserDto>(currentUser);
            }
            else
            {
                throw new InvalidQueryParamsException();
            }
        }

        public async Task<UserDto> CreateAdminAsync(UserDto create)
        {
            if (CheckEntity(create))
            {
                var user = mapper.Map<User>(create);
                user.EmailConfirmed = true;
                user.Id = Guid.NewGuid().ToString();
                var result = await userManager.CreateAsync(user, create.Password);

                if (!result.Succeeded)
                {
                    throw new DbNullException();
                }

                var currentUser = await userManager.FindByNameAsync(user.UserName);
                await userManager.AddToRoleAsync(currentUser, Roles.Agent);
                await db.SaveChangesAsync();
                return mapper.Map<UserDto>(currentUser);
            }
            else
            {
                throw new InvalidQueryParamsException();
            }
        }

        public async Task DeleteAsync(string id)
        {
            var user = await db.Users.Where(p => p.Id == id).Include(c => c.Comments).Include(c => c.Properties).Include(c => c.Contracts).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new DbNullException();
            }
            using (var transaction = db.Database.BeginTransaction())
            {
                try 
                {
                    foreach (var comment in user.Comments)
                    {
                        db.Comments.Remove(comment);
                    }
                    foreach (var property in user.Properties)
                    {
                        db.Properties.Remove(property);
                    }
                    foreach (var contract in user.Contracts)
                    {
                        if (!contract.IsPaid)
                        {
                            await transaction.RollbackAsync();
                            return;
                        }
                        db.Contracts.Remove(contract);
                    }

                    db.Users.Remove(user);

                    await db.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch(Exception)
                {
                    await transaction.RollbackAsync();
                }
                
            }
        }

        public async Task<UserDto> ModifyEmailAsync(string id, string email)
        {
            var user = await db.Users.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new DbNullException();
            }

            user.Email = string.IsNullOrEmpty(email) ? user.Email : email;

            await db.SaveChangesAsync();
            return mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> ModifyUserNameAsync(string id, string username)
        {
            var user = await db.Users.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new DbNullException();
            }

            user.UserName = string.IsNullOrEmpty(username) ? user.UserName : username;

            await db.SaveChangesAsync();
            return mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> ModifyNameAsync(string id, string fname, string lname)
        {
            var user = await db.Users.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new DbNullException();
            }

            user.FirstName = string.IsNullOrEmpty(fname) ? user.FirstName : fname;
            user.LastName = string.IsNullOrEmpty(lname) ? user.LastName : lname;

            await db.SaveChangesAsync();
            return mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> ModifyPasswordAsync(string id, string currentPassword, string newpassword)
        {
            var user = await db.Users.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new DbNullException();
            }

            if (string.IsNullOrEmpty(currentPassword))
            {
                throw new InvalidQueryParamsException();
            }
            if (string.IsNullOrEmpty(newpassword))
            {
                throw new InvalidQueryParamsException();
            }

            await userManager.ChangePasswordAsync(user, currentPassword, newpassword);

            await db.SaveChangesAsync();
            return mapper.Map<UserDto>(user);
        }

        public bool CheckEntity(UserDto user)
        {
            if (string.IsNullOrEmpty(user.Email)) return false;
            if (string.IsNullOrEmpty(user.FirstName)) return false;
            if (string.IsNullOrEmpty(user.LastName)) return false;
            if (string.IsNullOrEmpty(user.UserName)) return false;
            if (string.IsNullOrEmpty(user.Password)) return false;
            return true;
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await db.Users.Where(p => p.Email == email).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new DbNullException();
            }
            return mapper.Map<UserDto>(user);
        }
    }
}
