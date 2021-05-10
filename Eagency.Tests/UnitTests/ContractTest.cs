using AutoMapper;
using Eagency.BLL.Config.Mapper;
using Eagency.BLL.Services;
using Eagency.Dal;
using Eagency.Dal.Entities;
using Eagency.Web.Shared.Dto;
using Eagency.Web.Shared.Enums;
using Eagency.Web.Shared.Exceptions;
using Eagency.Web.Shared.Extensions;
using FluentAssertions;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Eagency.Tests.UnitTests
{
    public class ContractTest : IDisposable
    {
        private EagencyDbContext _context;
        private ContractService _service;
        private IMapper mapper;

        public ContractTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapProfile());
            });
            mapper = mockMapper.CreateMapper();

            var options = new DbContextOptionsBuilder<EagencyDbContext>()
                .UseInMemoryDatabase("TestDatabaseContract")
                .Options;

            var someOptions = Options.Create(new OperationalStoreOptions());

            _context = new EagencyDbContext(options, someOptions);

            User user = new User()
            {
                Id = "seedone"
            };

            _context.Users.Add(user);
            _context.Contracts.AddRange(SeedData());
            _context.Properties.AddRange(SeedProperty());
            _context.SaveChanges();

            _service = new ContractService(_context, mapper);
        }

        [Fact]
        public async Task GetAllContract()
        {
            var contractlist = await _service.GetAllAsync();
            var expectedlist = _context.Contracts.ToList();

            contractlist.Count().Should().Be(expectedlist.Count);
        }

        [Fact]
        public async Task GetAllByUserId_paged()
        {
            string userid = "seedone";
            int pagesize = 3;
            int pagenumber = 1;

            var contracts = _context.Contracts.Where(c => c.ClientId == userid).ToList();
            PagedResult<ContractDto> actualcontracts = await _service.GetAllByUserIdPagedAsync(userid, pagesize, pagenumber);

            actualcontracts.PageSize.Should().Be(pagesize);
            actualcontracts.PageNumber.Should().Be(pagenumber);
            actualcontracts.AllResultsCount.Should().Be(contracts.Count);
            actualcontracts.Results.Count().Should().Be(contracts.Count);

            var contract = actualcontracts.Results.FirstOrDefault();
            contracts.Should().NotBeNull();

            contract.IsPaid.Should().Be(false);
            contract.IsSigned.Should().Be(false);
            contract.Id.Should().Be(1);
            contract.ClientId.Should().Be("seedone");
            contract.Date.Should().Be(new DateTimeOffset(2021, 2, 5, 12, 30, 30, new TimeSpan(1, 0, 0)).AddDays(2));
            contract.FeePercentage.Should().Be(0.28);
            contract.PaymentFrequency.Should().Be(2);
            contract.PaymentMethod.Should().Be(PaymentMethod.BankTransfer);
            contract.PropertyId.Should().Be(3);
        }

        [Fact]
        public void GetAllByUserId_paged_failedpaging()
        {
            string userid = "seedone";
            var pagenumber = -1;
            var pagesize = -1;

            Func<Task> action = async () => await _service.GetAllByUserIdPagedAsync(userid, pagesize, pagenumber);
            action.Should().Throw<InvalidQueryParamsException>();
        }

        [Fact]
        public void GetAllByUserId_paged_usernotfound()
        {
            string userid = "nincsilyen";
            var pagenumber = 1;
            var pagesize = 2;

            Func<Task> action = async () => await _service.GetAllByUserIdPagedAsync(userid, pagesize, pagenumber);
            action.Should().Throw<DbNullException>();
        }

        [Fact]
        public void Create_failedvalidation()
        {
            ContractDto contractdto = new ContractDto();

            Func<Task> action = async () => await _service.CreateAsync(contractdto);
            action.Should().Throw<InvalidQueryParamsException>();
        }

        [Fact]
        public void Create_notfoundproperty()
        {
            ContractDto contractdto = new ContractDto()
            {
                ClientId = "seedone",
                Date = new DateTimeOffset(2021, 2, 5, 12, 30, 30, new TimeSpan(1, 0, 0)).AddDays(-2),
                FeePercentage = 0.28,
                IsPaid = false,
                IsSigned = false,
                PaymentFrequency = 2,
                PaymentMethod = PaymentMethod.BankTransfer,
                PropertyId = 3
            };

            Func<Task> action = async () => await _service.CreateAsync(contractdto);
            action.Should().Throw<InvalidQueryParamsException>();
        }

        [Fact]
        public void Create_soldpropertyfailed()
        {
            var property = _context.Properties.Where(c => c.Id == 3).FirstOrDefault();
            property.Sold = true;
            ContractDto contractdto = new ContractDto()
            {
                ClientId = "seedone",
                Date = new DateTimeOffset(2021, 2, 5, 12, 30, 30, new TimeSpan(1, 0, 0)).AddDays(-2),
                FeePercentage = 0.28,
                IsPaid = false,
                IsSigned = false,
                PaymentFrequency = 2,
                PaymentMethod = PaymentMethod.BankTransfer,
                PropertyId = 3
            };

            Func<Task> action = async () => await _service.CreateAsync(contractdto);
            action.Should().Throw<InvalidQueryParamsException>().WithMessage("Hibás paraméterek.");
        }

        [Fact]
        public async Task CreateCommentAsync()
        {
            ContractDto contractdto = new ContractDto()
            {
                ClientId = "seedone",
                Date = new DateTimeOffset(2021, 2, 5, 12, 30, 30, new TimeSpan(1, 0, 0)).AddDays(-2),
                FeePercentage = 0.28,
                IsPaid = false,
                IsSigned = false,
                PaymentFrequency = 2,
                PaymentMethod = PaymentMethod.BankTransfer,
                PropertyId = 10
            };

            var result = await _service.CreateAsync(contractdto);
        }

        [Fact]
        public void Delete_notfound()
        {
            Func<Task> action = async () => await _service.DeleteAsync(10);
            action.Should().Throw<DbNullException>();
        }

        [Fact]
        public async Task Delete()
        {
            var actuallist = _context.Contracts.ToList();
            var list = await _service.GetAllAsync();
            list.Count().Should().Be(actuallist.Count);

            await _service.DeleteAsync(1);

            actuallist = _context.Contracts.ToList();
            list = await _service.GetAllAsync();
            list.Count().Should().Be(actuallist.Count);
        }

        [Fact]
        public void ModifyPaid_notfound()
        {
            Func<Task> action = async () => await _service.ModifyPaidAsync(10,true);
            action.Should().Throw<DbNullException>();
        }

        [Fact]
        public async Task ModifyPaid()
        {
            var result = await _service.ModifyPaidAsync(2, true);
            result.Id.Should().Be(2);
            result.IsPaid.Should().Be(true);
        }

        [Fact]
        public void ModifySign_notfound()
        {
            Func<Task> action = async () => await _service.ModifySignedAsync(10);
            action.Should().Throw<DbNullException>();
        }

        [Fact]
        public async Task ModifySign()
        {
            var result = await _service.ModifySignedAsync(2);
            result.Id.Should().Be(2);
            result.IsSigned.Should().Be(true);
        }

        private List<Contract> SeedData()
        {
            return new List<Contract>()
            {
                new Contract()
                {
                    Id = 1,
                    ClientId = "seedone",
                    Date = new DateTimeOffset(2021,2,5,12,30,30,new TimeSpan(1, 0, 0)).AddDays(2),
                    FeePercentage = 0.28,
                    IsPaid = false,
                    IsSigned = false,
                    PaymentFrequency = 2,
                    PaymentMethod = PaymentMethod.BankTransfer,
                    PropertyId = 3
                },
                new Contract()
                {
                    Id = 2,
                    ClientId = "seedtwo",
                    Date = new DateTimeOffset(2021,2,5,12,30,30,new TimeSpan(1, 0, 0)).AddDays(6),
                    FeePercentage = 0.58,
                    IsPaid = true,
                    IsSigned = true,
                    PaymentFrequency = 3,
                    PaymentMethod = PaymentMethod.Cash,
                    PropertyId = 5
                },
                new Contract()
                {
                    Id = 3,
                    ClientId = "seedthree",
                    Date = new DateTimeOffset(2021,2,5,12,30,30,new TimeSpan(1, 0, 0)).AddDays(27),
                    FeePercentage = 0.39,
                    IsPaid = true,
                    IsSigned = false,
                    PaymentFrequency = 5,
                    PaymentMethod = PaymentMethod.Creditcard,
                    PropertyId = 1
                }
            };
        }

        public List<Property> SeedProperty()
        {
            return new List<Property>()
            {
                new Property()
                {
                    Id = 1,
                    NumberOfParkingSpaces = 2,
                    NumberOfBathrooms = 1,
                    NumberOfBedrooms = 3,
                    NumberOfGarages = 1,
                    Area = 110,
                    AgentId = "admin",
                    HouseType = HouseType.Detached,
                    Description = "Situated on the first floor we offer a two bedroom apartment offering partial Marina Views. The apartment is within walking distance to Swansea Marina and all the local restaurants and coffee shops, close to the new music arena and town centre. The apartment offers a L shaped hallway leading to lounge with french doors and balcony, master bedroom, bathroom, kitchen and second bedroom and benifits from lift access, recently fitted UPVC windows. and allocated secure underground parking.",
                    ImageName = "alap1.jpg",
                    IsFurnished = true,
                    Price = 22000,
                    Sold = true
                },
                new Property()
                {
                    Id = 10,
                    NumberOfParkingSpaces = 2,
                    NumberOfBathrooms = 1,
                    NumberOfBedrooms = 3,
                    NumberOfGarages = 1,
                    Area = 110,
                    AgentId = "admin",
                    HouseType = HouseType.Detached,
                    Description = "Situated on the first floor we offer a two bedroom apartment offering partial Marina Views. The apartment is within walking distance to Swansea Marina and all the local restaurants and coffee shops, close to the new music arena and town centre. The apartment offers a L shaped hallway leading to lounge with french doors and balcony, master bedroom, bathroom, kitchen and second bedroom and benifits from lift access, recently fitted UPVC windows. and allocated secure underground parking.",
                    ImageName = "alap1.jpg",
                    IsFurnished = true,
                    Price = 22000,
                    Sold = false
                },
                new Property()
                {
                    Id = 3,
                    NumberOfParkingSpaces = 2,
                    NumberOfBathrooms = 1,
                    NumberOfBedrooms = 2,
                    NumberOfGarages = 2,
                    Area = 35,
                    AgentId = "admin",
                    HouseType = HouseType.Mediterranean,
                    Description = "This very popular and pretty, coastal village of Horton is nestled between the glorious Oxwich Bay and Port Eynon Bay along the South coast of the Gower Peninsula. A popular beach side spot, Horton is in an area of outstanding natural beauty which is blessed with many award-winning sandy beaches and amazing coastal walks.",
                    ImageName = "alap3.jpg",
                    IsFurnished = true,
                    Price = 17400,
                    Sold = true
                },
                 new Property()
                {
                    Id = 5,
                    NumberOfParkingSpaces = 0,
                    NumberOfBathrooms = 1,
                    NumberOfBedrooms = 3,
                    NumberOfGarages = 2,
                    Area = 64,
                    AgentId = "admin",
                    HouseType = HouseType.Colonial,
                    Description = "emi detached property comprising entrance hallway, lounge and fitted kitchen to the ground floor. To the first floor there are two bedrooms and a bathroom. Externally there is generously sized garden wrapping around the side and rear. Benefits include uPVC double glazing throughout and newly fitted Hive gas central heating with newly fitted radiators. The property is situated close to local amenities and transport links with easy access to the M4. Viewing is highly recommended to appreciate this well presented property. ",
                    ImageName = "alap5.jpg",
                    IsFurnished = false,
                    Price = 35000,
                    Sold = true
                }
            };
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _service = null;
            mapper = null;
        }
    }
}
