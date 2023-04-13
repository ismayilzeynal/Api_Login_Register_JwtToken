namespace WebApi.Dtos.ProductDtos
{
    public class ProductReturnDto
    {
        public string Name { get; set; }
        public double SalePrice { get; set; }
        public double CostPrice { get; set; }
        public double Profit { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public CategoryInProductReturnDto Category { get; set; }
    }

    public class CategoryInProductReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set;}
    }
}
