namespace ECommerce.Domin.Model.OrderModule
{
    public class ProductItemOrderd
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
    }
}