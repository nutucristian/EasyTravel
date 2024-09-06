using Microsoft.Maui.Controls;
using EasyTravel.Models;
using EasyTravel.Services;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using EasyTravel.Messages;

namespace EasyTravel.Views
{
    public partial class TripDetailsPage : ContentPage
    {
        private Trip? _selectedTrip;
        public ObservableCollection<Trip> Trips { get; set; }
        public ObservableCollection<TripDetail> TripDetails { get; set; }

        public TripDetailsPage()
        {
            InitializeComponent();
            Trips = new ObservableCollection<Trip>(TripService.GetTrips());
            TripDetails = new ObservableCollection<TripDetail>();
            BindingContext = this;
            WeakReferenceMessenger.Default.Register<UpdateTripsMessage>(this, (r, m) => LoadTrips());
        }

        public TripDetailsPage(Trip selectedTrip) : this()
        {
            _selectedTrip = selectedTrip;
            TripDetailsEditor.Text = _selectedTrip?.Details;
        }

        private void OnTripButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            _selectedTrip = button.BindingContext as Trip;
            TripDetailsEditor.Text = _selectedTrip?.Details;
        }

        private void OnSaveDetailsClicked(object sender, EventArgs e)
        {
            if (_selectedTrip != null)
            {
                _selectedTrip.Details = TripDetailsEditor.Text;
                TripService.UpdateTrip(_selectedTrip);
                LoadTripDetails();
            }
        }

        private void LoadTrips()
        {
            Trips.Clear();
            var trips = TripService.GetTrips();
            foreach (var trip in trips)
            {
                Trips.Add(trip);
            }
            LoadTripDetails();
        }

        private void LoadTripDetails()
        {
            TripDetails.Clear();
            foreach (var trip in TripService.GetTrips())
            {
                if (!string.IsNullOrEmpty(trip.Details))
                {
                    TripDetails.Add(new TripDetail { Destination = trip.Destination, Details = trip.Details });
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadTrips();
        }
    }

    public class TripDetail
    {
        public string Destination { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
    }
}
