namespace Sales.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Common.Models;
    using Services;

    public class ProductsViewModel
    {
        private ApiService apiService;

        public ObservableCollection<Product> Products { get; set; }

        public ProductsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();

        }

        private async Task LoadProducts()
        {
            var response = await apiService.GetList<Product>("https://salesapiservices.azurewebsites.net", "/api", "/Products");
            if (response.IsSuccess)
            {
                var products = (List<Product>)response.Result;
                this.Products = new ObservableCollection<Product>(products);
            }
        }
    }
}