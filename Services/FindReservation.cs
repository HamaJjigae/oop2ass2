using Traveless.Models;

namespace Traveless.Services
{
    public class FindReservation
    {
        private readonly ReservationManager _reservationManager;

        public FindReservation(ReservationManager reservationManager)
        {
            _reservationManager = reservationManager ?? throw new ArgumentNullException(nameof(reservationManager));
        }

        public (List<MakeReservation> Reservations, string? ErrorMessage) FindReservations(string? reservationCode, string? airline, string? name)
        {
            if (string.IsNullOrEmpty(reservationCode) && string.IsNullOrEmpty(airline) && string.IsNullOrEmpty(name))
            {
                return (new List<MakeReservation>(), "At least one search parameter is required.");
            }

            var reservations = _reservationManager.Reservations;

            var filteredReservations = reservations.AsEnumerable();

            if (!string.IsNullOrEmpty(reservationCode))
            {
                filteredReservations = filteredReservations.Where(r => r.ReservationCode == reservationCode);
            }

            if (!string.IsNullOrEmpty(airline))
            {
                filteredReservations = filteredReservations.Where(r => r.Airline == airline);
            }

            if (!string.IsNullOrEmpty(name))
            {
                filteredReservations = filteredReservations.Where(r => r.Name == name);
            }

            return (filteredReservations.ToList(), null);
        }
    }
}
