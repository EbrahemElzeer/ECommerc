using ECommerce.Domin.Model.ProductModel;
using ECommerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Specifications.ProductsSpecifications
{
    internal static class ProductsSpecificationHelper
    {
        public static Expression<Func<Product,bool>>GetCriteria(ProductQueryParams queryParams)
        {
            return P =>
        (!queryParams.BrandId.HasValue || P.ProductBrandId == queryParams.BrandId.Value)
        && (!queryParams.TypeId.HasValue || P.ProductTypeId == queryParams.TypeId.Value)
        && (string.IsNullOrEmpty(queryParams.Search) || (P.Name.ToLower().Contains(queryParams.Search.ToLower())));
        }
    }
}
