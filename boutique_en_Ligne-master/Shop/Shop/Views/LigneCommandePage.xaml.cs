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
    public partial class LigneCommandePage : ContentPage
    {
        public LigneCommandePage(int idcommand)
        {
            InitializeComponent();
            BindingContext = new LigneCommandeViewModel( idcommand);

        }
          
    }
}