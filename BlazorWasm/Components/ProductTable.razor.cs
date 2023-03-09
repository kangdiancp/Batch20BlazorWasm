using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Northwind.Contracts.Models;

namespace BlazorWasm.Components
{
    public partial class ProductTable
    {
        [Parameter]
        public List<ProductDto> Products { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        [Parameter]
        public EventCallback<int> OnDeleted { get; set; }

        private void RedirectToUpdate(int id)
        {
            var url = Path.Combine("/updateProduct/",id.ToString());
            NavigationManager.NavigateTo(url);
        }

        private async Task Delete(int id)
        {
            var product = Products.FirstOrDefault(p => p.ProductID.Equals(id));
            var confirmed = await Js.InvokeAsync<bool>("confirm", $"Delete product {product.ProductName} ?");
            if (confirmed)
            {
                await OnDeleted.InvokeAsync(id);
            }
        }



    }
}
