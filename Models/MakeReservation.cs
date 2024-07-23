using Traveless.Services;

namespace Traveless.Models
{
    public class MakeReservation
    {
        private string? reservationCode;
        private string? flightCode;
        private string? airline;
        private double? cost;
        private string? name;
        private string? citizenship;
        private bool status;
        private readonly ReservationManager _reservationManager;

        public string? ReservationCode
        {
            get => reservationCode;
            set => reservationCode = value;
        }

        public string? FlightCode
        {
            get => flightCode;
            set => flightCode = value;
        }

        public string? Airline
        {
            get => airline;
            set => airline = value;
        }

        public double? Cost
        {
            get => cost;
            set => cost = value;
        }

        public string? Name
        {
            get => name;
            set => name = value;
        }

        public string? Citizenship
        {
            get => citizenship;
            set => citizenship = value;
        }

        public bool Status
        {
            get => status;
            set => status = value;
        }

        public static string StatusToString(bool status) => status ? "Active" : "Inactive";
        public MakeReservation(FlightsObj flight, string? name, string? citizenship, ReservationManager reservationManager)
        {
            if (flight.Seats == 0)
            {
                throw new InvalidOperationException("The flight is fully booked.");
            }
            if (flight == null)
            {
                throw new InvalidOperationException("There is no flight.");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidOperationException("There is no name.");
            }
            if (string.IsNullOrWhiteSpace(citizenship))
            {
                throw new InvalidOperationException("There is no citizenship.");
            }

            _reservationManager = reservationManager ?? throw new InvalidOperationException("ReservationManager is not available.");
            ReservationCode = _reservationManager.GenerateCode();
            FlightCode = flight.FlightCode;
            Airline = flight.Airline;
            Cost = flight.Cost;
            Name = name;
            Citizenship = citizenship;
            Status = true;
            flight.DecrementSeats();
            _reservationManager.AddReservation(this);
        }
    }
}

