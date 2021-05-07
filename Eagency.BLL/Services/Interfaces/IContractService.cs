using Eagency.Web.Shared.Dto;
using Eagency.Web.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.BLL.Services.Interfaces
{
    public interface IContractService
    {
        Task<IEnumerable<ContractDto>> GetAllAsync();
        Task<PagedResult<ContractDto>> GetAllByUserIdPagedAsync(string userid, int pagesize, int pagenumber);
        Task<ContractDto> CreateAsync(ContractDto create);
        Task DeleteAsync(int id);
        Task<ContractDto> ModifyPaidAsync(int id, bool ispaid);
        Task<ContractDto> ModifySignedAsync(int id);

    }
}
