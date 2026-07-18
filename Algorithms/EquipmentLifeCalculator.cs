namespace FactoryGuardian.Algorithms
{

    public class EquipmentLifeCalculator // 기계 수명 계산
    {
        // 합계 100점
        public const double TempMaxPenalty = 30.0; 
        public const double VibrationMaxPenalty = 25.0; 
        public const double RunningMaxPenalty = 20.0; 
        public const double InspectionDelayMaxPenalty = 25.0;


        public double CalculateHealth(
            double currentTemp, double normalTemp, double criticalTemp,

            double currentVibration, double normalVibrationMax, double criticalVibration,

            double runningHours, double recommendedMaintenanceHours,

            int inspectionDelayedDays)

        {
            // 1. 온도 감점 계산
            double tempPenalty = CalculateTempPenalty(
                currentTemp, normalTemp, criticalTemp);

            // 2. 진동 감점 계산
            double vibrationPenalty = CalculateVibrationPenalty(
                currentVibration, normalVibrationMax, criticalVibration);

            // 3. 운전 시간 감점 계산
            double runningPenalty = CalculateRunningHoursPenalty(
                runningHours, recommendedMaintenanceHours);

            // 4. 점검 지연 감점 계산
            double delayPenalty = CalculateInspectionDelayPenalty(inspectionDelayedDays);

            // 5. 총점 계산
            double healthScore = 100.0 - tempPenalty - vibrationPenalty - runningPenalty - delayPenalty;

            // 점수 보정*
            return Math.Max(0, Math.Min(100, healthScore));
        }


        // 정상범위를 초과한 값이 임계치까지 얼마나 도달했는지 비율 계산 (온도)

        private double CalculateTempPenalty(
            double current, double normalMax, double critical)
        {
            if (current <= normalMax)
                return 0.0;

            double ratio =
                (current - normalMax) / (critical - normalMax);

            double penalty = ratio * TempMaxPenalty;

            if (penalty < 0.0)
                return 0.0;

            if (penalty > TempMaxPenalty)
                return TempMaxPenalty;

            return penalty;
        }

        // 정상범위를 초과한 값이 임계치까지 얼마나 도달했는지 비율 계산 (진동)

        private double CalculateVibrationPenalty(
           double current, double normalMax, double critical)
        {
            if (current <= normalMax)
                return 0.0;

            double ratio =
                (current - normalMax) / (critical - normalMax);

            double penalty = ratio * VibrationMaxPenalty;

            if (penalty < 0.0)
                return 0.0;

            if (penalty > VibrationMaxPenalty)
                return VibrationMaxPenalty;

            return penalty;
        }

        //
        private double CalculateRunningHoursPenalty(
           double runningHours, double recommendedMaintenanceHours)
        {
            if (recommendedMaintenanceHours <= 0.0)
                return 0.0;

            double ratio =
                 runningHours / recommendedMaintenanceHours;

            if (ratio < 0.0)
                ratio = 0.0;

            if (ratio > 1.0)
                ratio = 1.0;

            return ratio * recommendedMaintenanceHours;
        }

        // 
        private double CalculateInspectionDelayPenalty(int delayedDays)

        {
            if (delayedDays <= 0)
                return 0.0;

            double ratio = (double)delayedDays / 30.0;

            if (ratio > 1.0)
                ratio = 1.0;

            return ratio * InspectionDelayMaxPenalty;
        }
    }
}