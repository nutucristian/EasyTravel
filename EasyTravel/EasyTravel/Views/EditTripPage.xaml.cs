using Microsoft.Maui.Controls;
using EasyTravel.Models;
using EasyTravel.Services;
using CommunityToolkit.Mvvm.Messaging; // Asigură-te că acest using este adăugat
using EasyTravel.Messages;

namespace EasyTravel.Views
{
    public partial class EditTripPage : ContentPage
    {
        private Trip _trip;

        public EditTripPage(Trip trip)
        {
            InitializeComponent();
            _trip = trip;
            BindingContext = _trip;
        }

        private async void OnSaveTripClicked(object sender, EventArgs e)
        {
            TripService.UpdateTrip(_trip);
            WeakReferenceMessenger.Default.Send(new UpdateTripsMessage());
            await DisplayAlert("Success", "Trip updated successfully!", "OK");
            await Navigation.PopAsync();
        }
    }
}
