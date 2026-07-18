namespace FactoryGuardian.Algorithms
{

    public class MaintenanceScoreCalculator
    {
        // 운전 시간
        private const double HourofRunning = 0.3;

        // 위험 레벨
        private const double RiskLVL = 0.4;

        // 점검지연
        private const double InspectDelays = 0.3;


    public double Calculate(
    double runningHours, double recommendedHours,
    double riskScore, double inspectionDelayedDays)

        {
            double runningScore = 0;

            if (recommendedHours > 0)
            {
                runningScore = (runningHours / recommendedHours) * 100;

                if (runningScore > 100)
                    runningScore = 100;
            }

            double delayedScore = 0;

            if (inspectionDelayedDays > 0)
            {
                delayedScore = (inspectionDelayedDays / 30.0) * 100;

                if (delayedScore > 100)
                    delayedScore = 100;
            }


            double equipmentMaintenancePriorityscore =
             (runningScore * HourofRunning) + (riskScore * RiskLVL) + (delayedScore * InspectDelays);

            if (equipmentMaintenancePriorityscore < 0)
                return 0;

            if (equipmentMaintenancePriorityscore > 100)
                return 100;

            return equipmentMaintenancePriorityscore;
        }
    public string GetPriorityLevel(double riskScore)
        {
            if (riskScore <= 30)
                return "Low";

            if (riskScore <= 55)
                return "Mid";

            if (riskScore <= 80)
                return "High";

            return "Critical";
        }


        // 설비 별 우선순위 점수를 기준으로 내림차순 정렬. (우선순위 순으로 정렬)
        public List<int> GetSortByPriority(
            Dictionary<int, double> equipmentScores)
        {
            return equipmentScores
                .OrderByDescending(x => x.Value)
                .Select(x => x.Key)
                .ToList();
        }
    }
}
