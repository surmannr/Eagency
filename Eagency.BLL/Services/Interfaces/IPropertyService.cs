using Eagency.Web.Shared.Dto;
using Eagency.Web.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.BLL.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<PropertyDto> GetByIdAsync(int id);
        Task<IEnumerable<PropertyDto>> GetAllAsync();
        Task<PagedResult<PropertyDto>> GetAllAvailablePagedAsync(int pagesize, int pagenumber);
        Task<PagedResult<PropertyDto>> GetAllPagedAsync(int pagesize, int pagenumber);
        Task<PropertyDto> CreateAsync(PropertyDto create);
        Task DeleteAsync(int id);
        Task<PropertyDto> ModifyAsync(int id, PropertyDto modify);

    }
}
