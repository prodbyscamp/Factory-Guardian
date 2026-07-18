using FactoryGuardian.Models;
using FactoryGuardian.Services;
using System.Collections.ObjectModel;


namespace FactoryGuardian.ViewModels
{

    public class MaintenanceViewModel
        : BaseViewModel
    {


        private readonly MaintenanceService _service;



        public ObservableCollection<MaintenanceTask>
            Tasks
        {
            get;
            set;
        }



        public MaintenanceViewModel()
        {

            _service =
                new MaintenanceService();


            Tasks =
                new ObservableCollection<MaintenanceTask>();


            Load();

        }



        private void Load()
        {

            foreach (var task
                in _service.GetTasks())
            {
                Tasks.Add(task);
            }

        }

    }

}