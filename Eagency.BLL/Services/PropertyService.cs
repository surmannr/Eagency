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
    public class PropertyService : IPropertyService
    {
        private readonly EagencyDbContext db;
        private readonly IMapper mapper;

        public PropertyService(EagencyDbContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }

        public async Task<PropertyDto> GetByIdAsync(int id)
        {
            var property = await db.Properties.Where(p => p.Id == id).FirstOrDefaultAsync();
            if(property == null)
            {
                throw new DbNullException();
            }
            return mapper.Map<PropertyDto>(property);
        }

        public async Task<IEnumerable<PropertyDto>> GetAllAsync()
        {
            var properties = await db.Properties.ToListAsync();
            return mapper.Map<IEnumerable<PropertyDto>>(properties);
        }

        public async Task<PagedResult<PropertyDto>> GetAllPagedAsync(int pagesize, int pagenumber)
        {
            if (pagesize < 0 || pagenumber < 0)
            {
                throw new InvalidQueryParamsException();
            }

            PagedResult<PropertyDto> result = new PagedResult<PropertyDto>();

            var properties = await db.Properties.OrderBy(c => c.Id).Paging(pagesize, pagenumber).ToListAsync();
            var count = await db.Properties.CountAsync();

            result.Results = mapper.Map<IEnumerable<PropertyDto>>(properties);
            result.PageSize = pagesize;
            result.PageNumber = pagenumber;
            result.AllResultsCount = count;

            return result;
        }

        public async Task<PropertyDto> CreateAsync(PropertyDto create)
        {
            if (CheckEntity(create))
            {
                var result = db.Properties.Add(mapper.Map<Property>(create));
                await db.SaveChangesAsync();
                return mapper.Map<PropertyDto>(result.Entity);
            } 
            else
            {
                throw new InvalidQueryParamsException();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var property = await db.Properties.Where(p => p.Id == id).Include(c => c.Comments).Include(c => c.Contract).FirstOrDefaultAsync();
            if (property == null)
            {
                throw new DbNullException();
            }

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var comment in property.Comments)
                    {
                        db.Comments.Remove(comment);
                    }
                    
                    if(property.Contract != null)
                    {
                        await transaction.RollbackAsync();
                    }

                    db.Properties.Remove(property);
                    await db.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                }

            }

        }

        public async Task<PropertyDto> ModifyAsync(int id, PropertyDto modify)
        {
            if (CheckEntity(modify))
            {
                var entity = await db.Properties.Where(p => p.Id == id).FirstOrDefaultAsync();
                if(entity == null)
                {
                    throw new DbNullException();
                }

                entity.Address.City = modify.City;
                entity.Address.Street = modify.Street;
                entity.Address.Country = modify.Country;
                entity.Address.ZipCode = modify.ZipCode;
                entity.HouseType = modify.HouseType;
                entity.AgentId = modify.AgentId;
                entity.Area = modify.Area;
                entity.Description = modify.Description;
                entity.ImageName = modify.ImageName;
                entity.IsFurnished = modify.IsFurnished;
                entity.NumberOfBathrooms = modify.NumberOfBathrooms;
                entity.NumberOfBedrooms = modify.NumberOfBedrooms;
                entity.NumberOfGarages = modify.NumberOfGarages;
                entity.NumberOfParkingSpaces = modify.NumberOfParkingSpaces;
                entity.Price = modify.Price;

                await db.SaveChangesAsync();
                return mapper.Map<PropertyDto>(entity);
            }
            else
            {
                throw new InvalidQueryParamsException();
            }
        }

        private bool CheckEntity(PropertyDto dto)
        {
            if(dto != null)
            {
                if (string.IsNullOrEmpty(dto.AgentId)) return false;
                if (dto.Area < 0) return false;
                if (string.IsNullOrEmpty(dto.City)) return false;
                if (string.IsNullOrEmpty(dto.Street)) return false;
                if (string.IsNullOrEmpty(dto.Country)) return false;
                if (dto.ZipCode < 0) return false;
                if (dto.Price < 0) return false;
                if (dto.NumberOfBathrooms < 0) return false;
                if (dto.NumberOfBedrooms < 0) return false;
                if (dto.NumberOfGarages < 0) return false;
                if (dto.NumberOfParkingSpaces < 0) return false;

                return true;
            } 
            else
            {
                return false;
            }
        }
    }
}
