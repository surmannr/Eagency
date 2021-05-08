using Eagency.BLL.Services.Interfaces;
using Eagency.Dal.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Eagency.Web.Server
{
    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
    {
        public MyUserClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var identity = await base.CreateAsync(user);
            var claimsIdentity = (ClaimsIdentity)identity.Identity;
            if(user != null)
            {
                claimsIdentity.AddClaim(new Claim("AllName", user.FirstName + " " + user.LastName));
                claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));

                var role = await UserManager.GetRolesAsync(user);
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role[0]));
            }

            return identity;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim("AllName", user.FirstName + " " + user.LastName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));

            var role = await UserManager.GetRolesAsync(user);
            identity.AddClaim(new Claim(ClaimTypes.Role, role[0]));

            return identity;
        }
    }

    public class MyClaimTransformation : IClaimsTransformation
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> userManager;

        public MyClaimTransformation(IUserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            this.userManager = userManager;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var clone = principal.Clone();
            var newIdentity = (ClaimsIdentity)clone.Identity;

            var Id = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (Id == null)
            {
                return principal;
            }

            var user = await _userService.GetByIdModelAsync(Id.Value);
            if (user == null)
            {
                return principal;
            }

            newIdentity.AddClaim(new Claim("AllName", user.FirstName + " " + user.LastName));
            newIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            newIdentity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            newIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));

            var role = await userManager.GetRolesAsync(user);
            newIdentity.AddClaim(new Claim(ClaimTypes.Role, role[0]));

            return clone;
        }
    }
}
