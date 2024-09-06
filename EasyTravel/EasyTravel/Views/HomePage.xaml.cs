// Views/HomePage.xaml.cs
using Microsoft.Maui.Controls;

namespace EasyTravel.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void OnAddNewTripClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTripPage());
        }
    }
}
