using AutoMapper;
using Eagency.BLL.Services.Interfaces;
using Eagency.Dal;
using Eagency.Dal.Entities;
using Eagency.Web.Shared.Dto;
using Eagency.Web.Shared.Exceptions;
using Eagency.Web.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly EagencyDbContext db;
        private readonly IMapper mapper;

        public CommentService(EagencyDbContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }

        public async Task<PagedResult<CommentDto>> GetAllByPropertyIdPagedAsnyc(int propertyid, int pagesize, int pagenumber)
        {
            if (pagesize < 0 || pagenumber < 0 || propertyid < 0)
            {
                throw new InvalidQueryParamsException();
            }

            var comments = await db.Comments.Where(c => c.PropertyId == propertyid).Include(e => e.User).OrderBy(c => c.Date)
                                            .Paging(pagesize, pagenumber).ToListAsync();
            var count = await db.Comments.Where(c => c.PropertyId == propertyid).CountAsync();

            PagedResult<CommentDto> result = new PagedResult<CommentDto>();

            result.AllResultsCount = count;
            result.PageNumber = pagenumber;
            result.PageSize = pagesize;
            result.Results = mapper.Map<IEnumerable<CommentDto>>(comments);

            return result;
        }

        public async Task<CommentDto> CreateAsync(CommentDto create)
        {
            if (CheckEntity(create))
            {
                var map = mapper.Map<Comment>(create);
                var result = db.Comments.Add(map);
                await db.SaveChangesAsync();
                return mapper.Map<CommentDto>(result.Entity);
            }
            else
            {
                throw new InvalidQueryParamsException();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var comment = await db.Comments.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (comment == null)
            {
                throw new DbNullException();
            }
            db.Comments.Remove(comment);
            await db.SaveChangesAsync();
        }

        public async Task<CommentDto> AddAnswerAsync(int id, string answer)
        {
            if (!string.IsNullOrEmpty(answer))
            {
                var entity = await db.Comments.Where(p => p.Id == id).FirstOrDefaultAsync();
                if (entity == null)
                {
                    throw new DbNullException();
                }

                entity.Answer = answer;

                await db.SaveChangesAsync();
                return mapper.Map<CommentDto>(entity);
            }
            else
            {
                throw new InvalidQueryParamsException();
            }
        }

        private bool CheckEntity(CommentDto dto)
        {
            if (dto != null)
            {
                if (dto.Date > DateTimeOffset.Now.AddMinutes(5)) return false;
                if (string.IsNullOrEmpty(dto.Question)) return false;
                if (dto.PropertyId <= 0) return false;
                if (string.IsNullOrEmpty(dto.UserId)) return false;

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
