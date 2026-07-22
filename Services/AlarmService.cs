using FactoryGuardian.Models;



namespace FactoryGuardian.Services
{
    public class AlarmService
    {
        public Alarm CreateAlarm(Equipment equipment)
        {

            var alarm = new Alarm();


            alarm.EquipmentId = equipment.Id;
            alarm.CreatedAt = DateTime.Now;



            if (equipment.RiskScore >= 80)
            {
                alarm.Level = "Critical";
                alarm.Message =
                    $"{equipment.Name} 위험 상태";
            }

            else if (equipment.RiskScore >= 50)
            {
                alarm.Level = "Warning";
                alarm.Message =
                    $"{equipment.Name} 점검 필요";
            }

            else
            {
                alarm.Level = "Info";
                alarm.Message =
                    $"{equipment.Name} 정상";
            }


            alarm.CreatedAt = DateTime.Now;


            return alarm;
        }
    }
}