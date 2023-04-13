using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data.DAL;
using WebApi.Dtos.ProductDtos;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public ProductController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        //[Authorize(Roles ="Admin")]
        public IActionResult GetAll(string search, int page = 1)
        {
            var query = _appDbContext.Products
                .Include(p=>p.Category)
                .ThenInclude(c=>c.Products)
                .Where(p => !p.IsDelete);
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(q => q.Name.Contains(search));
            }

            var products = query.Skip((page - 1) * 2)
            .Take(2)
            .ToList();

            ProductListDto productListDto = new();
            productListDto.TotalCount = query.Count();
            productListDto.CurrentPage = page;

            productListDto.Items = products.Select(p => new ProductListItemDto
            {
                Name = p.Name,
                CostPrice = p.CostPrice,
                SalePrice = p.SalePrice,
                CreatedDate = p.CreatedDate,
                UpdatedDate = p.UpdatedDate,
                Category = new()
                {
                    Id = p.CategoryId,
                    Name = p.Category.Name,
                    ProductCount = p.Category.Products.Count
                }
            }).ToList();

            return StatusCode(200, productListDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            Product product = _appDbContext.Products
                .Include(p => p.Category)
                .Where(p => !p.IsDelete)
                .FirstOrDefault(x => x.Id == id);
            if (product == null) return StatusCode(StatusCodes.Status404NotFound);

            ProductReturnDto productReturnDto = _mapper.Map<ProductReturnDto>(product);

            return Ok(productReturnDto);
        }


        [HttpPost]
        public IActionResult AddProduct(ProductCreateDto productCreateDto)
        {
            var category = _appDbContext.Categories
                .Where(c => !c.IsDelete)
                .FirstOrDefault(c => c.Id == productCreateDto.CategoryId);
            if (category == null) return NotFound();
            Product newProduct = new()
            {
                Name = productCreateDto.Name,
                CategoryId = category.Id,
                SalePrice = productCreateDto.SalePrice,
                isActive = productCreateDto.IsActive,
                CostPrice = productCreateDto.CostPrice,
                IsDelete = productCreateDto.IsDelete
            };
            _appDbContext.Products.Add(newProduct);
            _appDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _appDbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            _appDbContext.Products.Remove(product);
            _appDbContext.SaveChanges();


            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductUpdateDto productUpdateDto)
        {
            var existProduct = _appDbContext.Products.FirstOrDefault(p => p.Id == id);
            if (existProduct == null) return NotFound();

            existProduct.Name = productUpdateDto.Name;
            existProduct.SalePrice = productUpdateDto.SalePrice;
            existProduct.CostPrice = productUpdateDto.CostPrice;
            existProduct.isActive = productUpdateDto.isActive;
            _appDbContext.SaveChanges();

            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPatch]
        public IActionResult ChangeStatus(int Id, bool isActive)
        {
            var existProduct = _appDbContext.Products.FirstOrDefault(p => p.Id == Id);
            if (existProduct == null) return NotFound();

            existProduct.isActive = isActive;
            _appDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status204NoContent);
        }

    }
}
