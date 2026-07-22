using FactoryGuardian.Models;
using FactoryGuardian.Repositories;

namespace FactoryGuardian.Services
{
    public class DashboardService
    {

        private readonly EquipmentRepository _repository;



        public DashboardService()
        {
            _repository =
                new EquipmentRepository("Data Source=factory.db");
        }




        public DashboardStatus GetStatus()
        {

            var equipments =
                _repository.GetAll();



            var result =
                new DashboardStatus();



            result.TotalEquipment =
                equipments.Count;



            result.RunningCount =
                equipments.Count(
                    x => x.Status == "Running");



            result.ErrorCount =
                equipments.Count(
                    x => x.Status == "Error");



            result.InspectionDue =
                equipments.Count(
                    x =>
                    x.NextInspection != null &&
                    x.NextInspection <= DateTime.Today);



            if (equipments.Count > 0)
            {
                result.AverageHealth =
                    equipments.Average(
                        x => x.HealthScore);


                result.AverageRisk =
                    equipments.Average(
                        x => x.RiskScore);
            }


            return result;
        }

    }
}