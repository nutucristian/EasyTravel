using Microsoft.Maui.Controls;
using EasyTravel.Models;
using EasyTravel.Services;
using System.Linq;
using CommunityToolkit.Mvvm.Messaging;
using EasyTravel.Messages;

namespace EasyTravel.Views
{
    public partial class TripsListPage : ContentPage
    {
        public TripsListPage()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<UpdateTripsMessage>(this, (r, m) => LoadData());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadData();
        }

        private void LoadData()
        {
            TripsCollectionView.ItemsSource = TripService.GetTrips();
        }

        private async void OnTripSelected(object sender, SelectionChangedEventArgs e)
        {
            var selectedTrip = e.CurrentSelection.FirstOrDefault() as Trip;
            if (selectedTrip != null)
            {
                await Navigation.PushAsync(new TripDetailsPage(selectedTrip));
            }
        }

        private async void OnEditTripClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var trip = button?.CommandParameter as Trip;
            if (trip != null)
            {
                await Navigation.PushAsync(new EditTripPage(trip));
            }
        }

        private async void OnDeleteTripClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var trip = button?.CommandParameter as Trip;
            if (trip != null)
            {
                var confirm = await DisplayAlert("Delete Trip", $"Are you sure you want to delete the trip to {trip.Destination}?", "Yes", "No");
                if (confirm)
                {
                    TripService.DeleteTrip(trip);
                    LoadData();
                }
            }
        }
    }
}
