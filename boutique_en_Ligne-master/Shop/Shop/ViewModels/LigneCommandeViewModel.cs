// LigneCommandeViewModel.cs
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shop.Models;

namespace Shop.ViewModels
{
    public class LigneCommandeViewModel : BaseViewModel
    {
        private ObservableCollection<LigneCommande> _lignesCommande;

        public ObservableCollection<LigneCommande> LignesCommande
        {
            get { return _lignesCommande; }
            set { SetProperty(ref _lignesCommande, value); }
        }

        public LigneCommandeViewModel(int id)
        {
            List<LigneCommande> lignesCommandeList = App.mydataBase.ObtenirLesLignesCommande(id);

            if (id != 0 && lignesCommandeList != null)
            {
                LignesCommande = new ObservableCollection<LigneCommande>(lignesCommandeList);
                Console.WriteLine($"Loaded  offfffffff {LignesCommande.Count} ");
            }
            else
            {
                // Handle the case where either the command or its lines are not found
                LignesCommande = new ObservableCollection<LigneCommande>();
            }

            OnPropertyChanged(nameof(LignesCommande));
        }

        public LigneCommandeViewModel()
        {
        }
    }
}
