// AdminCommandsViewModel.cs
using System.Windows.Input;
using Xamarin.Forms;
using Shop.Models;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Views;

namespace Shop.ViewModels
{
    public class AdminCommandsViewModel : BaseViewModel
    {
        private ObservableCollection<Commande> _commands;
        private Commande _selectedCommand;

        public ObservableCollection<Commande> Commands
        {
            get { return _commands; }
            set { SetProperty(ref _commands, value); }
        }

        public Commande SelectedCommand
        {
            get { return _selectedCommand; }
            set
            {
                SetProperty(ref _selectedCommand, value);
                ShowCommandDetails.Execute(value);
            }
        }

        public ICommand ShowCommandDetails { get; }

        public AdminCommandsViewModel()
        {
            _commands = new ObservableCollection<Commande>(App.mydataBase.ObtenirToutesLesCommandes());
            ShowCommandDetails = new Command<Commande>(ExecuteShowCommandDetails);
        }

        private void ExecuteShowCommandDetails(Commande selectedCommand)
        {
            Console.WriteLine($"Loaded  ligne de commande  for commande {selectedCommand.Id}");

            if (selectedCommand != null)
            {
                // Navigate to the LigneCommandePage
                Application.Current.MainPage.Navigation.PushAsync(new LigneCommandePage(selectedCommand.Id));
               
            }
        }
    }
}
