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
    public class ContractService : IContractService
    {
        private readonly EagencyDbContext db;
        private readonly IMapper mapper;

        public ContractService(EagencyDbContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }

        public async Task<IEnumerable<ContractDto>> GetAllAsync()
        {
            var contracts = await db.Contracts.ToListAsync();
            return mapper.Map<IEnumerable<ContractDto>>(contracts);
        }

        public async Task<PagedResult<ContractDto>> GetAllByUserIdPagedAsync(string userid,int pagesize, int pagenumber)
        {
            if (pagesize < 0 || pagenumber < 0 || string.IsNullOrEmpty(userid))
            {
                throw new InvalidQueryParamsException();
            }

            var user = await db.Users.Where(c => c.Id == userid).FirstOrDefaultAsync();
            if(user == null)
            {
                throw new DbNullException();
            }

            PagedResult<ContractDto> result = new PagedResult<ContractDto>();

            var properties = await db.Contracts.Where(c => c.ClientId == userid).OrderBy(c => c.Date).Paging(pagesize, pagenumber).ToListAsync();
            var count = await db.Contracts.Where(c => c.ClientId == userid).CountAsync();

            result.Results = mapper.Map<IEnumerable<ContractDto>>(properties);
            result.PageSize = pagesize;
            result.PageNumber = pagenumber;
            result.AllResultsCount = count;

            return result;
        }

        public async Task<ContractDto> CreateAsync(ContractDto create)
        {
            if (CheckEntity(create))
            {
                var result = db.Contracts.Add(mapper.Map<Contract>(create));
                await db.SaveChangesAsync();
                return mapper.Map<ContractDto>(result.Entity);
            }
            else
            {
                throw new InvalidQueryParamsException();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var contract = await db.Contracts.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (contract == null)
            {
                throw new DbNullException();
            }
            db.Contracts.Remove(contract);
            await db.SaveChangesAsync();
        }

        public async Task<ContractDto> ModifyPaidAsync(int id, bool ispaid)
        {
            var entity = await db.Contracts.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (entity == null)
            {
                throw new DbNullException();
            }

            entity.IsPaid = ispaid;

            await db.SaveChangesAsync();
            return mapper.Map<ContractDto>(entity);
        }

        private bool CheckEntity(ContractDto dto)
        {
            if (dto != null)
            {
                if (dto.Date > DateTimeOffset.Now.AddMinutes(5)) return false;
                if (dto.FeePercentage < 0 || dto.FeePercentage > 1) return false;
                if (dto.PaymentFrequency < 0) return false;
                if (dto.PropertyId < 0) return false;
                if (string.IsNullOrEmpty(dto.ClientId)) return false;

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<ContractDto> ModifySignedAsync(int id)
        {
            var entity = await db.Contracts.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (entity == null)
            {
                throw new DbNullException();
            }

            entity.IsSigned = true;

            await db.SaveChangesAsync();
            return mapper.Map<ContractDto>(entity);
        }
    }
}
