using BlazorWasm.Client.Features;
using Northwind.Contracts.Models;
using Northwind.Domain.RequestFeatures;

namespace BlazorWasm.HttpRepository
{
    public interface IProductHttpRepository
    {
        Task<List<ProductDto>> GetProducts();

        Task<PagingResponse<ProductDto>> GetProductPaging(ProductParameters productParameters);
    }
}
