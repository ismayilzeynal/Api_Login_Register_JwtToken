namespace WebApi.Dtos.CategoryDtos
{
    public class CategoryListDto
    {
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public List<CategoryListItemDto> Items { get; set; }
    }
}
