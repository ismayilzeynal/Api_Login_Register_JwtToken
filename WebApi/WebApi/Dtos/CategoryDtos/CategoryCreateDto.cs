using FluentValidation;
using WebApi.Dtos.ProductDtos;

namespace WebApi.Dtos.CategoryDtos
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public IFormFile Photo { get; set; }
        public bool IsDelete { get; set; }
    }




    public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValidator()
        {
            RuleFor(c => c.Name)
                .MaximumLength(50).WithMessage("50-den uzun ola bilmez")
                .NotNull().WithMessage("Bos qoymaq bilmez");
            RuleFor(c => c.Desc)
               .MaximumLength(400).WithMessage("50-den uzun ola bilmez");
            RuleFor(c => c.IsDelete)
                .Equal(false).WithMessage("mutleq false olmalidir")
                .NotNull().WithMessage("Bos qoymaq bilmez");
        }
    }
}
