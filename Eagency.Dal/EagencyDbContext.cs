using Eagency.Dal.Entities;
using Eagency.Dal.EntityConfigurations;
using Eagency.Web.Shared.Enums;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.Dal
{
    public class EagencyDbContext : ApiAuthorizationDbContext<User>
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Property> Properties { get; set; }

        public EagencyDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var converter = new EnumToStringConverter<HouseType>();

            builder.Entity<Property>(entity => {

                entity.OwnsOne(e => e.Address);

                entity.Property(e => e.HouseType)
                      .HasConversion(converter);

                entity.HasMany(e => e.Comments).WithOne(e => e.Property)
                      .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(e => e.Contract).WithOne(e => e.Property)
                      .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(e => e.User).WithMany(e => e.Properties)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            var converter2 = new EnumToStringConverter<PaymentMethod>();

            builder.Entity<Contract>(entity => {

                entity.Property(e => e.PaymentMethod)
                      .HasConversion(converter2);

                entity.HasOne(e => e.Property).WithOne(e => e.Contract)
                      .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(e => e.User).WithMany(e => e.Contracts)
                      .OnDelete(DeleteBehavior.ClientSetNull);

            });

            builder.Entity<User>(entity => {
                entity.HasMany(e => e.Contracts).WithOne(e => e.User)
                      .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasMany(e => e.Properties).WithOne(e => e.User)
                      .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasMany(e => e.Comments).WithOne(e => e.User)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            builder.Entity<Comment>(entity => {
                entity.HasOne(e => e.Property).WithMany(e => e.Comments)
                      .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(e => e.User).WithMany(e => e.Comments)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            builder.ApplyConfiguration(new RoleEntityConfiguration());
            builder.ApplyConfiguration(new UserEntityConfiguration());
            builder.ApplyConfiguration(new IdentityUserRoleEntityConfiguration());
            builder.ApplyConfiguration(new PropertyEntityConfiguration());
            builder.ApplyConfiguration(new CommentEntityConfiguration());
            builder.ApplyConfiguration(new ContractEntityConfiguration());

        }
    }
}
