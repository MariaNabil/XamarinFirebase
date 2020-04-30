using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace offlineFirebase
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        IAuth auth;
        public LoginPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
        }

        async void LoginClicked(object sender, EventArgs e)
        {
            string Token = await auth.LoginWithEmailPassword(EmailInput.Text, PasswordInput.Text);
            
            if (Token.Substring(0, 5).Equals("error"))
            {
                await DisplayAlert("Authentication Failed", Token, "OK");
            }
            else if (Token != "")
            {
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                ShowError();
            }
        }

        async private void ShowError()
        {
            await DisplayAlert("Authentication Failed", "E-mail or password are incorrect. Try again!", "OK");
        }

        async void SignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }
    }
}
