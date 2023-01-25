namespace SharknessShop.Models
{
    public class ProductPic
    {
        public int Id { get; set; }
        public string PathName { get; set; }
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
    }
}
