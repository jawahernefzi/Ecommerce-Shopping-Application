using Shop.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Shop.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginButtonClicked);
        }

        private void OnLoginButtonClicked()
        {
            // Validate credentials and perform login logic
            if (IsValidCredentials())
            {
                // Navigate to the home page or perform any other desired action
                Application.Current.MainPage.Navigation.PushAsync(new AdminPage());
            }
            else
            {
                // Display an error message
                Application.Current.MainPage.DisplayAlert("Error", "incorrect username or password.", "OK");
            }
        }

        private bool IsValidCredentials()
        {
            // Implement your validation logic here
            return Username == "admin" && Password == "admin";
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
