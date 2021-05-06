using Eagency.Web.Shared.Dto;
using Eagency.Web.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.BLL.Services.Interfaces
{
    public interface ICommentService
    {
        Task<PagedResult<CommentDto>> GetAllByPropertyIdPagedAsnyc(int propertyid, int pagesize, int pagenumber);
        Task<CommentDto> CreateAsync(CommentDto create);
        Task DeleteAsync(int id);
        Task<CommentDto> AddAnswerAsync(int id, string answer);
    }
}
