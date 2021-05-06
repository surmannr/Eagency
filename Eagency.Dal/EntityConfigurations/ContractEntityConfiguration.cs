using Eagency.Dal.Entities;
using Eagency.Web.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.Dal.EntityConfigurations
{
    public class ContractEntityConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.HasData(
                new Contract()
                {
                    Id = 1,
                    ClientId = "seedone",
                    Date = DateTimeOffset.Now.AddDays(13),
                    FeePercentage = 0.28,
                    IsPaid = false,
                    PaymentFrequency = 2,
                    PaymentMethod = PaymentMethod.BankTransfer,
                    PropertyId = 3
                },
                new Contract()
                {
                    Id = 2,
                    ClientId = "seedtwo",
                    Date = DateTimeOffset.Now.AddDays(5),
                    FeePercentage = 0.58,
                    IsPaid = true,
                    PaymentFrequency = 3,
                    PaymentMethod = PaymentMethod.Cash,
                    PropertyId = 5
                },
                new Contract()
                {
                    Id = 3,
                    ClientId = "seedthree",
                    Date = DateTimeOffset.Now.AddDays(27),
                    FeePercentage = 0.39,
                    IsPaid = true,
                    PaymentFrequency = 5,
                    PaymentMethod = PaymentMethod.Creditcard,
                    PropertyId = 1
                }
            );
        }
    }
}
