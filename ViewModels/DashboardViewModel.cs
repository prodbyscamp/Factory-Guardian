using FactoryGuardian.Models;
using FactoryGuardian.Services;


namespace FactoryGuardian.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {

        private readonly DashboardService _service;



        public DashboardViewModel()
        {
            _service =
                new DashboardService();


            Load();
        }



        private DashboardStatus _status
            = new DashboardStatus();


        public DashboardStatus Status
        {
            get => _status;

            set =>
            SetProperty(
                ref _status,
                value);
        }




        private void Load()
        {
            Status =
                _service.GetStatus();
        }

    }
}