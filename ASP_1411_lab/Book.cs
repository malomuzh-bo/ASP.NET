using Newtonsoft.Json;

namespace ASP_1411_lab
{
    public class Book
    {
        public string Id { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string Style { get; set; }
        public string Publisher { get; set; }
        public int Year { get; set; }
    }
}
