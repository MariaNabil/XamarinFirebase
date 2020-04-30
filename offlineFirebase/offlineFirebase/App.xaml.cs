using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace offlineFirebase
{
    public partial class App : Application
    {
       public static Action updateListView;
        public static App Current;

        public App()
        {
            InitializeComponent();
            Current = this;
            MainPage = new NavigationPage( new LoginPage());
        }

        public void Update()
        {
            MainPage = new NavigationPage(new MainPage());
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
