using FactoryGuardian.Models;
using FactoryGuardian.Services;
using System.Collections.ObjectModel;


namespace FactoryGuardian.ViewModels
{

    public class EquipmentViewModel : BaseViewModel
    {

        private readonly EquipmentService _service;



        public ObservableCollection<Equipment> Equipments
        {
            get;
            set;
        }



        public EquipmentViewModel()
        {

            _service =
                new EquipmentService();


            Equipments =
                new ObservableCollection<Equipment>();


            Load();

        }



        private void Load()
        {

            // 나중에 Repository 연결
                
            var list =
                service.GetAll();


            foreach (var item in list)
            {
                Equipments.Add(item);   
            }

        }

    }

}