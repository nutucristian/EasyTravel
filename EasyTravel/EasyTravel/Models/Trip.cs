using System;

namespace EasyTravel.Models
{
    public class Trip
    {
        public Guid ID { get; set; } // ID unic pentru fiecare Trip
        public string Destination { get; set; }
        public string Country { get; set; }
        public int Duration { get; set; }
        public double Rating { get; set; }
        public string Details { get; set; } // Proprietate pentru detalii

        public Trip()
        {
            ID = Guid.NewGuid(); // Generare ID unic
            Destination = string.Empty;
            Country = string.Empty;
            Details = string.Empty; // Inițializare detalii
        }
    }
}
