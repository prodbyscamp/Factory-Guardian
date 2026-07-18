namespace FactoryGuardian.Algorithms
{ 

public class InspectionCalculator

{
    private const int DefaultCycleDays = 30;

    // 현재 기계 상태 반영한 다음 점검 예상 일 계산
    public DateTime CalculateNextInspectionDate(
        DateTime lastInspectedDate, double equipScore, int baseCycleDays = 0)
    {
            int cycleDays = baseCycleDays > 0
                ? baseCycleDays
                : DefaultCycleDays;

        // 점검 주기 조정
        double adjustmentFactor = GetAdjustmentFactor(equipScore);

        int adjustedDays = (int)Math.Round(cycleDays * adjustmentFactor);


        // 최소 점검 간격
        if (adjustedDays < 7)
            adjustedDays = 7;

        return lastInspectedDate.AddDays(adjustedDays);
    }

    // 점수에 따른 점검주기 보정값
    private double GetAdjustmentFactor(double EquipScore) {

        // 양호 할 시 (점검 주기 연장)
        if (EquipScore >= 80)
            return 1.2;

        // 정상
        if (EquipScore >= 60)
            return 1.0;

        // 주의 상태 (점검 주기 앞당김)
        if (EquipScore >= 40)
            return 0.7;

        // 위험
        return 0.5;

    }

    public int GetDelayDays(DateTime nextInspectionDate)
    {
        return (DateTime.Today - nextInspectionDate.Date).Days;
    }
}
}