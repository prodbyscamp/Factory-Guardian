using FactoryGuardian.Repositories;
using FactoryGuardian.Algorithms;
using FactoryGuardian.Models;

namespace FactoryGuardian.Services
{
    public class EquipmentService
    {
        private readonly Riskcalculator _riskCalculator;
        private readonly MaintenanceScoreCalculator _priorityCalculator;
        private readonly EquipmentLifeCalculator _healthCalculator;

        public EquipmentService()
        {
            _healthCalculator = new EquipmentLifeCalculator();
            _riskCalculator = new Riskcalculator();
            _priorityCalculator = new MaintenanceScoreCalculator();
        }



        public void UpdateStatus(Equipment equipment)
        {
            // 기계의 컨디션 값

            equipment.HealthScore =
                _healthCalculator.CalculateHealth(
                    equipment.Temperature,
                    60,
                    100,
                    equipment.Vibration,
                    4.5,
                    11,
                    equipment.RunHours,
                    1000,
                    0
                );


            // 위험도 값

            equipment.RiskScore =
                _riskCalculator.CalculateRiskScore(
                    equipment.HealthScore,
                    equipment.Temperature,
                    100,
                    0
                );


            // 우선순위 값

            equipment.PriorityScore =
                _priorityCalculator.Calculate(
                    equipment.RunHours,
                    1000,
                    equipment.RiskScore,
                    0
                );
        }
    }
}