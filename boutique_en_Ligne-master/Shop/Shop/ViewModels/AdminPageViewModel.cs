using Shop.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Shop.ViewModels
{
    public class AdminPageViewModel
    {
        public Command NavigateToProductsCommand { get; }
        public Command NavigateToCategoriesCommand { get; }
        public Command NavigateToCommandsCommand { get; }

        public AdminPageViewModel()
        {
            NavigateToProductsCommand = new Command(OnNavigateToProducts);
            NavigateToCategoriesCommand = new Command(OnNavigateToCategories);
             NavigateToCommandsCommand = new Command(OnNavigateToCommands);
        }

        private async void OnNavigateToProducts()
          {
            await Shell.Current.Navigation.PushAsync(new ProductsPageAdmin());
          } 

        private async void OnNavigateToCategories()
        {
            await Shell.Current.Navigation.PushAsync(new AdminCategoriesPage());
        }
        
        private async void OnNavigateToCommands()
        {
          await Shell.Current.Navigation.PushAsync(new AdminCommandsPage());
        }
    }
}
