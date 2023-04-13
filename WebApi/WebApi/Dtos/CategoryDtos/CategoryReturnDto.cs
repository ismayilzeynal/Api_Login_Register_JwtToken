namespace WebApi.Dtos.CategoryDtos
{
    public class CategoryReturnDto
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int ProductsCount { get; set; }
    }
}
