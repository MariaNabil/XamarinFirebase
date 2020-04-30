using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace offlineFirebase
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        IAuth auth;
        public SignUpPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
        }

        async void SignUpClicked(object sender, EventArgs e)
        {
            string created = auth.SignUpWithEmailPassword(EmailInput.Text, PasswordInput.Text);
            if (created.Length == 0)
            {
                await DisplayAlert("Success", "Welcome to our system. Log in to have full access", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Sign Up Failed", created, "OK");
            }
        }
    }
}