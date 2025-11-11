using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domin.Model.ProductModel
{
    public class Product:BaseEntity<int>
    {
     
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }


        #region RelationShips
        public int ProductBrandId { get; init; }
        public ProductBrand ProductBrand { get; set; }=null!;

        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; } = null!;
        #endregion


    }
}
