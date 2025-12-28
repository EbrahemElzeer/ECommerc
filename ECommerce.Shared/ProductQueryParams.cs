using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared
{
    public class ProductQueryParams
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Search { get; set; }

        public ProductsSortingOptions Sort { get; set;}

     
        private int _PageIndex = 1;
        public int PageIndex
        {
            get => _PageIndex=1;
            set => _PageIndex = (value <0) ? 1 : value;
        }

        private const int defaultPageSize = 5;
        private const int MaxPageSize = 10;
        private int _PageSize  = 3;
        public int PageSize
        {
            get => _PageSize;
            set
            {

                if (value <= 0) _PageSize = defaultPageSize;
                else if (value > MaxPageSize) _PageSize = MaxPageSize;
                else _PageSize = value;


            }
        }
    }
}
