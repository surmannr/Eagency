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
    public class RoleEntityConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole()
                {
                    Name = Roles.Agent,
                    NormalizedName = Roles.Agent.ToUpper(),
                    Id = Roles.Agent,
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole()
                {
                    Name = Roles.Customer,
                    NormalizedName = Roles.Customer.ToUpper(),
                    Id = Roles.Customer,
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            );
        }
    }
}
