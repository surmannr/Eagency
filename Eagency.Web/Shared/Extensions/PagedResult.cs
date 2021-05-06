﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagency.Web.Shared.Extensions
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Results { get; set; }
        public int AllResultsCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
