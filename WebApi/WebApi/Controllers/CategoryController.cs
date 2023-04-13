using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data.DAL;
using WebApi.Dtos.CategoryDtos;
using WebApi.Dtos.ProductDtos;
using WebApi.Extensions;
using WebApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.WebRequestMethods;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public CategoryController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetAll(string? search, int page = 1)
        {
            var query = _appDbContext.Categories
                .Where(c => !c.IsDelete);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(q => q.Name.Contains(search));

            var categories = query.Skip((page - 1) * 2)
            .Take(2)
            .ToList();

            CategoryListDto categoryListDto = new();
            categoryListDto.TotalCount = query.Count();
            categoryListDto.CurrentPage = page;
            categoryListDto.Items = categories.Select(c => new CategoryListItemDto
            {
                Name = c.Name,
                Desc = c.Desc,
                ImageUrl = "https://localhost:7197/img/" + c.ImageUrl,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate
            }).ToList();

            return StatusCode(200, categoryListDto);
        }




        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            Category category = _appDbContext.Categories
                .Include(c=>c.Products)
                .Where(c => !c.IsDelete)
                .FirstOrDefault(x => x.Id == id);
            if (category == null) return StatusCode(StatusCodes.Status404NotFound);

            CategoryReturnDto categoryReturnDto = _mapper.Map<CategoryReturnDto>(category);
            return Ok(categoryReturnDto);
        }


        [HttpPost]
        public IActionResult AddCategory([FromForm] CategoryCreateDto categoryCreateDto)
        {
            if (categoryCreateDto.Photo == null) return StatusCode(409);
            if (!categoryCreateDto.Photo.IsImage()) return BadRequest("photo type deyil");
            if (!categoryCreateDto.Photo.CheckSize(1)) return BadRequest("size duzgun deyil");


            Category newCategory = new()
            {
                Name = categoryCreateDto.Name,
                Desc = categoryCreateDto.Desc,
                ImageUrl = categoryCreateDto.Photo.SaveImage(_webHostEnvironment, "img", categoryCreateDto.Photo.FileName),
                IsDelete = categoryCreateDto.IsDelete,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };
            _appDbContext.Categories.Add(newCategory);
            _appDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created, newCategory);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            _appDbContext.Categories.Remove(category);
            _appDbContext.SaveChanges();

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, CategoryUpdateDto categoryUpdateDto)
        {
            if (id == null) return NotFound();      
            var existCatgeory = _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (existCatgeory == null) return NotFound();
            bool result = _appDbContext.Categories.Any(c => c.Name.ToLower() == categoryUpdateDto.Name.ToLower() && c.Id != id);
            if (result) return StatusCode(409);

            if (categoryUpdateDto.Photo == null) return StatusCode(409);
            if (!categoryUpdateDto.Photo.IsImage()) return BadRequest("photo type deyil");
            if (!categoryUpdateDto.Photo.CheckSize(1)) return BadRequest("size duzgun deyil");


            existCatgeory.Name = categoryUpdateDto.Name;
            existCatgeory.Desc = categoryUpdateDto.Desc;
            // img deyisikliyi ucun elave mentiq yazmaq lazimdi
            // existCatgeory.ImageUrl = categoryUpdateDto.Photo.SaveImage(_webHostEnvironment, "img", categoryUpdateDto.Photo.FileName);
            existCatgeory.IsDelete = categoryUpdateDto.IsDelete;
            _appDbContext.SaveChanges();

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPatch]
        public IActionResult ChangeStatus(int id, bool isActive)
        {
            var existCatgeory = _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (existCatgeory == null) return NotFound();

            existCatgeory.IsDelete = isActive;
            _appDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
