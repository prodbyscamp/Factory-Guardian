using FactoryGuardian.Algorithms;
using FactoryGuardian.Models;

namespace FactoryGuardian.Services
{
    public class FailureService
    {

        private readonly FailureAnalyzer _analyzer;



        public FailureService()
        {
            _analyzer =
                new FailureAnalyzer();
        }




        public Dictionary<string, int>
            GetMonthlyStatistics(
            List<FailureAnalyzer.FailureRecord> records)
        {

            return _analyzer
                .GetMonthlyFailureCount(records);

        }




        public double CalculateMTBF(
            double runningHours,
            int failureCount)
        {

            return _analyzer
                .CalculateMTBF(
                    runningHours,
                    failureCount);
        }

    }
}