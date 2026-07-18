namespace FactoryGuardian.Models
{
    public class DashboardStatus : BaseModel
    {
        // 전체 설비 개수
        public int TotalEquipment { get; set; }


        // 현재 가동 중인 equip의 수
        public int RunningCount { get; set; }


        // 점검 필요한 equip의 수
        public int InspectionDue { get; set; }


        // 고장 상태인 equip의 수
        public int ErrorCount { get; set; }


        // 장비의 컨디션 점수?
        public double AverageHealth { get; set; }


        // 위태로운 기계 점수?
        public double AverageRisk { get; set; }
    }
}