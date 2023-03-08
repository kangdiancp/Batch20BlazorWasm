using BlazorWasm.HttpRepository;
using Microsoft.AspNetCore.Components;
using Northwind.Contracts.Models;
using Northwind.Domain.Entities;
using Northwind.Domain.RequestFeatures;

namespace BlazorWasm.Pages
{
    public partial class ProductPaging
    {
        public List<ProductDto> ProductListPaging { get; set; } = new List<ProductDto>();
        public MetaData MetaData { get; set; } = new MetaData();

        private ProductParameters _productParameters = new ProductParameters();

        [Inject]
        public IProductHttpRepository ProductRepo { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await GetProductPaging();
        }

        private async Task SelectedPage(int page)
        {
            _productParameters.PageNumber = page;
            await GetProductPaging();
        }

        private async Task GetProductPaging()
        {
            var pagingResponse = await ProductRepo.GetProductPaging(_productParameters);
            ProductListPaging = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }
    }
}
