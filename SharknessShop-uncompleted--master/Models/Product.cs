namespace SharknessShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductPic> ProductPics { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; }
        public int ProductCategoryId { get; set; }
        public virtual ProductCategory? ProductCategory { get; set; }
        public string Info { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }

        public Product()
        {
            ProductOrders = new List<ProductOrder>();
            ProductPics = new List<ProductPic>();
        }
    }
}
