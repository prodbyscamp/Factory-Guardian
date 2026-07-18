using FactoryGuardian.Models;

namespace FactoryGuardian.Models
{
    public class Alarm : BaseModel
    {
        public int Id { get; set; }

        public int EquipmentId { get; set; }

        public string Message { get; set; } = "";

        public string Level { get; set; } = "Info";

        public DateTime CreatedAt { get; set; }
    }
}