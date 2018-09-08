using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParadiseExplorer.Models
{
    public class PagedResult<T>
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public List<T> Items { get; set; }
    }
}
