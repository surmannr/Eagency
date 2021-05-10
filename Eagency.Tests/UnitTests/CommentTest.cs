using AutoMapper;
using Eagency.BLL.Config.Mapper;
using Eagency.BLL.Services;
using Eagency.Dal;
using Eagency.Dal.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using IdentityServer4.EntityFramework.Options;
using Xunit;
using Eagency.Web.Shared.Dto;
using Eagency.Web.Shared.Extensions;
using System.Linq;
using FluentAssertions;
using Eagency.Web.Shared.Exceptions;

namespace Eagency.Tests.UnitTests
{
    public class CommentTest : IDisposable
    {
        private EagencyDbContext _context;
        private CommentService _service;
        private IMapper mapper;

        public CommentTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapProfile());
            });
            mapper = mockMapper.CreateMapper();

            var options = new DbContextOptionsBuilder<EagencyDbContext>()
                .UseInMemoryDatabase("TestDatabaseComment")
                .Options;

            var someOptions = Options.Create(new OperationalStoreOptions());

            _context = new EagencyDbContext(options, someOptions);

            _context.Comments.AddRange(SeedData());
            _context.SaveChanges();

            _service = new CommentService(_context, mapper);
        }

        [Fact]
        public async Task GetCommentByApartmentIdTest()
        {
            int propertyid = 1;
            var pagenumber = 1;
            var pagesize = 2;

            var expect = _context.Comments.Where(c => c.PropertyId == propertyid).Include(c => c.User).ToList();

            PagedResult<CommentDto> entity = await _service.GetAllByPropertyIdPagedAsnyc(propertyid, pagesize, pagenumber);

            entity.PageNumber.Should().Be(pagenumber);
            entity.PageSize.Should().Be(pagesize);
            entity.AllResultsCount.Should().Be(expect.Count);

            var comment = entity.Results.FirstOrDefault(c => c.PropertyId == propertyid);
            var expectedcomment = expect.FirstOrDefault(c => c.PropertyId == propertyid);
            comment.Should().NotBeNull();

            comment.Question.Should().Be(expectedcomment.Question);
            comment.Answer.Should().BeNull();
            comment.Date.ToString("yyyy/MM/DD").Should().Be(expectedcomment.Date.ToString("yyyy/MM/DD"));
            comment.PropertyId.Should().Be(expectedcomment.PropertyId);
            comment.Id.Should().Be(expectedcomment.Id);
            comment.UserId.Should().Be(expectedcomment.UserId);
        }

        [Fact]
        public void GetCommentByApartmentIdTest_failedpaging()
        {
            int propertyid = 1;
            var pagenumber = -1;
            var pagesize = -1;

           Func<Task> action = async () => await _service.GetAllByPropertyIdPagedAsnyc(propertyid, pagesize, pagenumber);
           action.Should().Throw<InvalidQueryParamsException>();
        }

        [Fact]
        public void CreateComment_validationfail()
        {
            CommentDto commentDto = new CommentDto();

            Func<Task> action = async () => await _service.CreateAsync(commentDto);
            action.Should().Throw<InvalidQueryParamsException>();
        }

        [Fact]
        public async Task CreateComment()
        {
            CommentDto commentDto = new CommentDto() { 
                Id = 9,
                Answer = null,
                Date = new DateTimeOffset(2021, 4, 5, 12, 30, 30, new TimeSpan(1, 0, 0)),
                Question = "Test question",
                PropertyId = 1,
                UserId = "teszt"
            };

            var result = await _service.CreateAsync(commentDto);
            result.Should().NotBeNull();

            result.Id.Should().Be(9);
            result.Answer.Should().BeNull();
            result.Date.Should().Be(new DateTimeOffset(2021, 4, 5, 12, 30, 30, new TimeSpan(1, 0, 0)));
            result.Question.Should().Be("Test question");
            result.PropertyId.Should().Be(1);
            result.UserId.Should().Be("teszt");
        }

        [Fact]
        public async Task DeleteComment()
        {
            int propertyid = 1;
            var pagenumber = 1;
            var pagesize = 2;

            var expect = _context.Comments.Where(c => c.PropertyId == propertyid).Include(c => c.User).ToList();
            int beforecount = expect.Count;
            PagedResult<CommentDto> entity = await _service.GetAllByPropertyIdPagedAsnyc(propertyid, pagesize, pagenumber);

            entity.PageNumber.Should().Be(pagenumber);
            entity.PageSize.Should().Be(pagesize);
            entity.AllResultsCount.Should().Be(beforecount);
            entity.Results.Count().Should().Be(beforecount);

            await _service.DeleteAsync(propertyid);
            expect = _context.Comments.Where(c => c.PropertyId == propertyid).Include(c => c.User).ToList();

            int aftercount = expect.Count;
            entity = await _service.GetAllByPropertyIdPagedAsnyc(propertyid, pagesize, pagenumber);

            entity.PageNumber.Should().Be(pagenumber);
            entity.PageSize.Should().Be(pagesize);
            entity.AllResultsCount.Should().Be(aftercount);
            entity.Results.Count().Should().Be(aftercount);
        }

        [Fact]
        public void DeleteComment_notfound()
        {
            Func<Task> action = async () => await _service.DeleteAsync(9);
            action.Should().Throw<DbNullException>();
        }

        [Fact]
        public void AddAnswer_nullstring()
        {
            Func<Task> action = async () => await _service.AddAnswerAsync(1, null);
            action.Should().Throw<InvalidQueryParamsException>();
        }

        [Fact]
        public void AddAnswer_notfound()
        {
            Func<Task> action = async () => await _service.AddAnswerAsync(10, "teszt");
            action.Should().Throw<DbNullException>();
        }

        [Fact]
        public async Task AddAnswer()
        {
            var expect = _context.Comments.FirstOrDefault(c => c.Id == 1);

            expect.Answer.Should().BeNull();

            var actual = await _service.AddAnswerAsync(1, "testtext");

            actual.Answer.Should().NotBeNull();
        }

        private List<Comment> SeedData()
        {
            return new List<Comment>()
            {
                new Comment()
                {
                    Id = 1,
                    Question = "Are there any shops nearby?",
                    Answer = null,
                    Date = new DateTimeOffset(2021,7,5,12,30,30,new TimeSpan(1, 0, 0)),
                    UserId = "seedone",
                    PropertyId = 1,
                },
                new Comment()
                {
                    Id = 2,
                    Question = "Is the home in a flood zone or prone to other natural disasters?",
                    Answer = null,
                    Date = new DateTimeOffset(2021,7,5,12,30,30,new TimeSpan(1, 0, 0)).AddDays(23),
                    UserId = "seedtwo",
                    PropertyId = 2,
                },
                new Comment()
                {
                    Id = 3,
                    Question = "Why is the seller leaving?",
                    Answer = "He has a new job in another town.",
                    Date = new DateTimeOffset(2021,7,5,12,30,30,new TimeSpan(1, 0, 0)).AddDays(23),
                    UserId = "seedthree",
                    PropertyId = 3,
                },
                new Comment()
                {
                    Id = 4,
                    Question = "How old is the roof?",
                    Answer = "It's one year old.",
                    Date = new DateTimeOffset(2021,7,5,12,30,30,new TimeSpan(1, 0, 0)).AddDays(23),
                    UserId = "seedone",
                    PropertyId = 4,
                },
                new Comment()
                {
                    Id = 5,
                    Question = "Is the home in a flood zone or prone to other natural disasters?",
                    Date = new DateTimeOffset(2021,7,5,12,30,30,new TimeSpan(1, 0, 0)).AddDays(23),
                    Answer = null,
                    UserId = "seedtwo",
                    PropertyId = 5,
                },
                new Comment()
                {
                    Id = 6,
                    Question = "How is the neighborhood?",
                    Date = new DateTimeOffset(2021,7,5,12,30,30,new TimeSpan(1, 0, 0)).AddDays(23),
                    Answer = null,
                    UserId = "seedthree",
                    PropertyId = 6,
                },
                new Comment()
                {
                    Id = 7,
                    Question = "Have there been previous problems with the house, or repairs which have been necessary?",
                    Answer = null,
                    Date = new DateTimeOffset(2021,7,5,12,30,30,new TimeSpan(1, 0, 0)).AddDays(23),
                    UserId = "seedone",
                    PropertyId = 7,
                },
                new Comment()
                {
                    Id = 8,
                    Question = "What’s included with the sale?",
                    Answer = null,
                    UserId = "seedtwo",
                    Date = new DateTimeOffset(2021,7,5,12,30,30,new TimeSpan(1, 0, 0)).AddDays(23),
                    PropertyId = 8,
                },
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
