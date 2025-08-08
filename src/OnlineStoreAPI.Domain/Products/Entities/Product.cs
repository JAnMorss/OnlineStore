using OnlineStoreAPI.Domain.Products.ValueObjects;
using OnlineStoreAPI.Domain.Shared;
using OnlineStoreAPI.Shared.Kernel.Domain;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;
using OnlineStoreAPI.Domain.Categories.Entities;
using OnlineStoreAPI.Domain.Products.Events;
using OnlineStoreAPI.Domain.Products.Errors;

namespace OnlineStoreAPI.Domain.Products.Entities
{
    public sealed class Product : BaseEntity
    {
        private Product() 
        {
            Name = null!;
            Description = null!;
            Price = Money.Zero(Currency.None);
            Stock = Stock.Empty;
        }

        public Product(
            Guid id,
            ProductName name,
            ProductDescription description,
            Money price,
            Stock stock, 
            Guid categoryId) : base(id)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            CategoryId = categoryId;
        }

        public ProductName Name { get; private set; }

        public ProductDescription Description { get; private set; }

        public Money Price { get; private set; }

        public Stock Stock { get; private set; }

        public Guid CategoryId { get; private set; }

        public Category Category { get; private set; } = null!;

        public string NameText => Name.Value;

        public static Result<Product> Create(
            Category category,
            ProductName name,
            ProductDescription description,
            decimal amount,
            Currency currency,
            Stock stock)
        {
            if (amount < 0)
                return Result.Failure<Product>(ProductErrors.NegativePrice);

            var money = new Money(amount, currency);

            var product = new Product(
                Guid.NewGuid(),
                name,
                description,
                money,
                stock,
                category.Id); 

            product.RaiseDomainEvent(new ProductCreatedDomainEvent(product.Id));

            return Result.Success(product);
        }

        public void UpdateDetails(
            ProductName name, 
            ProductDescription description, 
            Money price, 
            Stock stock)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;

            RaiseDomainEvent(new ProductUpdatedDomainEvent(Id));
        }


        public void UpdateStock(Stock newStock)
        {
            Stock = newStock;
            RaiseDomainEvent(new StockUpdatedDomainEvent(Id, Stock.Quantity));
        }


        public Result DecreaseStock(int quantity)
        {
            var result = Stock.Decrease(quantity);

            if (result.IsFailure)
                return Result.Failure(ProductErrors.OutOfStock);

            Stock = result.Value;

            RaiseDomainEvent(new StockDecreasedDomainEvent(Id, Stock.Quantity));

            return Result.Success();
        }

        public Result IncreaseStock(int quantity)
        {
            if (quantity <= 0)
                return Result.Failure(ProductErrors.InvalidStockQuantity);

            Stock = Stock.Increase(quantity);

            RaiseDomainEvent(new StockIncreasedDomainEvent(Id, Stock.Quantity, quantity));

            return Result.Success();
        }

        public Result UpdatePrice(Money newPrice)
        {
            if (newPrice.Amount < 0)
                return Result.Failure(ProductErrors.NegativePrice);

            Price = newPrice;

            RaiseDomainEvent(new ProductPriceUpdatedDomainEvent(Id, newPrice.Amount, newPrice.Currency));

            return Result.Success();
        }


    }
}

