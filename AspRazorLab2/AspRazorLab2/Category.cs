namespace AspRazorLab2
{
    public class Category
    {
        public string Id { get; set; }
        public string CategoryName { get; set; }
        public List<Product>? products { get; set; }
        public string getProd()
        {
            string str = "";
            if(products != null)
            {
                foreach (var item in products)
                {
                    str += $"*{item.Name}\n";
                }
                return str;
            }
            return "";
        }
        public override string ToString()
        {
            string str = "";
            if(products != null)
            {
                foreach (var item in products)
                {
                    str += $"*{item.Name}\n";
                }
            }
            else
            {
                str = "Empty";
            }
            return $"{CategoryName},\nProducts:\n{str}";
        }
    }
}
