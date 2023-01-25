namespace ASP_1011
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public override string ToString()
        {
            return $"{Name}: {Price}";
        }
    }
}
