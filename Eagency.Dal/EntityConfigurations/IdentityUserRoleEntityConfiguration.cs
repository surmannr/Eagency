using Eagency.Web.Shared.Contants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.Dal.EntityConfigurations
{
    public class IdentityUserRoleEntityConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>() { UserId = "admin", RoleId = Roles.Agent },
                 new IdentityUserRole<string>() { UserId = "seedone", RoleId = Roles.Customer },
                 new IdentityUserRole<string>() { UserId = "seedtwo", RoleId = Roles.Customer },
                 new IdentityUserRole<string>() { UserId = "seedthree", RoleId = Roles.Customer }
            );
        }
    }
}
