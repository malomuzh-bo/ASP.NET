namespace ASP_1011
{
    public class Category
    {
        public string Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Product> Products { get; set; }
        public Category(string name, ICollection<Product> products)
        {
            CategoryName = name;
            Products = products;
        }
        public Category()
        {
            Products = new List<Product>();
        }
        public string getProd()
        {
            string str = "";
            if (Products != null)
            {
                foreach (var item in Products)
                {
                    str += $"{item.Name}\n";
                }
                return str;
            }
            return str;
        }
        public override string ToString()
        {
            string str = "";
            if (Products != null)
            {
                foreach (var item in Products)
                {
                    str += $"{item.Name}\n";
                }
            }
            else
            {
                str = "Error__";
            }
            return $"{CategoryName}:\n\nProducts:\n{str}";
        }
    }
}
