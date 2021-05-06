using Eagency.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.Dal.EntityConfigurations
{
    public class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasData(
                new Comment()
                {
                    Id = 1,
                    Question = "Are there any shops nearby?",
                    Answer = null,
                    Date = DateTimeOffset.Now.AddDays(23),
                    UserId = "seedone",
                    PropertyId = 1,
                },
                new Comment()
                {
                    Id = 2,
                    Question = "Is the home in a flood zone or prone to other natural disasters?",
                    Answer = null,
                    Date = DateTimeOffset.Now.AddDays(23),
                    UserId = "seedtwo",
                    PropertyId = 2,
                },
                new Comment()
                {
                    Id = 3,
                    Question = "Why is the seller leaving?",
                    Answer = "He has a new job in another town.",
                    Date = DateTimeOffset.Now.AddDays(23),
                    UserId = "seedthree",
                    PropertyId = 3,
                },
                new Comment()
                {
                    Id = 4,
                    Question = "How old is the roof?",
                    Answer = "It's one year old.",
                    Date = DateTimeOffset.Now.AddDays(23),
                    UserId = "seedone",
                    PropertyId = 4,
                },
                new Comment()
                {
                    Id = 5,
                    Question = "Is the home in a flood zone or prone to other natural disasters?",
                    Date = DateTimeOffset.Now.AddDays(23),
                    Answer = null,
                    UserId = "seedtwo",
                    PropertyId = 5,
                },
                new Comment()
                {
                    Id = 6,
                    Question = "How is the neighborhood?",
                    Date = DateTimeOffset.Now.AddDays(23),
                    Answer = null,
                    UserId = "seedthree",
                    PropertyId = 6,
                },
                new Comment()
                {
                    Id = 7,
                    Question = "Have there been previous problems with the house, or repairs which have been necessary?",
                    Answer = null,
                    Date = DateTimeOffset.Now.AddDays(23),
                    UserId = "seedone",
                    PropertyId = 7,
                },
                new Comment()
                {
                    Id = 8,
                    Question = "What’s included with the sale?",
                    Answer = null,
                    UserId = "seedtwo",
                    Date = DateTimeOffset.Now.AddDays(23),
                    PropertyId = 8,
                },
                new Comment()
                {
                    Id = 9,
                    Question = "Why is the seller leaving?",
                    Date = DateTimeOffset.Now.AddDays(23),
                    Answer = "He has a new job in another town.",
                    UserId = "seedthree",
                    PropertyId = 1,
                },
                new Comment()
                {
                    Id = 10,
                    Question = "How old is the roof?",
                    Answer = "It's one year old.",
                    Date = DateTimeOffset.Now.AddDays(23),
                    UserId = "seedone",
                    PropertyId = 2,
                },
                new Comment()
                {
                    Id = 11,
                    Question = "Is the home in a flood zone or prone to other natural disasters?",
                    Answer = null,
                    Date = DateTimeOffset.Now.AddDays(23),
                    UserId = "seedtwo",
                    PropertyId = 3,
                },
                new Comment()
                {
                    Id = 12,
                    Question = "How is the neighborhood?",
                    Answer = null,
                    Date = DateTimeOffset.Now.AddDays(23),
                    UserId = "seedthree",
                    PropertyId = 6,
                }
            );
        }
    }
}
