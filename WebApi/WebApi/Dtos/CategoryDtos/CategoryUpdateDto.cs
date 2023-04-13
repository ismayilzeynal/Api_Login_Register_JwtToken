namespace WebApi.Dtos.CategoryDtos
{
    public class CategoryUpdateDto
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public IFormFile Photo { get; set; }
        public bool IsDelete { get; set; }
    }
}
