// Views/AddTripPage.xaml.cs
using Microsoft.Maui.Controls;
using EasyTravel.Models;
using EasyTravel.Services;

namespace EasyTravel.Views
{
    public partial class AddTripPage : ContentPage
    {
        public AddTripPage()
        {
            InitializeComponent();
        }

        private async void OnSaveTripClicked(object sender, EventArgs e)
        {
            var trip = new Trip
            {
                Destination = DestinationEntry.Text,
                Country = CountryEntry.Text,
                Duration = int.Parse(DurationEntry.Text),
                Rating = double.Parse(RatingEntry.Text)
            };

            TripService.SaveTrip(trip);

            await DisplayAlert("Success", "Trip saved successfully!", "OK");
            await Navigation.PopAsync();
        }
    }
}
