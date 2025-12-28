using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Dtos.BasketDtos
{
    public record BasketItemDto(
        int Id,
        string ProductName,
        decimal Price,
        string PictureUrl,
       
        int Quantity
        );

}
