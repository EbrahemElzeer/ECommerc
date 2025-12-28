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
    public class ProductWithCountSpecification : BaseSpecifications<Product, int>
    {
        public ProductWithCountSpecification(ProductQueryParams queryParams) : base(ProductsSpecificationHelper.GetCriteria(queryParams))  
        {
        }
    }
}
