using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace offlineFirebase
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public static List<Persons> collections = new List<Persons>();
        public static ObservableCollection<Persons> observableCollection= new ObservableCollection<Persons>(collections);
        //static ListView lv;
        static MainPage staticMainPage;
        public MainPage()
        {
            InitializeComponent();
            // lv = namesListview;
            staticMainPage = this;
        }


        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            try
            {
                string  name = "";
                int id=0 , age=0;
                if (txtId.Text != null )
                {
                    int.TryParse(txtId.Text,out id);
                }
                if (txtName.Text != null)
                {
                    name = txtName.Text;
                }
                if (txtAge.Text != null)
                {
                    int.TryParse(txtAge.Text, out age);
                }
                var isSaved = DependencyService.Get<IFirebaseRepository<Persons>>().SetPerson(id, name,age);
                if (!isSaved)
                {
                    await DisplayAlert("Error", "Not Saved", "OK");
                }
                else
                {
                    await DisplayAlert("Done", "New Person Is Saved", "OK");
                    txtName.Text = "";
                    txtId.Text = "";
                    txtAge.Text = "";
                    //collections = await DependencyService.Get<IFirebaseRepository<Persons>>().NewGetAll();
                    //observableCollection = new ObservableCollection<Persons>(collections);
                    //this.BindingContext = collections;
                }
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
                await DisplayAlert("Error", msg, "OK");
            }
        }

        private async void BtnRetrive_Clicked(object sender, EventArgs e)
        {
            var collections = await DependencyService.Get<IFirebaseRepository<Persons>>().NewGetAll();
            observableCollection = new ObservableCollection<Persons>(collections);
        }

        private void BtnUpdate_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnDelete_Clicked(object sender, EventArgs e)
        {

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                //var collections = await DependencyService.Get<IFirebaseRepository<Persons>>().GetAllAsync();
                await DependencyService.Get<IFirebaseRepository<Persons>>().NewGetAllAndListens();
                //observableCollection = new ObservableCollection<Persons>(collections);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                await DisplayAlert("Error", msg, "OK");
            }
        }


        public static void updateListView(List<Persons> persons )
        {
            try
            {
                collections = persons;
                observableCollection = new ObservableCollection<Persons>(collections);
                staticMainPage.namesListview.ItemsSource = observableCollection;
            }catch (Exception ex)
            {
                string msg = ex.Message;
                staticMainPage.DisplayAlert("Error", msg, "OK");
            }
        }

    }
}
