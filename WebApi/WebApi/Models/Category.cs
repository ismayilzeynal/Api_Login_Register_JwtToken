namespace WebApi.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDelete { get; set; }
        public List<Product> Products { get; set; }
    }
}
