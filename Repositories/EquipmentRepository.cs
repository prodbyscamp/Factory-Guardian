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