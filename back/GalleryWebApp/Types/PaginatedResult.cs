using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryWebApp.Types
{
    public class PaginatedResult<T> : PaginatedResultBase
    {
        public IEnumerable<T> Items { get; set; }
        public bool IsEmpty => Items == null || !Items.Any();
        public bool IsNotEmpty => !IsEmpty;

        public PaginatedResult(IEnumerable<T> items,
            int currentPage, int resultsPerPage,
            int totalPages, long totalResults) :
            base (currentPage, resultsPerPage, totalPages, totalResults)
        {
            Items = items;
        }
    }
}
