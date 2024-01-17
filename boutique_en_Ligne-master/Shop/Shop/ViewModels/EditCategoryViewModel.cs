using Shop.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Shop.ViewModels
{
    public class EditCategoryViewModel : BaseViewModel
    {
        private Categorie _category;

        public Categorie Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
        }

        public ICommand SaveCommand { get; }
        public EditCategoryViewModel()
        {
            // Default constructor logic, if needed
        }
        public EditCategoryViewModel(Categorie selectedCategory)
        {
            Category = selectedCategory ?? new Categorie();

            SaveCommand = new Command(OnSave);
            Console.WriteLine("EditCategoryViewModel constructor called");
        }

        private void OnSave()
        {
            if (Category.Id == 0)
            {
                // Nouvelle catégorie
                App.mydataBase.AjouterCategorie(Category);
            }
            else
            {
                // Catégorie existante
                App.mydataBase.ModifierCategorie(Category);
            }

            Console.WriteLine("Category saved successfully!");
            Application.Current.MainPage.Navigation.PopAsync(); // Naviguer en arrière
        }

    }
}