using ECommerce.Domin.Model.ProductModel;
using ECommerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Specifications.ProductsSpecifications
{
    public class ProductsWithTypeAndBrandSpecification : BaseSpecifications<Product, int>
    {
        public ProductsWithTypeAndBrandSpecification(ProductQueryParams queryParams) : base(ProductsSpecificationHelper.GetCriteria(queryParams))
        

        {
           
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            switch (queryParams.Sort)
            {
                case ProductsSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductsSortingOptions.NameDesc:
                    AddOrderByDescending( p => p.Name);
                    break;
                    case ProductsSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                    case ProductsSortingOptions.PriceDesc:
                    AddOrderByDescending( p => p.Price);
                    break;
                    default:
                    AddOrderBy(p => p.Id);
                    break;
            }

            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        }

        public ProductsWithTypeAndBrandSpecification(int id):base(x=>x.Id==id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
