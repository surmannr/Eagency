using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.Web.Shared.Extensions
{
    public static class PagingExtension
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> list, int pagesize, int page)
        {
            return list.Skip(pagesize * (page - 1)).Take(pagesize);
        }

        public static IEnumerable<T> PagingEnumerable<T>(this IEnumerable<T> list, int pagesize, int page)
        {
            return list.Skip(pagesize * (page - 1)).Take(pagesize);
        }

    }
}
