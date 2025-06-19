using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Categories.Errors
{
    public static class CategoryErrors
    {
        public static Error NotFound = new(
            "Category.NotFound",
            "The product with the specified identifier was not found.");
    }
}
