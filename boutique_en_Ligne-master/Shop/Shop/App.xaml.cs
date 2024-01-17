using Shop.Services;
using Shop.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shop
{
    public partial class App : Application
    {
        public static BoutiqueDataBase db;
        public static Panier shoppingCart; // Add this line

        public static Panier ShoppingCart // Add this property
        {
            get
            {
                if (shoppingCart == null)
                {
                    shoppingCart = new Panier();
                }
                return shoppingCart;
            }
        }
        public static BoutiqueDataBase mydataBase
        {
            get
            {
                if (db == null)

                {
                    db = new BoutiqueDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyStore.db3"));
                }
                return db;
            }
        }


        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
