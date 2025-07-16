using Library.Order.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Order.Infrastructure.Products
{
    public class MockProductService : IProductService
    {
        private readonly List<ProductDetailsDto> _mockProducts = new List<ProductDetailsDto>
        {
            new ProductDetailsDto { ProductId = Guid.Parse("a1b2c3d4-e5f6-7890-1234-567890abcdef"), Price = 25.00m, IsPhysical = true, StockQuantity = 10 },
            new ProductDetailsDto { ProductId = Guid.Parse("b2c3d4e5-f6a7-8901-2345-67890abcdef1"), Price = 10.00m, IsPhysical = false, StockQuantity = 0 },
            new ProductDetailsDto { ProductId = Guid.Parse("c3d4e5f6-a7b8-9012-3456-7890abcdef23"), Price = 12.50m, IsPhysical = true, StockQuantity = 5 },
        };

        public Task<List<ProductDetailsDto>> GetProductDetails(List<Guid> productIds)
        {
            var foundProducts = _mockProducts.Where(p => productIds.Contains(p.ProductId)).ToList();
            return Task.FromResult(foundProducts);
        }
    }
}