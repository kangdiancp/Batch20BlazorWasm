using BlazorWasm.Client.Features;
using Northwind.Contracts.Models;
using Northwind.Domain.RequestFeatures;

namespace BlazorWasm.HttpRepository
{
    public interface IProductHttpRepository
    {
        Task<List<ProductDto>> GetProducts();

        Task<PagingResponse<ProductDto>> GetProductPaging(ProductParameters productParameters);

        Task<List<SupplierDto>> GetSupplier();

        Task CreateProduct(ProductCreateDto productCreateDto);

        Task UpdateProduct(ProductDto productDto);

        Task<ProductDto> GetProductById(int id);

        Task DeleteProduct(int id);




    }
}
