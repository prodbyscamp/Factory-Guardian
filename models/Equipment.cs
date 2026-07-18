using System;
using FactoryGuardian.Models;

namespace FactoryGuardian.Models
{
    // 기계 정보
    // (nn : nn = Repository Class)
    public class Equipment : BaseModel
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }


        private string _name = "";
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        private string _modelName = "";
        public string ModelName
        {
            get => _modelName;
            set => SetProperty(ref _modelName, value);
        }


        private string _manufacturer = "";
        public string Manufacturer
        {
            get => _manufacturer;
            set => SetProperty(ref _manufacturer, value);
        }


        private string _location = "";
        public string Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }



        // 기계들 센서 데이타
        private double _temperature;
        public double Temperature
        {
            get => _temperature;
            set => SetProperty(ref _temperature, value);
        }


        private double _vibration;
        public double Vibration
        {
            get => _vibration;
            set => SetProperty(ref _vibration, value);
        }


        private double _power;
        public double Power
        {
            get => _power;
            set => SetProperty(ref _power, value);
        }


        private double _runHours;
        public double RunHours
        {
            get => _runHours;
            set => SetProperty(ref _runHours, value);
        }
        
        // 계산 결과
        private double _healthScore = 100;
        public double HealthScore
        {
            get => _healthScore;
            set => SetProperty(ref _healthScore, value);
        }


        private double _riskScore;
        public double RiskScore
        {
            get => _riskScore;
            set => SetProperty(ref _riskScore, value);
        }


        private string _riskLevel = "Low";
        public string RiskLevel
        {
            get => _riskLevel;
            set => SetProperty(ref _riskLevel, value);
        }


        private double _priorityScore;
        public double PriorityScore
        {
            get => _priorityScore;
            set => SetProperty(ref _priorityScore, value);
        }


        private string _status = "Idle";
        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }



        // 점검 정보
        private DateTime? _lastInspection;
        public DateTime? LastInspection
        {
            get => _lastInspection;
            set => SetProperty(ref _lastInspection, value);
        }


        private DateTime? _nextInspection;
        public DateTime? NextInspection
        {
            get => _nextInspection;
            set => SetProperty(ref _nextInspection, value);
        }


        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}