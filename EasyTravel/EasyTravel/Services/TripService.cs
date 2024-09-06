using Microsoft.Maui.Storage;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using EasyTravel.Models;
using CommunityToolkit.Mvvm.Messaging;
using EasyTravel.Messages;

namespace EasyTravel.Services
{
    public static class TripService
    {
        private const string TripsKey = "Trips";

        public static List<Trip> GetTrips()
        {
            var tripsJson = Preferences.Get(TripsKey, string.Empty);
            return string.IsNullOrEmpty(tripsJson) ? new List<Trip>() : JsonConvert.DeserializeObject<List<Trip>>(tripsJson) ?? new List<Trip>();
        }

        public static void SaveTrip(Trip trip)
        {
            var trips = GetTrips();
            trips.Add(trip);
            var tripsJson = JsonConvert.SerializeObject(trips);
            Preferences.Set(TripsKey, tripsJson);
            WeakReferenceMessenger.Default.Send(new UpdateTripsMessage());
        }

        public static void DeleteTrip(Trip trip)
        {
            var trips = GetTrips();
            var tripToRemove = trips.FirstOrDefault(t => t.ID == trip.ID);
            if (tripToRemove != null)
            {
                trips.Remove(tripToRemove);
                var tripsJson = JsonConvert.SerializeObject(trips);
                Preferences.Set(TripsKey, tripsJson);
                WeakReferenceMessenger.Default.Send(new UpdateTripsMessage());
            }
        }

        public static void UpdateTrip(Trip updatedTrip)
        {
            var trips = GetTrips();
            var index = trips.FindIndex(t => t.ID == updatedTrip.ID);
            if (index != -1)
            {
                trips[index] = updatedTrip;
                var tripsJson = JsonConvert.SerializeObject(trips);
                Preferences.Set(TripsKey, tripsJson);
                WeakReferenceMessenger.Default.Send(new UpdateTripsMessage());
            }
        }
    }
}
