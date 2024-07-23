using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Traveless.Models;
using Traveless.Services;

namespace Traveless.ViewModels
{
    public class FlightsViewModel : INotifyPropertyChanged
    {
        private string? reservationCode;
        private readonly FindFlights? _findFlightsService;
        private readonly ReservationManager? _reservationManager;
        private readonly FindReservation? _findReservation;
        private List<FlightsObj>? _flightResults;
        private FlightsObj? _selectedFlight;
        private string? selectedFlightCode;
        private string? from;
        private string? to;
        private string? day;
        private string? _name;
        private string? _citizenship;
        private string? resCode;
        private string? resAirline;
        private string? resName;
        private List<MakeReservation>? _resResults;
        private MakeReservation? _selectedReservation;
        private string? selectedReservationCode;
        private bool? resStatus;

        public string? ResCode
        {
            get => resCode;
            set
            {
                if (resCode != value)
                {
                    resCode = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? ResAirline
        {
            get => resAirline;
            set
            {
                if (resAirline != value)
                {
                    resAirline = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? ResName
        {
            get => resName;
            set
            {
                if (resName != value)
                {
                    resName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? From
        {
            get => from;
            set
            {
                if (from != value)
                {
                    from = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? To
        {
            get => to;
            set
            {
                if (to != value)
                {
                    to = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? Day
        {
            get => day;
            set
            {
                if (day != value)
                {
                    day = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<FlightsObj>? FlightResult
        {
            get => _flightResults;
            set
            {
                if (_flightResults != value)
                {
                    _flightResults = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<MakeReservation>? ResResults
        {
            get => _resResults;
            set
            {
                if (_resResults != value)
                {
                    _resResults = value;
                    OnPropertyChanged();
                }
            }
        }
        public FlightsObj? SelectedFlight
        {
            get => _selectedFlight;
            set
            {
                if (_selectedFlight != value)
                {
                    _selectedFlight = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? SelectedFlightCode
        {
            get => selectedFlightCode;
            set
            {
                if (selectedFlightCode != value)
                {
                    selectedFlightCode = value;
                    OnPropertyChanged();
                    SelectedFlight = FlightResult?.FirstOrDefault(f => f.FlightCode == selectedFlightCode);
                }
            }
        }
        public MakeReservation? SelectedReservation
        {
            get => _selectedReservation;
            set
            {
                if (_selectedReservation != value)
                {
                    _selectedReservation = value;
                    OnPropertyChanged();
                    if (_selectedReservation != null)
                    {
                        ResStatus = _selectedReservation.Status;
                    }
                }
            }
        }
        public string? SelectedReservationCode
        {
            get => selectedReservationCode;
            set
            {
                if (selectedReservationCode != value)
                {
                    selectedReservationCode = value;
                    OnPropertyChanged();
                    SelectedReservation = ResResults?.FirstOrDefault(f => f.ReservationCode == selectedReservationCode);
                }
            }
        }
        public string? ReservationCode
        {
            get => reservationCode;
            set
            {
                if (reservationCode != value)
                {
                    reservationCode = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? Citizenship
        {
            get => _citizenship;
            set
            {
                if (_citizenship != value)
                {
                    _citizenship = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool? ResStatus
        {
            get => resStatus;
            set
            {
                if (resStatus != value)
                {
                    resStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SearchCommand { get; }
        public ICommand ReserveCommand { get; }
        public ICommand ResSearchCommand { get; }
        public ICommand ResUpdateCommand { get; }

        public FlightsViewModel(FindFlights findFlightsService, ReservationManager reservationManager, FindReservation findReservation)
        {
            _findFlightsService = findFlightsService ?? throw new ArgumentNullException(nameof(findFlightsService));
            _reservationManager = reservationManager ?? throw new ArgumentNullException(nameof(reservationManager));
            _findReservation = findReservation ?? throw new ArgumentNullException(nameof(findReservation));
            SearchCommand = new Command(OnSearch);
            ReserveCommand = new Command(OnReserve);
            ResSearchCommand = new Command(OnResSearch);
            ResUpdateCommand = new Command(OnResUpdate);
        }

        private void OnSearch()
        {
            SelectedFlightCode = null;
            if (From == "Any" || To == "Any" || Day == "Any" || From == null || To == null || Day == null)
            {
                return;
            }

            if (_findFlightsService != null)
            {
                var results = _findFlightsService.ValidateAndSearch(From, To, Day);

                if (results.Count == 0)
                {
                    return;
                }

                FlightResult = results;
            }
        }

        private async void OnReserve()
        {
            try
            {
                if (SelectedFlight == null || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Citizenship))
                {
                    throw new Exception("You must select a flight and both Name and Citizenship must not be null or empty.");
                }
                if (_reservationManager == null)
                {
                    throw new InvalidOperationException("ReservationManager is not available.");
                }
                var reservation = new MakeReservation(SelectedFlight, Name, Citizenship, _reservationManager);
                _reservationManager.AddReservation(reservation);
                _reservationManager.Persist();
                ReservationCode = reservation.ReservationCode;
            }
            catch (Exception ex)
            {
                await ShowErrorPopup(ex.Message);
            }
        }

        private static Task ShowErrorPopup(string message)
        {
            var mainPage = Application.Current?.MainPage;
            if (mainPage != null)
            {
                return mainPage.DisplayAlert("Error", message, "OK");
            }
            else
            {
                throw new InvalidOperationException("MainPage is not set.");
            }
        }

        private void OnResSearch()
        {
            if (_findReservation != null)
            {
                var (results, errorMessage) = _findReservation.FindReservations(ResCode, ResAirline, ResName);

                if (errorMessage != null)
                {
                    _ = ShowErrorPopup(errorMessage);
                }
                else
                {
                    ResResults = results;
                }
            }
        }

        private void OnResUpdate()
        {
            if (SelectedReservation != null)
            {
                SelectedReservation.Name = Name;
                SelectedReservation.Citizenship = Citizenship;
                SelectedReservation.Status = ResStatus ?? true;

                _reservationManager?.Persist();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}