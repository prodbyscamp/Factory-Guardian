using Microsoft.Data.Sqlite;
using FactoryGuardian.Models;

namespace FactoryGuardian.Repositories
{
    public class EquipmentRepository
    {
        private readonly string _connectionString;


        public EquipmentRepository(string connectionString)
        {
            _connectionString = connectionString;
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = """
                CREATE TABLE IF NOT EXISTS Equipment (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    ModelName TEXT,
                    Manufacturer TEXT,
                    Location TEXT,
                    Temperature REAL DEFAULT 0,
                    Vibration REAL DEFAULT 0,
                    Power REAL DEFAULT 0,
                    RunningHours REAL DEFAULT 0,
                    HealthScore REAL DEFAULT 100,
                    RiskScore REAL DEFAULT 0,
                    RiskLevel TEXT DEFAULT 'Low',
                    PriorityScore REAL DEFAULT 0,
                    Status TEXT DEFAULT '정상',
                    LastInspectionDate TEXT,
                    NextInspectionDate TEXT,
                    UpdatedAt TEXT
                );
                """;
            command.ExecuteNonQuery();
            
            // Insert dummy data if table is empty
            command.CommandText = "SELECT COUNT(*) FROM Equipment";
            long count = (long)command.ExecuteScalar();
            if (count == 0)
            {
                command.CommandText = """
                    INSERT INTO Equipment (Name, ModelName, Manufacturer, Location, Temperature, Vibration, Power, RunningHours, HealthScore, RiskScore, RiskLevel, PriorityScore, Status)
                    VALUES 
                    ('로봇 암 1호기', 'RA-1000', 'TechCorp', 'Line A', 45.5, 0.2, 500.0, 1200.5, 95.0, 10.0, 'Low', 5.0, '정상'),
                    ('컨베이어 벨트', 'CB-200', 'LogiSys', 'Line A', 35.0, 0.1, 200.0, 8000.0, 80.0, 40.0, 'Mid', 45.0, '점검 필요');
                """;
                command.ExecuteNonQuery();
            }
        }


        // 전체 기계 검색ㄱ
        public List<Equipment> GetAll()
        {
            var list = new List<Equipment>();

            using var connection =
                new SqliteConnection(_connectionString);

            connection.Open();


            var command = connection.CreateCommand();

            command.CommandText =
            """
            SELECT
                Id,
                Name,
                ModelName,
                Manufacturer,
                Location,
                Temperature,
                Vibration,
                Power,
                RunningHours,
                HealthScore,
                RiskScore,
                RiskLevel,
                PriorityScore,
                Status,
                LastInspectionDate,
                NextInspectionDate
            FROM Equipment;
            """;


            using var reader = command.ExecuteReader();


            while (reader.Read())
            {
                var equipment = new Equipment();

                equipment.Id =
                    reader.GetInt32(0);

                equipment.Name =
                    reader.GetString(1);


                equipment.ModelName =
                    reader.IsDBNull(2)
                    ? ""
                    : reader.GetString(2);


                equipment.Manufacturer =
                    reader.IsDBNull(3)
                    ? ""
                    : reader.GetString(3);


                equipment.Location =
                    reader.IsDBNull(4)
                    ? ""
                    : reader.GetString(4);


                equipment.Temperature =
                    reader.GetDouble(5);


                equipment.Vibration =
                    reader.GetDouble(6);


                equipment.Power =
                    reader.GetDouble(7);


                equipment.RunHours =
                    reader.GetDouble(8);


                equipment.HealthScore =
                    reader.GetDouble(9);


                equipment.RiskScore =
                    reader.GetDouble(10);


                equipment.RiskLevel =
                    reader.GetString(11);


                equipment.PriorityScore =
                    reader.GetDouble(12);


                equipment.Status =
                    reader.GetString(13);



                list.Add(equipment);
            }


            return list;
        }




        // 기계 조회
        public Equipment? GetById(int id)
        {
            using var connection =
                new SqliteConnection(_connectionString);

            connection.Open();


            var command = connection.CreateCommand();


            command.CommandText =
            """
            SELECT *
            FROM Equipment
            WHERE Id = $id;
            """;


            command.Parameters.AddWithValue(
                "$id",
                id);



            using var reader =
                command.ExecuteReader();


            if (!reader.Read())
                return null;



            return new Equipment
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Temperature = reader.GetDouble(6),
                Vibration = reader.GetDouble(7),
                Power = reader.GetDouble(8),
                RunHours = reader.GetDouble(9)
            };
        }





        // 기계 설비 추가
        public void Add(Equipment equipment)
        {
            using var connection =
                new SqliteConnection(_connectionString);


            connection.Open();


            var command =
                connection.CreateCommand();


            command.CommandText =
            """
            INSERT INTO Equipment
            (
                Name,
                ModelName,
                Manufacturer,
                Location
            )
            VALUES
            (
                $name,
                $model,
                $maker,
                $location
            );
            """;


            command.Parameters.AddWithValue(
                "$name",
                equipment.Name);


            command.Parameters.AddWithValue(
                "$model",
                equipment.ModelName);


            command.Parameters.AddWithValue(
                "$maker",
                equipment.Manufacturer);


            command.Parameters.AddWithValue(
                "$location",
                equipment.Location);


            command.ExecuteNonQuery();
        }




        // 기계 상태 업데이트
        public void UpdateStatus(
            Equipment equipment)
        {
            using var connection =
                new SqliteConnection(_connectionString);


            connection.Open();


            var command =
                connection.CreateCommand();


            command.CommandText =
            """
            UPDATE Equipment
            SET
                Temperature = $temp,
                Vibration = $vibration,
                HealthScore = $health,
                RiskScore = $risk,
                RiskLevel = $riskLevel,
                PriorityScore = $priority,
                UpdatedAt = CURRENT_TIMESTAMP

            WHERE Id = $id;
            """;


            command.Parameters.AddWithValue(
                "$temp",
                equipment.Temperature);


            command.Parameters.AddWithValue(
                "$vibration",
                equipment.Vibration);


            command.Parameters.AddWithValue(
                "$health",
                equipment.HealthScore);


            command.Parameters.AddWithValue(
                "$risk",
                equipment.RiskScore);


            command.Parameters.AddWithValue(
                "$riskLevel",
                equipment.RiskLevel);


            command.Parameters.AddWithValue(
                "$priority",
                equipment.PriorityScore);


            command.Parameters.AddWithValue(
                "$id",
                equipment.Id);


            command.ExecuteNonQuery();
        }
    }
}