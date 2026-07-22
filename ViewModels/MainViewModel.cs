using System.Windows.Input;

namespace FactoryGuardian.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private object _currentView;

        public ICommand DashboardCommand { get; set; }
        public ICommand RobotCommand { get; set; }
        public ICommand EquipmentCommand { get; set; }
        public ICommand AlertsCommand { get; set; }
        public ICommand MaintenanceCommand { get; set; }
        public ICommand ToggleThemeCommand { get; set; }

        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public MainViewModel()
        {
            DashboardCommand = new RelayCommand(o => CurrentView = new DashboardViewModel());
            RobotCommand = new RelayCommand(o => CurrentView = new RobotMonitorViewModel());
            EquipmentCommand = new RelayCommand(o => CurrentView = new EquipmentViewModel());
            AlertsCommand = new RelayCommand(o => CurrentView = new AlertsViewModel());
            MaintenanceCommand = new RelayCommand(o => CurrentView = new MaintenanceViewModel());
            ToggleThemeCommand = new RelayCommand(o => { /* Theme toggle logic later */ });

            // Set default view
            CurrentView = new DashboardViewModel();
        }
    }
}
