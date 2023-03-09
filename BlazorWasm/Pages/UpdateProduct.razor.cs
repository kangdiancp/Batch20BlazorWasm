

using BlazorWasm.HttpRepository;
using BlazorWasm.Shared;
using Microsoft.AspNetCore.Components;
using Northwind.Contracts.Models;
using Northwind.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWasm.Pages
{
    public partial class UpdateProduct
    {
        private ProductDto _product = new ProductDto();
        private List<SupplierDto> suppliers = new List<SupplierDto>();

		private SuccessNotification? _notification;

        [Parameter]
        public int Id { get; set; }

		[Inject]
        public IProductHttpRepository? ProductRepo { get; set; }

        private async Task Update()
        {
            await ProductRepo.UpdateProduct(_product);
            _notification.Show("/productPaging");
        }

        protected async override Task OnInitializedAsync()
        {
            //fetch data product agar bisa tampil di page update
            _product = await ProductRepo.GetProductById(Id);
            
            // call method getSupplier to fill suppliers variable, execute before page createProduct rendered
            suppliers = await ProductRepo.GetSupplier();

        }
    }
}
