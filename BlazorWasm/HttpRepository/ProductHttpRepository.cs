using BlazorWasm.Client.Features;
using Microsoft.AspNetCore.WebUtilities;
using Northwind.Contracts.Models;
using Northwind.Domain.Entities;
using Northwind.Domain.RequestFeatures;
using System.Text;
using System.Text.Json;

namespace BlazorWasm.HttpRepository
{
    public class ProductHttpRepository : IProductHttpRepository
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public ProductHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true};
        }

        public async Task CreateProduct(ProductCreateDto productCreateDto)
        {
            var content = JsonSerializer.Serialize(productCreateDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var postResult = await _httpClient.PostAsync("product", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public async Task DeleteProduct(int id)
        {
            var url = Path.Combine("product", id.ToString());

            var deleteResult = await _httpClient.DeleteAsync(url);
            var deleteContent = await deleteResult.Content.ReadAsStringAsync();

            if (!deleteResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(deleteContent);
            }
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var url = Path.Combine("product", id.ToString());

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var product = JsonSerializer.Deserialize<ProductDto>(content, _options);
            return product;
        }

        public async Task<PagingResponse<ProductDto>> GetProductPaging(ProductParameters productParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = productParameters.PageNumber.ToString(),
                ["searchTerm"] = productParameters.SearchTerm == null ? "" : productParameters.SearchTerm,
                ["orderBy"] = productParameters.OrderBy
            };


            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("product/pageList",queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<ProductDto>
            {
                Items = JsonSerializer.Deserialize<List<ProductDto>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
            };

            return pagingResponse;


        }

        public async Task<List<ProductDto>> GetProducts()
        {
            //call api end point, e.g 
            var response = await _httpClient.GetAsync("product");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var products = JsonSerializer.Deserialize<List<ProductDto>>(content,_options);
            return products;
        }

        public async Task<List<SupplierDto>> GetSupplier()
        {
            var response = await _httpClient.GetAsync("supplier");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var suppliers = JsonSerializer.Deserialize<List<SupplierDto>>(content, _options);
            return suppliers;
        }

        public async Task UpdateProduct(ProductDto productDto)
        {
            var content = JsonSerializer.Serialize(productDto);
            var bodyContent = new StringContent(content,Encoding.UTF8,"application/json");
            var url = Path.Combine("product", productDto.ProductID.ToString());

            var postResult = await _httpClient.PutAsync(url, bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }
    }
}
