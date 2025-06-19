using FluentValidation;

namespace OnlineStore.Application.Products.Queries.GetPagedProducts
{
    public sealed class GetPagedProductsQueryValidator : AbstractValidator<GetPagedProductsQuery>
    {
        public GetPagedProductsQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("Page must be greater than 0.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage("PageSize must be between 1 and 100.");
        }
    }
}
