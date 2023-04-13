using FluentValidation;

namespace WebApi.Dtos.ProductDtos
{
    public class ProductCreateDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public double SalePrice { get; set; }
        public double CostPrice { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }

    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(x => x.CategoryId).GreaterThanOrEqualTo(1).WithMessage("1 ve ya boyuk olmalidir")
                .NotNull().WithMessage("Bos qoymaq bilmez");
            RuleFor(p => p.Name)
                .MaximumLength(50).WithMessage("50-den uzun ola bilmez")
                .NotNull().WithMessage("Bos qoymaq bilmez");
            RuleFor(p => p.SalePrice)
                .GreaterThanOrEqualTo(0).WithMessage("0-dan boyuk olmalidir")
                .NotNull().WithMessage("Bos qoymaq bilmez");
            RuleFor(p => p.CostPrice)
                .GreaterThanOrEqualTo(0).WithMessage("0-dan boyuk olmalidir")
                .NotNull().WithMessage("Bos qoymaq bilmez");
            RuleFor(p => p.IsActive)
                .Equal(true).WithMessage("mutleq true olmalidir")
                .NotNull().WithMessage("Bos qoymaq bilmez");

            RuleFor(p => p)
               .Custom((p, context) =>
               {
                   if (p.SalePrice < p.CostPrice)
                   {
                       context.AddFailure("SalePrice", "SalePrice CostPrice-dan kicik ola bolmez");
                   }
               });
        }
    }
}
