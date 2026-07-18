using System;

namespace FactoryGuardian.Models
{
    public class MaintenanceTask : BaseModel
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }


        private int _equipmentId;
        public int EquipmentId
        {
            get => _equipmentId;
            set => SetProperty(ref _equipmentId, value);
        }


        // 화면 표시용 디비엔 저장 안됨
        private string _equipmentName = "";
        public string EquipmentName
        {
            get => _equipmentName;
            set => SetProperty(ref _equipmentName, value);
        }


        private string _title = "";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        private string _description = "";
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }


        // 예방 및 고장 수리
        private string _taskType = "Preventive";
        public string TaskType
        {
            get => _taskType;
            set => SetProperty(ref _taskType, value);
        }


        private string _priority = "Medium";
        public string Priority
        {
            get => _priority;
            set => SetProperty(ref _priority, value);
        }


        // 생성완료
        private string _status = "Created";
        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }


        private string _assignedTo = "";
        public string AssignedTo
        {
            get => _assignedTo;
            set => SetProperty(ref _assignedTo, value);
        }


        public DateTime CreatedDate { get; set; }


        public DateTime? StartedDate { get; set; }


        public DateTime? CompletedDate { get; set; }


        private string _note = "";
        public string Note
        {
            get => _note;
            set => SetProperty(ref _note, value);
        }
    }
}