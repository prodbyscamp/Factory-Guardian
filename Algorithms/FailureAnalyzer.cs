namespace FactoryGuardian.Algorithms
{

    // 고장 이력을 분석하여 데이터통계를 생성하는 클라스
    public class FailureAnalyzer
    {
        public class FailureRecord
        {

            public int EquipmentID { get; set; }
            public string EquipmentName { get; set; }
            public DateTime FailureDate { get; set; }
            public string FailureType { get; set; }

        }

        // 설비별 고장 발생 횟수 집계
        public Dictionary<string, int> GetMonthlyFailureCount(List<FailureRecord> records)
        {
            return records
                .GroupBy(r => r.FailureDate.ToString("yyyy-MM"))
                .OrderBy(g => g.Key)
                .ToDictionary(
                   g => g.Key,
                   g => g.Count()
                );
        }

        // 고장 유형별 발생 비율 집계.
        public Dictionary<string, int> GetFailureCountByEquipment(List<FailureRecord> records)
        {
            return records
                .GroupBy(r => r.EquipmentName)
                .OrderByDescending(g => g.Count())
                .ToDictionary(
                   g => g.Key,
                   g => g.Count()
                );

        }

        // 고장 유형별 발생 비율 계산
        public Dictionary<string, double> GetFailureTypeRatio(List<FailureRecord> records)
        {
            int totalCount = records.Count;

            if (totalCount == 0)
                return new Dictionary<string, double>();

            return records
                .GroupBy(r => r.FailureType)
                .ToDictionary(
                   g => g.Key,
                   g => Math.Round((double)g.Count() / totalCount * 100, 1)
                );
        }

        // 평균 고장 간격 (Mean Time Between Failures)을 계산
        public double CalculateMTBF(double totalRunningHours,
            int failureCount)
        {
            if (failureCount <= 0)
                return -1.0;

            return Math.Round(totalRunningHours / failureCount, 1);
        }

        // 특정 설비의 최근 기간 내 고장 횟수 조회
        public int GetRecentFailureCount(
            List<FailureRecord> r, int equipmentID, int days)
        {
            DateTime cutoffDate = DateTime.Today.AddDays(-days);

            return r
                .Count(r => r.EquipmentID == equipmentID && 
                r.FailureDate >= cutoffDate);
        }
    }
}
