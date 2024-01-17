using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminCommandsPage : ContentPage
    {
        public AdminCommandsPage()
        {
            InitializeComponent();
            BindingContext = new AdminCommandsViewModel();

        }
    }
}