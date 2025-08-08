using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStoreAPI.Domain.Products.Errors
{
    public static class ProductErrors
    {
        public static Error NotFound = new(
            "Product.NotFound",
            "The product with the specified identifier was not found.");

        public static Error NameEmpty = new(
            "Product.NameEmpty",
            "The product name cannot be empty.");

        public static Error DescriptionEmpty = new(
            "Product.DescriptionEmpty",
            "The product description cannot be empty.");

        public static Error NegativePrice = new(
            "Product.NegativePrice",
            "The product price cannot be negative.");

        public static Error OutOfStock = new(
            "Product.OutOfStock",
            "The product does not have enough stock to fulfill the request.");

        public static Error InvalidStockQuantity = new(
            "Product.InvalidStockQuantity",
            "The stock quantity must be greater than or equal to zero.");

        public static Error NullValue = new(
            "Product.NullValue",
            "The product value cannot be null.");
    }
}
