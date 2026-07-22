namespace FactoryGuardian.Algorithms
{
    public class Riskcalculator
    {
        // 악화 위험 영향 50%
        private const double HealthWeight = 0.5;

        // 온도 위험 영향 30%
        private const double TempWeight = 0.3;

        // 고장 가능성 반영 20%
        private const double FailureWeight = 0.2;
 
    public double CalculateRiskScore(
        double HealthScore, double currentTemp,
        double criticalTemp, int recentFailureCount)
        {
            // 헬스스코어는 높을 수록 좋은 수 이므로 100 - n으로 위험도를 변경함.
            double healthRisk = 100.0 - HealthScore;

            // 온도 위험 영향 같은 경우엔 임계온도 대비 어느 정도 떨어졌는지 계산 그리고 위험도는 최대 100으로 제한을 둠.
            double tempRatio = 0.0;
            if (criticalTemp > 0)
            {
                tempRatio = (currentTemp / criticalTemp) * 100.0;

                if (tempRatio < 0)
                    tempRatio = 0;

                if (tempRatio > 100)
                    tempRatio = 100;
            } // close if (criticalTemp > 0)

            // 최근 고장 이력 횟수에 따라 위험도를 증가시킴.
            double failureScore =
                recentFailureCount * 20.0;

            if (failureScore > 100)
                failureScore = 100;

            if (failureScore < 0)
                failureScore = 0;


        double riskScore = (healthRisk * HealthWeight) + (tempRatio * TempWeight) + (failureScore * FailureWeight);

            return riskScore;
    
}

    public string GetRiskLevel(double riskScore)
        {
            if (riskScore <= 25)
                return "Low";

            if (riskScore <= 50)
                return "Mid";

            if (riskScore <= 75)
                return "High";

                return "Critical";
        }

    }
}